CREATE TABLE DiningTable
(
	Id INT PRIMARY KEY IDENTITY(1000, 1) NOT NULL,
	TableNumber INT NULL,
	TableDescription VARCHAR(MAX) NULL,
	Capacity INT NULL,
	IsAvailable BIT NOT NULL DEFAULT 1,

	EntityId INT NULL,

	CONSTRAINT FK_DiningTable_Entity FOREIGN KEY (EntityId) REFERENCES Entity(Id)
);
