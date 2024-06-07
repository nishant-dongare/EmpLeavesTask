Create database Task1;
GO

USE Task1;
GO

--DROP TABLE Employee;
CREATE TABLE Employee(
	emp_id int primary key identity,
    ename varchar(100),
	email varchar(100),
	contact decimal(10,0),
	doj VARCHAR(20)
);
GO

SELECT * FROM Employee;
Go

ALTER PROCEDURE CreateEmployee
    @Name VARCHAR(100),
    @Email VARCHAR(100),
    @Contact DECIMAL(10,0),
    @DOJ DATETIME
AS
BEGIN
    INSERT INTO Employee (ename,email, contact, doj)
    VALUES (@Name,@Email, @Contact, @DOJ);
    SELECT SCOPE_IDENTITY();
END;
GO
-- Create stored procedure to get all employees
CREATE PROCEDURE GetAllEmployees
AS
BEGIN
    -- Select statement to retrieve all employee records
    SELECT emp_id, ename, email, contact, doj
    FROM Employee;
END;
GO

-- Create stored procedure to get employee by ID
CREATE PROCEDURE GetEmployeeById
    @emp_id INT
AS
BEGIN
    -- Select statement to retrieve a specific employee by emp_id
    SELECT emp_id, ename, email, contact, doj
    FROM Employee
    WHERE emp_id = @emp_id;
END;
GO

ALTER PROCEDURE UpdateEmployee
    @emp_id INT,
    @ename VARCHAR(100),
    @Email VARCHAR(100),
    @Contact DECIMAL(10,0),
    @Doj VARCHAR(20)
AS
BEGIN
    -- Update statement to modify an existing employee record
    UPDATE Employee
    SET ename = @ename, email = @Email, contact = @Contact, doj = @Doj
    WHERE emp_id = @emp_id;
END;
GO


CREATE PROCEDURE DeleteEmployee
@EmpID INT
AS
BEGIN
    DELETE FROM Employee WHERE emp_id = @EmpID
END
GO


DROP TABLE LeaveApplication;
CREATE TABLE LeaveApplication(
    id int primary key identity,
    fromdate VARCHAR(20),
    todate VARCHAR(20),
    reason varchar(255),
    month int,
    year int,
    countofleaves INT,
    emp_id int references Employee(emp_id)
);
GO

ALTER PROCEDURE InsertLeaveApplication
    @fromdate VARCHAR(20),
    @todate VARCHAR(20),
    @reason VARCHAR(255),
    @month INT,
    @year INT,
    @emp_id INT,
    @countofleaves INT,
    @newId INT OUTPUT
AS
BEGIN
    -- Insert the new leave application
    INSERT INTO LeaveApplication (fromdate, todate, reason, month, year, emp_id,countofleaves)
    VALUES (@fromdate, @todate, @reason, @month, @year, @emp_id,@countofleaves);
    
    -- Get the ID of the newly inserted row
    SET @newId = SCOPE_IDENTITY();
END;