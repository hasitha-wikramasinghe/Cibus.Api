﻿CREATE TABLE FoodOrder
(
	[Id] INT PRIMARY KEY IDENTITY(1000, 1) NOT NULL,
	[OrderDateTime] DATETIME NOT NULL,
	[TotalAmount] DECIMAL NOT NULL DEFAULT 0.00,

	[DiningTableId] INT FOREIGN KEY REFERENCES DiningTable(Id) NULL,
	[CustomerId] INT FOREIGN KEY REFERENCES Customer(Id) NULL,
	[OrderStatusId] INT FOREIGN KEY REFERENCES OrderStatusList(Id) NULL,
	[OrderPaymentStatusId] INT FOREIGN KEY REFERENCES OrderPaymentStatusList(Id) NULL,
	[OrderTypeId] INT FOREIGN KEY REFERENCES OrderTypeList(Id) NULL,
	[EntityId] INT NULL,

	CONSTRAINT FK_FoodOrder_Entity FOREIGN KEY (EntityId) REFERENCES Entity(Id)
);

