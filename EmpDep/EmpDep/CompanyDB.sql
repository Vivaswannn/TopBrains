USE master;
GO

IF EXISTS (SELECT * FROM sys.databases WHERE name = 'CompanyDB')
BEGIN
    DROP DATABASE CompanyDB;
END
GO

CREATE DATABASE CompanyDB;
GO

USE CompanyDB;
GO

CREATE TABLE Departments
(
    DepartmentId INT PRIMARY KEY,
    DepartmentName VARCHAR(50),
    Location VARCHAR(50)
);
GO

CREATE TABLE Employees
(
    EmployeeId INT PRIMARY KEY,
    EmployeeName VARCHAR(50),
    Salary DECIMAL(10,2),
    DepartmentId INT
);
GO

-- Optional: Insert some sample data to start with
INSERT INTO Departments VALUES (10, 'IT', 'New York');
INSERT INTO Departments VALUES (20, 'HR', 'London');
INSERT INTO Departments VALUES (30, 'Sales', 'Tokyo');

INSERT INTO Employees VALUES (101, 'John Doe', 5000.00, 10);
INSERT INTO Employees VALUES (102, 'Jane Smith', 6000.00, 20);
GO

PRINT 'Database CompanyDB created successfully.'
