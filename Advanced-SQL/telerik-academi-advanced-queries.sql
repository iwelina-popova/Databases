--Task: Write a SQL query to find the names and salaries of the employees that take the minimal salary in the company.
--Use a nested SELECT statement.
SELECT e.FirstName + ' ' + e.LastName AS [Employees name], Salary
FROM Employees e
WHERE Salary = 
  (SELECT MIN(Salary) FROM Employees)
  
 --Task: Write a SQL query to find the names and salaries of the employees that have a salary
 --that is up to 10% higher than the minimal salary for the company.
DECLARE @MinSalary int = (SELECT MIN(Salary) FROM Employees)
DECLARE @HigherThanSalary int = @MinSalary + (@MinSalary * 0.1)

SELECT e.FirstName + ' ' + e.LastName AS [Employees name], Salary
FROM Employees e
WHERE Salary > @HigherThanSalary
   
 --Task: Write a SQL query to find the full name, salary and department of the employees
 --that take the minimal salary in their department.
SELECT e.FirstName + ' ' + e.LastName AS [FullName], e.Salary, d.Name
FROM Employees e
JOIN Departments d
	ON e.DepartmentID = d.DepartmentID
WHERE Salary = 
	(SELECT MIN(em.Salary)
	FROM Employees em
	WHERE em.DepartmentID = d.DepartmentID)
   
 --Task: Write a SQL query to find the average salary in the department #1.
SELECT AVG(Salary) AS [Average Salary]
FROM Employees 
WHERE DepartmentID = 1
   
 --Task: Write a SQL query to find the average salary in the "Sales" department.
SELECT AVG(Salary) AS [Average Salary in "Sales"]
FROM Employees e
JOIN Departments d
	ON e.DepartmentID = d.DepartmentID 
WHERE d.Name = 'Sales'
   
 --Task: Write a SQL query to find the number of employees in the "Sales" department.
SELECT COUNT(*) AS [Employees Count in "Sales"]
FROM Employees e
JOIN Departments d
	ON e.DepartmentID = d.DepartmentID 
WHERE d.Name = 'Sales'
   
 --Task: Write a SQL query to find the number of all employees that have manager.
SELECT COUNT(ManagerID) AS [Employees Count with manager]
FROM Employees 
   
 --Task: Write a SQL query to find the number of all employees that have no manager.
SELECT COUNT(*) AS [Employees Count without manager]
FROM Employees
WHERE ManagerID IS NULL
   
 --Task: Write a SQL query to find all departments and the average salary for each of them.
SELECT d.Name AS [Department], AVG(e.Salary) AS [Average Salary]
FROM Employees e
JOIN Departments d
	ON e.DepartmentID = d.DepartmentID
GROUP BY d.Name
   
 --Task: Write a SQL query to find the count of all employees in each department and for each town.
SELECT COUNT(*) AS [Employees Count], d.Name, t.Name
FROM Employees e
JOIN Departments d
	ON e.DepartmentID = d.DepartmentID
JOIN Addresses a
	ON e.AddressID = a.AddressID
JOIN Towns t
	ON a.TownID = t.TownID
GROUP BY d.Name, t.Name
   
 --Task: Write a SQL query to find all managers that have exactly 5 employees.
 --Display their first name and last name.
SELECT e.FirstName + ' ' + e.LastName AS [Manager name]
FROM Employees e
JOIN Employees em
	ON em.ManagerID = e.EmployeeID
GROUP BY e.EmployeeID, e.FirstName, e.LastName
HAVING COUNT(e.EmployeeID) = 5
   
 --Task: Write a SQL query to find all employees along with their managers.
 --For employees that do not have manager display the value "(no manager)".
SELECT e.FirstName + ' ' + e.LastName AS [Employee name],
		COALESCE(em.FirstName + ' ' + em.LastName, 'no manager') AS [Manager name]
FROM Employees e
LEFT JOIN Employees em
	ON e.ManagerID = em.EmployeeID
   
 --Task: Write a SQL query to find the names of all employees whose last name is
 --exactly 5 characters long. Use the built-in LEN(str) function.
