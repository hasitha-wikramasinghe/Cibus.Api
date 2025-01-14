CREATE TABLE OrderPaymentStatusList
(
	[Id] INT PRIMARY KEY IDENTITY(1000, 1) NOT NULL,
	[Name] VARCHAR(150) NOT NULL,

	[EntityId] INT NULL,

	CONSTRAINT FK_OrderPaymentStatusList_Entity FOREIGN KEY (EntityId) REFERENCES Entity(Id)
);
