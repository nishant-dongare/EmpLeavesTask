use master;
go
 alter database Task1 set single_user with rollback immediate

 drop database Task1

Create database Task1;
GO

USE Task1;
GO

DROP TABLE Employee;
CREATE TABLE Employee(
	emp_id int primary key identity,
    ename varchar(100),
	email varchar(100),
    passkey varchar(20),
	contact decimal(10,0),
	doj VARCHAR(20)
);
GO

SELECT * FROM Employee;
Go

CREATE PROCEDURE AddEmployee
    @Name VARCHAR(100),
    @Email VARCHAR(100),
    @Passkey varchar(20),
    @Contact DECIMAL(10,0),
    @DOJ DATETIME
AS
BEGIN
    INSERT INTO Employee (ename,email,passkey, contact, doj)
    VALUES (@Name,@Email,@Passkey, @Contact, @DOJ);
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

CREATE PROCEDURE UpdateEmployee
    @emp_id INT,
    @ename VARCHAR(100),
    @Email VARCHAR(100),
    @Passkey VARCHAR(20),
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

-------------------------------------------------------------------------------------------------------------------------------------
TRUNCATE TABLE LeaveApplication;
GO

SELECT * FROM LeaveApplication;
GO
CREATE TABLE LeaveApplication(
    leave_id int primary key identity,
    fromdate VARCHAR(20),
    todate VARCHAR(20),
    reason varchar(255),
    month INT,
    year INT,
    countofleaves INT,
    emp_id INT references Employee(emp_id),
    combine_id INT,
    leave_status varchar(10)
);
GO

CREATE PROCEDURE InsertLeaveApplication
    @fromdate VARCHAR(20),
    @todate VARCHAR(20),
    @reason VARCHAR(255),
    @month int,
    @year int,
    @countofleaves INT,
    @emp_id INT,
    @combine_id INT
AS
BEGIN
    -- Insert the new leave application
    INSERT INTO LeaveApplication (fromdate, todate, reason, month, year,countofleaves, emp_id,combine_id,leave_status)
    VALUES (@fromdate, @todate, @reason, @month, @year,@countofleaves,@emp_id,@combine_id,'Requested');
    
    -- Get the ID of the newly inserted row
    SELECT SCOPE_IDENTITY();
END;
GO

CREATE PROCEDURE GetEmployeeLeaveRequests
AS
BEGIN
    SELECT e.ename, e.email, l.reason, l.fromdate, l.todate, l.leave_status,l.leave_id
    FROM Employee e
    INNER JOIN LeaveApplication l ON e.emp_id = l.emp_id;
END;
GO

CREATE PROCEDURE GetEmployeeLeaveApplications
    @EmployeeId INT
AS
BEGIN
    SELECT *
    FROM Employee e
    INNER JOIN LeaveApplication l ON e.emp_id = l.emp_id
    WHERE e.emp_id = @EmployeeId;
END;
Go

CREATE PROCEDURE UpdateLeaveStatus
    @RequestId INT,
    @NewStatus VARCHAR(50)
AS
BEGIN
    UPDATE LeaveApplication
    SET leave_status = @NewStatus
    WHERE leave_id = @RequestId;
END
------------------------------------------------------------------------------------------------------------------
drop table OfferLetters;
CREATE TABLE OfferLetters(
    offer_id INT PRIMARY KEY IDENTITY,
    name VARCHAR(100),
    email VARCHAR(100),
    contact decimal(10,0),
    doj VARCHAR(20),
    filepath varchar(100)
);

select * from OfferLetters;

------------------------------------------------------------------------------------------------------------------
CREATE TABLE Payslips(
    payslip_id INT PRIMARY KEY IDENTITY,
    month VARCHAR(100),
    year VARCHAR(100),
    emp_id INT references Employee(emp_id),
    filepath VARCHAR(100)
);
GO

SELECT * FROM Payslips;
go

CREATE PROCEDURE InsertPayslip
    @month VARCHAR(100),
    @year VARCHAR(100),
    @emp_id INT,
    @filepath VARCHAR(100)
AS
BEGIN
    INSERT INTO Payslips (month, year, emp_id, filepath)
    VALUES (@month, @year, @emp_id, @filepath);
END;
