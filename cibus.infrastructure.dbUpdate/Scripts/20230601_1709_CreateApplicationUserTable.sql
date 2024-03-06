CREATE TABLE ApplicationUser(
	Id INT PRIMARY KEY IDENTITY(1000, 1) NOT NULL,
	Email VARCHAR(150) NOT NULL,
	HashedPassword BINARY(1000) NOT NULL,
	FirstName VARCHAR(50) NULL,
	LastName VARCHAR(50) NULL,
	NIC VARCHAR(30) NULL,
	DOB DATE NULL,
	IsDeleted BIT NOT NULL DEFAULT 0,
	CreatedBy INT NULL,
	CreatedOn DATE NULL,
	ModifiedBy INT NULL,
	ModifiedOn DATE NULL,

	ClientId INT FOREIGN KEY REFERENCES Client(Id)
);
GO