SELECT e.FirstName, e.LastName
FROM Employees e
WHERE LEN(e.LastName) = 5
   
 --Task: Write a SQL query to display the current date and time in the following
 --format "day.month.year hour:minutes:seconds:milliseconds".
  --Search in Google to find how to format dates in SQL Server.
SELECT FORMAT(GETDATE(), 'dd.MM.yyyy HH:mm:ss:ff') 
 
 --Task: Write a SQL statement to create a table Users.
  --Users should have username, password, full name and last login time.
  --Choose appropriate data types for the table fields. Define a primary key column with a primary key constraint.
  --Define the primary key column as identity to facilitate inserting records.
  --Define unique constraint to avoid repeating usernames.
  --Define a check constraint to ensure the password is at least 5 characters long.
 CREATE TABLE Users (
	Id int IDENTITY,
	Username nvarchar(50) NOT NULL CHECK(LEN(Username) > 5) UNIQUE,
	Pass nvarchar(50) NOT NULL CHECK(LEN(Pass) > 5),
	FullName nvarchar(100) NOT NULL,
	LastLoginTime DATETIME
	CONSTRAINT PK_Users PRIMARY KEY(Id)
)
GO
   
 --Task: Write a SQL statement to create a view that displays the users from the Users table
 --that have been in the system today.
  --Test if the view works correctly.
CREATE VIEW [Todays Logged Users] AS
SELECT Username
FROM Users
WHERE DATEDIFF(DAY, LastLoginTime, GETDATE()) = 0
   
 --Task: Write a SQL statement to create a table Groups.
 --Groups should have unique name (use unique constraint).
  --Define primary key and identity column.
