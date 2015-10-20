-- Create a db named 'AccountsManagement'
CREATE DATABASE Bank
GO

-- Create a new table named 'People'
CREATE TABLE People (
	Id int IDENTITY NOT NULL PRIMARY KEY,
	FirstName nvarchar(50) NOT NULL,
	LastName nvarchar(50) NOT NULL,
	SSN nvarchar(50) NOT NULL
)
GO

-- Create a new table named 'Accounts'
CREATE TABLE Accounts (
	Id int IDENTITY NOT NULL PRIMARY KEY,
	PersonId int NOT NULL FOREIGN KEY 
		REFERENCES People(Id),
	Balance money NOT NULL
)
GO

-- Insert some values in two tables
INSERT INTO People
	(FirstName, LastName, SSN)
VALUES 
	('Ivo','Ivov','123456'),
	('Todor','Todorov','234567'),
	('Mara','Marova','345678'),
	('Lina','Linova','456789')

INSERT INTO Accounts
	(PersonId, Balance)
VALUES 
	(1,1000.14),
	(2,10000.52),
	(3,20000.00),
	(4,53214.75)

-- Write a stored procedure that selects the full names of all persons.
USE Bank
GO

CREATE PROCEDURE dbo.usp_PersonFullName
AS
	SELECT FirstName + ' ' + LastName AS [FullName]
	FROM People
GO

EXEC dbo.usp_PersonFullName
GO

-- Create a stored procedure that accepts a number as a parameter and
-- returns all persons who have more money in their accounts than the supplied number.
USE Bank
GO

CREATE PROCEDURE dbo.usp_PeopleWithMoreMoneyThan (
	@minMoney money = 400)
AS
	SELECT FirstName + ' ' + LastName AS [FullName], a.Balance
	FROM People p
	JOIN Accounts a
		ON p.Id = a.PersonId
	WHERE a.Balance > @minMoney
	ORDER BY a.Balance
GO

EXEC dbo.usp_PeopleWithMoreMoneyThan
GO

-- Create a function that accepts as parameters – sum, yearly interest rate and number of months.
-- It should calculate and return the new sum.
-- Write a SELECT to test whether the function works as expected.
CREATE FUNCTION ufn_CalculateInterest(
	@sum money, @yearlyRate decimal, @months int)
	RETURNS money
AS
BEGIN
	RETURN @sum * (1 + @yearlyRate / 12) * @months
END
GO

SELECT Balance, ROUND(dbo.ufn_CalculateInterest(Balance, 0.05, 10), 2) as [Bonus]
FROM Accounts

-- Create a stored procedure that uses the function from the previous example to give
-- an interest to a person's account for one month.
--It should take the AccountId and the interest rate as parameters.
CREATE PROCEDURE usp_CalculateInterestForOneMonth(
	@accountId int, @yearlyInterestRate decimal)
AS
	DECLARE @givenIdBalance money = 
		(SELECT Balance 
		FROM Accounts
			WHERE Id = @accountId)

	DECLARE @newBalance money = 
		dbo.ufn_CalculateInterest(@givenIdBalance, @yearlyInterestRate, 1)

	SELECT p.FirstName, p.LastName, a.Balance, @newBalance AS [Interest for one Month]
	FROM People p
	JOIN Accounts a
		ON p.Id = a.PersonId
	WHERE a.Id = @accountId
GO

EXEC dbo.usp_CalculateInterestForOneMonth 1, 0.5

-- Add two more stored procedures WithdrawMoney(AccountId, money) and 
-- DepositMoney(AccountId, money) that operate in transactions.
CREATE PROCEDURE usp_WithdrawMoney(
	@accountId int, @money money)
AS
	BEGIN TRANSACTION
		UPDATE Accounts
		SET Balance = Balance - @money
		WHERE Id = @accountId
	COMMIT TRANSACTION
GO

CREATE PROCEDURE usp_DepositMoney(
	@accountId int, @money money)
AS
	BEGIN TRANSACTION
		UPDATE Accounts
		SET Balance = Balance + @money
		WHERE Id = @accountId
	COMMIT TRANSACTION
GO

EXEC dbo.usp_WithdrawMoney 1, 200
EXEC dbo.usp_DepositMoney 3, 100000

-- Create another table – Logs(LogID, AccountID, OldSum, NewSum).
-- Add a trigger to the Accounts table that enters a new entry into the Logs table 
-- every time the sum on an account changes.
CREATE TABLE Logs(
	LogId int IDENTITY PRIMARY KEY,
	AccountId int NOT NULL 
		FOREIGN KEY REFERENCES Accounts(Id),
	OldSum money NOT NULL,
	NewSum money NOT NULL
)
GO

CREATE TRIGGER tr_AccountsUpdate ON Accounts FOR UPDATE
AS
	INSERT INTO Logs(AccountId, OldSum, NewSum)
		SELECT i.Id, d.Balance, i.Balance
		FROM inserted AS i
		JOIN deleted AS d
			ON i.Id = d.Id
GO

UPDATE Accounts
SET Balance = Balance + 2000
WHERE Id = 4

-- Define a function in the database TelerikAcademy that returns all Employee's names
-- (first or middle or last name) and all town's names that are comprised of given set of letters.
-- Example: 'oistmiahf' will return 'Sofia', 'Smith', … but not 'Rob' and 'Guy'.
USE TelerikAcademy
GO

