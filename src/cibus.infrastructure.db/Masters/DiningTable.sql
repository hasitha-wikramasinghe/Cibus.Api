CREATE TABLE DiningTable
(
	[Id] INT PRIMARY KEY IDENTITY(1000, 1) NOT NULL,
	[Number] INT NULL,
	[Description] VARCHAR(MAX) NULL,
	[SeatingCapacity] INT NULL,
	[IsAvailable] BIT NOT NULL DEFAULT 1,

	[EntityId] INT NULL,

	CONSTRAINT FK_DiningTable_Entity FOREIGN KEY (EntityId) REFERENCES Entity(Id)
);
