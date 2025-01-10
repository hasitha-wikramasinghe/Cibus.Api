CREATE TABLE Permission
(
	[Id] INT PRIMARY KEY IDENTITY(1000, 1) NOT NULL,
	[Name] VARCHAR(150) NOT NULL,
	[EntityId] INT NULL,

	[IsDeleted] BIT NOT NULL DEFAULT 0,
	[CreatedBy] INT NULL,
	[CreatedOn] DATE NULL,
	[ModifiedBy] INT NULL,
	[ModifiedOn] DATE NULL,

	CONSTRAINT FK_Permission_Entity FOREIGN KEY (EntityId) REFERENCES Entity(Id)
);