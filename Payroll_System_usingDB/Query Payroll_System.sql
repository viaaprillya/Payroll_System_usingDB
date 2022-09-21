CREATE DATABASE Payroll_System

CREATE TABLE Position(
	PositionID varchar(10) NOT NULL PRIMARY KEY,
	Position_Name varchar(30),
	Basic_Salary int,
	Allowance int
);

CREATE TABLE Employee(
	EmployeeID int NOT NULL IDENTITY(1,1),
	Employee_Name varchar(30),
	Phone varchar(13) UNIQUE,
	Account_Number varchar(20) UNIQUE,
	PositionID varchar(10),
	PRIMARY KEY (EmployeeID),
	CONSTRAINT FK_EmployeePosition FOREIGN KEY (PositionID) REFERENCES Position(PositionID)
);

CREATE TABLE Overtime(
	OvertimeID int NOT NULL IDENTITY(1,1),
	EmployeeID int,
	Overtime_Date date,
	Overtime_Fee int,
	PRIMARY KEY (OvertimeID),
	CONSTRAINT FK_EmployeeOvertime FOREIGN KEY (EmployeeID) REFERENCES Employee(EmployeeID)
);

CREATE TABLE Bonus(
	BonusID int NOT NULL IDENTITY(1,1),
	EmployeeID int,
	Bonus_Date date,
	Bonus_Amount int,
	Bonus_Description varchar(30),
	PRIMARY KEY (BonusID),
	CONSTRAINT FK_EmployeeBonus FOREIGN KEY (EmployeeID) REFERENCES Employee(EmployeeID)
);

CREATE TABLE SalaryCut(
	SalaryCutID int NOT NULL IDENTITY(1,1),
	EmployeeID int,
	SalaryCut_Date date,
	SalaryCut_Amount int,
	SalaryCut_Description varchar(30),
	PRIMARY KEY (SalaryCutID),
	CONSTRAINT FK_EmployeeSalaryCut FOREIGN KEY (EmployeeID) REFERENCES Employee(EmployeeID)
);

CREATE TABLE Salary(
	SalaryID int NOT NULL IDENTITY(1,1),
	EmployeeID int,
	Salary_Date date,
	Basic_Salary int,
	Allowance int,
	Overtime_Total int,
	Bonus_Total int,
	SalaryCut_Total int,
	Salary_Total int
	PRIMARY KEY (SalaryID),
	CONSTRAINT FK_EmployeeSalary FOREIGN KEY (EmployeeID) REFERENCES Employee(EmployeeID)
);

INSERT INTO Position(PositionID, Position_Name, Basic_Salary, Allowance)
VALUES ('P1', 'CEO', '25000000', '5000000'),
('P2', 'Manager', '15000000', '3000000'),
('P3', 'Senior_Employee', '7500000', '1500000'),
('P4', 'Junior_Employee', '5000000', '1500000');
