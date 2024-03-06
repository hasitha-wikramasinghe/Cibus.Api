CREATE TABLE Client(
	Id INT PRIMARY KEY IDENTITY(1000, 1) NOT NULL,
	ClientName VARCHAR(150) NOT NULL,
	IsDeleted BIT NOT NULL DEFAULT 0,
	CreatedBy INT NULL,
	CreatedOn DATE NULL,
	ModifiedBy INT NULL,
	ModifiedOn DATE NULL,
);
GO