CREATE FUNCTION ufn_FindEmployeeOrTownNames(@pattern nvarchar)
	RETURNS @matchedNames TABLE (FoundedName nvarchar(100))
AS
BEGIN
	INSERT @matchedNames
		SELECT *
		FROM
			(SELECT FirstName FROM Employees
			UNION
			SELECT LastName FROM Employees
			UNION
			SELECT Name FROM Towns)
		AS temp(name)
	WHERE PATINDEX('%[' + @pattern + ']', name) > 0
	RETURN
END
GO

SELECT * FROM dbo.ufn_FindEmployeeOrTownNames('oistmiahf')

-- Using database cursor write a T-SQL script that scans all employees and their 
-- addresses and prints all pairs of employees that live in the same town
DECLARE employeeCursor CURSOR READ_ONLY FOR
	SELECT e1.FirstName + ' ' + e1.LastName AS [First Employee],
			t1.Name AS Town,
			e2.FirstName + ' ' + e2.LastName AS [Second Employee]			
	FROM Employees e1
	JOIN Addresses a1
		ON e1.AddressID = a1.AddressID
	JOIN Towns t1
		ON a1.TownID = t1.TownID,
		Employees e2
		JOIN Addresses a2
			ON e2.AddressID = a2.AddressID
		JOIN Towns t2
			ON a2.TownID = t2.TownID
		WHERE t1.TownID = t2.TownID 
			AND e1.EmployeeID != e2.EmployeeID
		ORDER BY [First Employee], [Second Employee]

OPEN employeeCursor

DECLARE @firstEmployee nvarchar(100),
		@secondEmployee nvarchar(100),
		@townName nvarchar(50)
FETCH NEXT FROM employeeCursor 
	INTO @firstEmployee, @townName, @secondEmployee

WHILE @@FETCH_STATUS = 0
	BEGIN
		PRINT @firstEmployee + '| and |' + @secondEmployee + '| - |' + @townName

		FETCH NEXT FROM employeeCursor
		INTO @firstEmployee, @townName, @secondEmployee
	END

CLOSE employeeCursor
DEALLOCATE employeeCursor

-- *Write a T-SQL script that shows for each town a list of all employees that live 
-- in it.
-- Sample output:
-- Sofia -> Martin Kulov, George Denchev
-- Ottawa -> Jose Saraiva
CREATE TABLE EmployeesTowns(
	Id int IDENTITY PRIMARY KEY,
	EmployeeName nvarchar(100) NOT NULL,
	TownName nvarchar(100) NOT NULL
	)

INSERT INTO EmployeesTowns
	SELECT e.FirstName + ' ' + e.LastName, t.Name
	FROM Employees e
	JOIN Addresses a
		ON e.AddressID = a.AddressID
	JOIN Towns t
		ON a.TownID = t.TownID
	GROUP BY t.Name, e.FirstName, e.LastName

DECLARE @employeeName nvarchar(100)
DECLARE @townName nvarchar(100)

DECLARE employeeCursor CURSOR READ_ONLY FOR
	SELECT DISTINCT et.TownName			
	FROM EmployeesTowns et

OPEN employeeCursor

FETCH NEXT FROM employeeCursor 
	INTO @townName

WHILE @@FETCH_STATUS = 0
		BEGIN
			DECLARE @empName nvarchar(MAX);
			SET @empName = N'';
			SELECT @empName += et.EmployeeName + N', '
			FROM EmployeesTowns et
			WHERE et.TownName = @townName
			PRINT @townName + ' -> ' + LEFT(@empName,LEN(@empName)-1);
			FETCH NEXT FROM employeeCursor INTO @townName
		END

CLOSE employeeCursor
DEALLOCATE employeeCursor

-- Define a .NET aggregate function StrConcat that takes as input a sequence of strings
-- and return a single string that consists of the input strings separated by ','.
-- For example the following SQL statement should return a single string:
-- SELECT StrConcat(FirstName + ' ' + LastName)
-- FROM Employees
IF NOT EXISTS (
    SELECT value
    FROM sys.configurations
    WHERE name = 'clr enabled' AND value = 1
)
BEGIN
    EXEC sp_configure @configname = clr_enabled, @configvalue = 1
    RECONFIGURE
END
GO

-- Remove the aggregate and assembly if they're there
IF (OBJECT_ID('dbo.concat') IS NOT NULL) 
    DROP Aggregate concat; 
GO 

IF EXISTS (SELECT * FROM sys.assemblies WHERE name = 'concat_assembly') 
    DROP assembly concat_assembly; 
GO      

CREATE Assembly concat_assembly 
   AUTHORIZATION dbo 
   FROM 'D:\Projects\Databases\Transact-SQL' --- CHANGE THE LOCATION (PATH to the same folder, where the script is)
   WITH PERMISSION_SET = SAFE; 
GO 

CREATE AGGREGATE dbo.concat ( 
    @Value NVARCHAR(MAX),
    @Delimiter NVARCHAR(4000) 
) 
    RETURNS NVARCHAR(MAX) 
    EXTERNAL Name concat_assembly.concat; 
GO 

SELECT dbo.concat(FirstName + ' ' + LastName, ', ')
FROM Employees
GO

DROP Aggregate concat; 
DROP assembly concat_assembly; 
GO