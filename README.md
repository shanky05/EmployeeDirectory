# EmployeeDirectory
Design Document

Design Document: Employee Directory with Tree Structure Visualization
Objective
Create an Employee Directory application that displays hierarchical relationships among employees in a tree structure. This application will allow users to view employees in both list and tree formats, and perform basic CRUD operations.
Features
1.	Hierarchical Data Storage
o	Use SQL Server to store employee data.
o	Implement hierarchical storage using either adjacency list or nested sets.
2.	Backend Implementation
o	Use .NET/.NET Core & C# for querying and serving data.
o	Implement basic CRUD operations for managing employee data.
o	Ensure that the repository pattern is used without Entity Framework.
3.	Frontend Visualization
o	Implement a user control/React component to visualize the hierarchy as a tree structure.
System Components
1. Database Design
•	Table: Employees
o	EmployeeID (int, Primary Key, Identity)
o	FirstName (nvarchar(50))
o	LastName (nvarchar(50))
o	ManagerID (int, Foreign Key, Nullable)
This schema uses an adjacency list model to represent hierarchical relationships, where ManagerID is a foreign key referring to EmployeeID in the same table.
2. Backend Implementation
2.1 Models
Employee Model
 
EmployeeTreeViewModel
 

2.2 Repository
IEmployeeRepository Interface
 

3. Data Model
CREATE TABLE Employees (
    EmployeeID INT PRIMARY KEY IDENTITY(1,1),
    FirstName NVARCHAR(50),
    LastName NVARCHAR(50),
    ManagerID INT NULL,
    FOREIGN KEY (ManagerID) REFERENCES Employees(EmployeeID)
);


4. Data Access Layer
4.1 Database Interaction
•	Use ADO.NET for direct SQL queries and data manipulation.
•	Implement repository pattern for data access.
4.2 CRUD Operations:
•	Create: Add new employee records and set their ManagerID.
•	Read: Retrieve hierarchical data to construct the tree structure.
•	Delete: Remove employee records and reassign or update hierarchy.

5. Frontend Visualization 
5.1 Razor view for List view page
5.2 Razor page for Tree view Page.
•	Creating a BuildHierarchy method to create hierarchy of employees using the recursive approach based on weather the selected employeeId is managerId for any records in the employee list.
•	Iterate over the hierarchy list using a foreach loop and create a node partial view for each node. This partial view displays the employee's name. If the employee has any subordinates, the partial view recursively calls itself to print the names of the subordinates. The process continues by checking if these subordinates have any further subordinates, and so on.

 
 



#SQL scripts

-- Create EmployeeDirectory Database

CREATE DATABASE EmployeeDirectory;

-- Creating Employee Table

CREATE TABLE Employees (
    EmployeeID INT PRIMARY KEY IDENTITY(1,1),
    FirstName NVARCHAR(50),
    LastName NVARCHAR(50),
    ManagerID INT NULL,
    FOREIGN KEY (ManagerID) REFERENCES Employees(EmployeeID)
);



-- Inserting sample data into Employees table
INSERT INTO Employees (FirstName, LastName, ManagerID) VALUES ('John', 'Doe', NULL);      -- EmployeeID 1, Top-level Manager
INSERT INTO Employees (FirstName, LastName, ManagerID) VALUES ('Jane', 'Smith', 1);      -- EmployeeID 2, Reporting to John Doe
INSERT INTO Employees (FirstName, LastName, ManagerID) VALUES ('Alice', 'Johnson', 1);    -- EmployeeID 3, Reporting to John Doe
INSERT INTO Employees (FirstName, LastName, ManagerID) VALUES ('Bob', 'Williams', 2);     -- EmployeeID 4, Reporting to Jane Smith
INSERT INTO Employees (FirstName, LastName, ManagerID) VALUES ('Charlie', 'Brown', 2);    -- EmployeeID 5, Reporting to Jane Smith
INSERT INTO Employees (FirstName, LastName, ManagerID) VALUES ('Eve', 'Davis', 3);        -- EmployeeID 6, Reporting to Alice Johnson
INSERT INTO Employees (FirstName, LastName, ManagerID) VALUES ('Frank', 'Miller', 3);      -- EmployeeID 7, Reporting to Alice Johnson
INSERT INTO Employees (FirstName, LastName, ManagerID) VALUES ('Grace', 'Wilson', 4);     -- EmployeeID 8, Reporting to Bob Williams
INSERT INTO Employees (FirstName, LastName, ManagerID) VALUES ('Hannah', 'Moore', 4);      -- EmployeeID 9, Reporting to Bob Williams
INSERT INTO Employees (FirstName, LastName, ManagerID) VALUES ('Ian', 'Taylor', 5);       -- EmployeeID 10, Reporting to Charlie Brown



