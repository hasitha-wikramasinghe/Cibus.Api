﻿CREATE TABLE Customer
(
	Id INT PRIMARY KEY IDENTITY(1000, 1) NOT NULL,
	FirstName VARCHAR(150) NOT NULL,
	LastName VARCHAR(150) NULL,
	PhoneNumber VARCHAR(50) NOT NULL,
	NIC VARCHAR(100) NULL,
	Email VARCHAR(255) NULL,

	ClientId INT FOREIGN KEY REFERENCES Client(Id) NOT NULL
);
