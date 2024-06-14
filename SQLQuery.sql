--use master;
go
-- alter database Task1 set single_user with rollback immediate

-- drop database Task1

Create database Task1;
GO

USE Task1;
GO

DROP TABLE Employee;
CREATE TABLE Employee(
	emp_id int primary key identity,
    ename varchar(100),
	email varchar(100) UNIQUE,
    passkey varchar(100),
	contact decimal(10,0),
	doj VARCHAR(20),
    erole VARCHAR(20)
);
GO

SELECT * FROM Employee;
Go

CREATE PROCEDURE AddEmployee
    @Name VARCHAR(100),
    @Email VARCHAR(100),
    @Passkey varchar(100),
    @Contact DECIMAL(10,0),
    @DOJ DATETIME,
    @Role VARCHAR(100)
AS
BEGIN
    INSERT INTO Employee (ename,email,passkey, contact, doj,erole)
    VALUES (@Name,@Email,@Passkey, @Contact, @DOJ,@Role);
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

exec GetEmployeeById 1
go
-----------------------------------------------

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
-------------------------------------------------------------------------------------------------------------------------------
------------------------------------------------------------------ AUTH -------------------------------------------------------
CREATE PROC AuthLogin
@uid varchar(100),
@passkey varchar(100)
as
begin
Select * from Employee where (email=@uid or ename = @uid) and passkey=@passkey;
end
Go

EXEC AuthLogin 'naqifahijabu@rungel.net','naqifahijabu@rungel';
-------------------------------------------------------------------------------------------------------------------------------------
DROP TABLE LeaveApplication;
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
SELECT * FROM LeaveApplication;
GO

exec GetCountOfLeaves 1 ,6, 2024;
go

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

create PROCEDURE GetCountOfLeaves
    @EmployeeId INT,
    @month INT,
    @year INT
    AS BEGIN
        -- Set NOCOUNT ON to prevent extra result sets from interfering with SELECT statements.
        SET NOCOUNT ON;

        -- Select the count of leaves for the given employee ID.
        SELECT SUM(countofleaves) AS countofleaves
        FROM LeaveApplication
        WHERE emp_id = @EmployeeId AND month=@month AND year=@year AND leave_status='Approved';
    END;
GO

exec GetCountOfLeaves 1 ,6, 2024;
go

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
--drop table OfferLetters;
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

--DROP table Payslips;

CREATE PROCEDURE InsertOrUpdatePayslip
    @Month INT,
    @Year INT,
    @Empid INT,
    @Filepath NVARCHAR(MAX)
AS
BEGIN
    -- Check if a record already exists for the given month, year, and employee ID
    IF EXISTS (
        SELECT 1
        FROM Payslips
        WHERE month = @Month AND year = @Year AND emp_id = @Empid
    )
    BEGIN
        -- Update the existing record
        UPDATE Payslips
        SET filepath = @Filepath
        WHERE month = @Month AND year = @Year AND emp_id = @Empid;
    END
    ELSE
    BEGIN
        -- Insert a new record
        INSERT INTO Payslips (month, year, emp_id, filepath)
        VALUES (@Month, @Year, @Empid, @Filepath);
    END
END;
go
------------------------------------------------------------------ TICKET -----------------------------------------------------------
CREATE TABLE RaiseTicket(
    ticket_id int primary key identity,
    raised_to int references Employee(emp_id),
    raised_by int references Employee(emp_id),
    ticket varchar(max),
    attachment varchar(max),
    ticket_date DATETIME DEFAULT GETDATE()
);
go

SELECT * FROM RaiseTicket;
GO
CREATE PROCEDURE CreateRaiseTicket
    @raised_to INT,
    @raised_by INT,
    @ticket VARCHAR(MAX),
    @attachment VARCHAR(MAX)
AS
BEGIN
    INSERT INTO RaiseTicket (raised_to, raised_by, ticket, attachment)
    VALUES (@raised_to, @raised_by, @ticket, @attachment);
END;
go

CREATE PROCEDURE GetEmployeesByRole
    @role NVARCHAR(50)
AS
BEGIN
    SELECT emp_id, ename 
    FROM Employee 
    WHERE erole = @role;
END;
-----------------------------------------------------------Solution-----------------------
CREATE TABLE Solution(
    solution_id int primary key identity,
    ticket_id int references RaiseTicket(ticket_id),
    ticket_solution varchar(max),
    solution_date DATETIME DEFAULT GETDATE()
);
go
CREATE PROCEDURE AddTicketSolution
    @ticket_id int,
    @ticket_solution varchar(max)
AS
BEGIN
    INSERT INTO Solution (ticket_id, ticket_solution, solution_date)
    VALUES (@ticket_id, @ticket_solution, GETDATE());
END;