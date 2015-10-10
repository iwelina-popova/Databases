SQL Queries

--Task: Write a SQL query to find all information about all departments.
SELECT *
FROM Departments


--Task: Write a SQL query to find all department names.
SELECT Name
FROM Departments

--Task: Write a SQL query to find the salary of each employee.
SELECT e.FirstName + ' ' + e.LastName AS [Employees Name], Salary
FROM Employees e

--Task: Write a SQL to find the full name of each employee.
SELECT e.FirstName + ' ' + e.LastName AS [Employees Name]
FROM Employees e

--Task: Write a SQL query to find the email addresses of each employee (by his first and last name).
--Consider that the mail domain is telerik.com. Emails should look like “John.Doe@telerik.com".
--The produced column should be named "Full Email Addresses".
SELECT e.FirstName + '.' + e.LastName + '@telerik.com' AS [Full Email Address]
FROM Employees e

--Task: Write a SQL query to find all different employee salaries.
SELECT DISTINCT Salary
FROM Employees e

--Task: Write a SQL query to find all information about the employees whose job title is “Sales Representative“.
SELECT *
FROM Employees
WHERE JobTitle = 'Sales Representative'

--Task: Write a SQL query to find the names of all employees whose first name starts with "SA".
SELECT e.FirstName + ' ' + e.LastName AS [Employees name]
FROM Employees e
WHERE e.FirstName LIKE 'SA%'

--Task: Write a SQL query to find the names of all employees whose last name contains "ei".
SELECT e.FirstName + ' ' + e.LastName AS [Employees name]
FROM Employees e
WHERE e.LastName LIKE '%ei%'

--Task: Write a SQL query to find the salary of all employees whose salary is in the range [20000…30000].
SELECT e.FirstName + ' ' + e.LastName AS [Employees name], Salary
FROM Employees e
WHERE e.Salary BETWEEN 20000 AND 30000

--Task: Write a SQL query to find the names of all employees whose salary is 25000, 14000, 12500 or 23600.
SELECT e.FirstName + ' ' + e.LastName AS [Employees name], Salary
FROM Employees e
WHERE e.Salary IN (25000, 14000, 12500, 23600)

--Task: Write a SQL query to find all employees that do not have manager.
SELECT e.FirstName + ' ' + e.LastName AS [Employees name], Salary
FROM Employees e
WHERE e.ManagerID IS NULL

--Task: Write a SQL query to find all employees that have salary more than 50000.
--Order them in decreasing order by salary.
SELECT e.FirstName + ' ' + e.LastName AS [Employees name], Salary
FROM Employees e
WHERE e.Salary > 50000
ORDER BY Salary DESC

--Task: Write a SQL query to find the top 5 best paid employees.
SELECT TOP 5 e.FirstName + ' ' + e.LastName AS [Employees name], Salary
FROM Employees e
ORDER BY Salary DESC

--Task: Write a SQL query to find all employees along with their address.
--Use inner join with ON clause.
SELECT e.FirstName + ' ' + e.LastName AS [Employees name], a.AddressText
FROM Employees e
INNER JOIN Addresses a
ON e.AddressID = a.AddressID

--Task: Write a SQL query to find all employees and their address.
--Use equijoins (conditions in the WHERE clause).
SELECT e.FirstName + ' ' + e.LastName AS [Employees name], a.AddressText
FROM Employees e, Addresses a
WHERE e.AddressID = a.AddressID

--Task: Write a SQL query to find all employees along with their manager.
SELECT e.FirstName + ' ' + e.LastName AS [Employees name], m.FirstName + ' ' + m.LastName AS [Manager name]
FROM Employees e, Employees m
WHERE e.ManagerID = m.EmployeeID

--Task: Write a SQL query to find all employees, along with their manager and their address.
--Join the 3 tables: Employees e, Employees m and Addresses a.
--With JOIN
SELECT e.FirstName + ' ' + e.LastName AS [Employees name], a.AddressText, m.FirstName + ' ' + m.LastName AS [Manager name]
FROM Employees e
JOIN Addresses a
ON e.AddressID = a.AddressID
JOIN Employees m
ON e.ManagerID = m.EmployeeID

--With WHERE
SELECT e.FirstName + ' ' + e.LastName AS [Employees name], a.AddressText, m.FirstName + ' ' + m.LastName AS [Manager name]
FROM Employees e, Employees m, Addresses a
WHERE e.ManagerID = m.EmployeeID
	AND e.AddressID = a.AddressID

--Task: Write a SQL query to find all departments and all town names as a single list.
--Use UNION.
SELECT Name AS [Town/Department]
FROM Departments 
	UNION 
SELECT Name AS [Town/Department]
FROM Towns

--Task: Write a SQL query to find all the employees and the manager for each of them along
--with the employees that do not have manager.
--Use right outer join. Rewrite the query to use left outer join.

--Using RIGHT OUTER JOIN
SELECT e.FirstName + ' ' + e.LastName AS [Employees name], m.FirstName + ' ' + m.LastName AS [Manager name]
FROM Employees e
RIGHT OUTER JOIN Employees m
ON e.ManagerID = m.EmployeeID

--Using LEFT OUTER JOIN
SELECT e.FirstName + ' ' + e.LastName AS [Employees name], m.FirstName + ' ' + m.LastName AS [Manager name]
FROM Employees e
LEFT OUTER JOIN Employees m
ON e.ManagerID = m.EmployeeID


--Task: Write a SQL query to find the names of all employees from the departments
--"Sales" and "Finance" whose hire year is between 1995 and 2005.
SELECT e.FirstName + ' ' + e.LastName AS [Employees name], e.HireDate, d.Name
FROM Employees e
JOIN Departments d
ON e.DepartmentID = d.DepartmentID
WHERE d.Name IN ('Sales', 'Finance') AND
	(e.HireDate BETWEEN '1995-01-01' AND '2005-12-31')
ORDER BY e.FirstName, e.LastName