CREATE TABLE Groups (
	Id int IDENTITY PRIMARY KEY,
	Name nvarchar(50) NOT NULL UNIQUE
)
GO
   
 --Task: Write a SQL statement to add a column GroupID to the table Users.
 --Fill some data in this new column and as well in the `Groups table.
 --Write a SQL statement to add a foreign key constraint between tables Users and Groups tables.
ALTER TABLE Users
ADD GroupId int NOT NULL
GO

ALTER TABLE Users
ADD CONSTRAINT FK_Users_Groups
	FOREIGN KEY (GroupId)
	REFERENCES Groups(Id)
GO

 
 --Task: Write SQL statements to insert several records in the Users and Groups tables.
INSERT INTO Groups
VALUES 
	('C#'),
	('JavaScript'),
	('PHP'),
	('Perl'),
	('Java')

INSERT INTO Users
VALUES
	('someuser', 'parolanqkakva', 'Some User', '2015-10-10 00:00:00', 1),
	('otheruser', 'parolanqkakva', 'Other User', '2014-10-10 00:00:00', 1),
	('someother', 'parolanqkakva', 'Some Other', '2015-4-14 00:00:00', 3),
	('anyuser', 'parolanqkakva', 'Any User', '2015-3-03 00:00:00', 2),
	('someuser1', 'parolanqkakva', 'Some User 1', '2015-1-11 00:00:00', 4),
	('someuser2', 'parolanqkakva', 'Some User 2', '2013-12-12 00:00:00', 5),
	('someuser3', 'parolanqkakva', 'Some User 2', '2015-1-01 00:00:00', 5)
   
 --Task: Write SQL statements to update some of the records in the Users and Groups tables.
UPDATE Groups
	SET Name = REPLACE(Name, 'Perl', 'Python')
	WHERE Id = 4
	
UPDATE Users
	SET FullName = REPLACE(FullName, 'Some', 'None')
   
 --Task: Write SQL statements to delete some of the records from the Users and Groups tables.
DELETE
FROM Users
WHERE GroupId = 5

DELETE *
FROM Groups
WHERE Id = 5

 --Task: Write SQL statements to insert in the Users table the names of all employees from the Employees table.
 --Combine the first and last names as a full name.
 --For username use the first letter of the first name + the last name (in lowercase).
 --Use the same for the password, and NULL for last login time.
INSERT INTO Users (Username, Pass, FullName, LastLoginTime)
	(SELECT LOWER(LEFT(FirstName, 1) + LastName),
		LOWER(LEFT(FirstName, 1) + LastName),
		FirstName + ' ' + LastName,
		NULL
	FROM Employees)
GO
   
 --Task: Write a SQL statement that changes the password to NULL for all users that
 --have not been in the system since 10.03.2010.
UPDATE Users
SET Pass = NULL
WHERE DATEDIFF(day, LastLoginTime, '2010-03-10') > 0
   
 --Task: Write a SQL statement that deletes all users without passwords (NULL password).
DELETE
FROM Users
WHERE Pass IS NULL
   
 --Task: Write a SQL query to display the average employee salary by department and job title.
SELECT d.Name AS [Department], e.JobTitle, AVG(e.Salary) AS [Average Salary]
FROM Employees e
JOIN Departments d
	ON e.DepartmentID = d.DepartmentID
GROUP BY d.Name, e.JobTitle
   
 --Task: Write a SQL query to display the minimal employee salary by department and
 --job title along with the name of some of the employees that take it.
SELECT MIN(e.FirstName + ' ' + e.LastName) AS [Employee name],
	 d.Name AS [Department], e.JobTitle,
	 MIN(e.Salary) AS [Minimal Salary]
FROM Employees e
JOIN Departments d
	ON e.DepartmentID = d.DepartmentID
GROUP BY d.Name, e.JobTitle
   
 --Task: Write a SQL query to display the town where maximal number of employees work.
SELECT TOP 1 t.Name AS Town, COUNT(e.EmployeeID) AS [Employees count]
FROM Employees e
JOIN Addresses a
	ON e.AddressID = a.AddressID
JOIN Towns t
	ON a.TownID = t.TownID
GROUP BY t.Name
ORDER BY [Employees count] DESC
   
 --Task: Write a SQL query to display the number of managers from each town.
SELECT t.Name AS Town, COUNT(e.EmployeeID) AS [Managers count]
FROM Employees e
JOIN Addresses a
	ON e.AddressID = a.AddressID
JOIN Towns t
	ON a.TownID = t.TownID
GROUP BY t.Name
ORDER BY [Managers count]
   
 --Task: Write a SQL to create table WorkHours to store work reports for each employee
 --(employee id, date, task, hours, comments).
 --Don't forget to define identity, primary key and appropriate foreign key.
 --Issue few SQL statements to insert, update and delete of some data in the table.
 --Define a table WorkHoursLogs to track all changes in the WorkHours table with triggers.
 --For each change keep the old record data, the new record data and the command (insert / update / delete).
   
   --Create table WorkHours
   CREATE TABLE WorkHours (
	Id int IDENTITY,
	EmployeeId int NOT NULL,
	Date DATETIME NOT NULL,
	Task nvarchar(500) NOT NULL,
	Hours int NOT NULL,
	Comments nvarchar(1000),
	CONSTRAINT PK_Id PRIMARY KEY(Id),
	CONSTRAINT FK_Employees_WorkHours
		FOREIGN KEY (EmployeeId)
		REFERENCES Employees(EmployeeID)
	)
	GO
	
	--Insert values
	INSERT INTO WorkHours(EmployeeId, [Date], Task, [Hours])
	VALUES
		(1, '2015-10-10', 'Create something new', 8),
		(2, '2015-10-11', 'Create something new', 10),
		(5, '2015-1-01', 'Create something new', 18),
		(10, '2015-10-10', 'Create something new', 20),
		(15, '2015-10-10', 'Create something new', 9),
		(12, '2015-10-10', 'Create something new', 30),
		(20, '2015-4-04', 'Create something new', 40),
		(31, '2015-10-10', 'Create something new', 8),
		(41, '2015-5-5', 'Create something new', 13)
		
	--Update data
	UPDATE WorkHours
	SET Comments = 'Good work'
	WHERE [Hours] < 15
	
	--Delete data
	DELETE
	FROM WorkHours
	WHERE Id IN (2, 5)
	
	--Create table WorkHoursLogs
	CREATE TABLE WorkHoursLogs (
		WorkLogId int IDENTITY,
		EmployeeId Int NOT NULL,
		OnDate DATETIME NOT NULL,
		Task nvarchar(500) NOT NULL,
		Hours Int NOT NULL,
		Comments nvarchar(1000),
		[Action] nvarchar(50) NOT NULL,
		CONSTRAINT FK_Employees_WorkHoursLogs
			FOREIGN KEY (EmployeeId)
			REFERENCES Employees(EmployeeID),
		CONSTRAINT [CC_WorkReportsLogs] CHECK ([Action] IN ('Insert', 'Delete', 'DeleteUpdate', 'InsertUpdate'))
	) 
	GO
	
	-- TRIGGER FOR INSERT
CREATE TRIGGER tr_InsertWorkReports ON WorkHours FOR INSERT
AS
INSERT INTO WorkHoursLogs(WorkLogId, EmployeeId, OnDate, Task, [Hours], Comments, [Action])
    SELECT WorkReportId, EmployeeID, OnDate, Task, [Hours], Comments, 'Insert'
    FROM inserted
GO

-- TRIGGER FOR DELETE
CREATE TRIGGER tr_DeleteWorkReports ON WorkHours FOR DELETE
AS
INSERT INTO WorkHoursLogs(WorkLogId, EmployeeId, OnDate, Task, [Hours], Comments, [Action])
    SELECT WorkReportId, EmployeeID, OnDate, Task, [Hours], Comments, 'Delete'
    FROM deleted
GO

-- TRIGGER FOR UPDATE
CREATE TRIGGER tr_UpdateWorkReports ON WorkHours FOR UPDATE
AS
INSERT INTO WorkHoursLogs(WorkLogId, EmployeeId, OnDate, Task, [Hours], Comments, [Action])
    SELECT WorkReportId, EmployeeID, OnDate, Task, [Hours], Comments, 'InsertUpdate'
    FROM inserted
	
	INSERT INTO WorkHoursLogs(WorkLogId, EmployeeId, OnDate, Task, [Hours], Comments, [Action])
    SELECT WorkReportId, EmployeeID, OnDate, Task, [Hours], Comments, 'DeleteUpdate'
    FROM deleted
GO

-- TEST TRIGGERS
DELETE
    FROM WorkHoursLogs

INSERT INTO WorkHours(EmployeeId, OnDate, Task, [Hours])
    VALUES (25, GETDATE(), 'TASK: 25', 25)

DELETE
    FROM WorkHours
    WHERE EmployeeId = 25

UPDATE WorkHours
    SET Comments = 'Updated'
    WHERE EmployeeId = 2

 --Task: Start a database transaction, delete all employees from the 'Sales' department
 --along with all dependent records from the pother tables.
 --At the end rollback the transaction.
BEGIN TRAN

	ALTER TABLE Departments
		DROP CONSTRAINT FK_Department_Employees
	GO

	DELETE
	FROM Employees e
	JOIN Departments d
		ON e.DepartmentID = d.DepartmentID
	WHERE d.Name = 'Sales'

ROLLBACK TRAN
   
 --Task: Start a database transaction and drop the table EmployeesProjects.
 --Now how you could restore back the lost table data?
BEGIN TRAN

	DROP TABLE EMployeesProjects

ROLLBACK TRAN
 
 --Task: Find how to use temporary tables in SQL Server.
 --Using temporary tables backup all records from EmployeesProjects and restore
 --them back after dropping and re-creating the table.
BEGIN TRANSACTION

    SELECT * 
        INTO #TempEmployeesProjects  --- Create new table
        FROM EmployeesProjects

    DROP TABLE EmployeesProjects

    SELECT * 
        INTO EmployeesProjects --- Create new table
        FROM #TempEmployeesProjects

    DROP TABLE #TempEmployeesProjects

ROLLBACK TRANSACTION
 
