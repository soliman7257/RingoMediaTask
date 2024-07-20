## Setup

1. Dewonload the repository.
2. Install dependencies.
4. Set up your database and configure connection details.
    -migration (open Package Manager Console ) and run command 
     1-Add-Migration InitialCreate
     2-Update-Database
----------------------------------------------------------------------------------------------------------------------------------
## Departments Script Sql

INSERT INTO Departments (Name, Logo, ParentDepartmentId) VALUES 
('Human Resources', 'hr-logo.png', NULL),
('Engineering', 'eng-logo.png', NULL),
('Sales', 'sales-logo.png', NULL);

-- Insert sub-departments for 'Human Resources'
INSERT INTO Departments (Name, Logo, ParentDepartmentId) VALUES 
('Recruitment', 'recruitment-logo.png', (SELECT Id FROM Departments WHERE Name = 'Human Resources')),
('Employee Relations', 'employee-relations-logo.png', (SELECT Id FROM Departments WHERE Name = 'Human Resources'));

-- Insert sub-departments for 'Engineering'
INSERT INTO Departments (Name, Logo, ParentDepartmentId) VALUES 
('Software Engineering', 'software-engineering-logo.png', (SELECT Id FROM Departments WHERE Name = 'Engineering')),
('Hardware Engineering', 'hardware-engineering-logo.png', (SELECT Id FROM Departments WHERE Name = 'Engineering'));

-- Insert sub-departments for 'Software Engineering'
INSERT INTO Departments (Name, Logo, ParentDepartmentId) VALUES 
('Frontend Development', 'frontend-logo.png', (SELECT Id FROM Departments WHERE Name = 'Software Engineering')),
('Backend Development', 'backend-logo.png', (SELECT Id FROM Departments WHERE Name = 'Software Engineering'));

