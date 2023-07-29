use PearTask

-- Create Suppliers Table
Create Table Suppliers (
	SupplierID int primary key identity(1,1),
	SupplierName varchar(50) NOT NULL
);

--create Products table
Create Table Products(
	ProductID int primary key identity(1,1),
    ProductName varchar(50) NOT NULL,
    QuantityPerUnit varchar(50),
    ReorderLevel int,
    SupplierID int foreign key references Suppliers(SupplierID),
    UnitPrice decimal(10,2),
    UnitsInStock int,
    UnitsOnOrder int
);



