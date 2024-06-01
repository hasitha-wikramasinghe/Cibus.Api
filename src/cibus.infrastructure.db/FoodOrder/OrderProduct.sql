﻿CREATE TABLE OrderProduct
(
	Id INT PRIMARY KEY IDENTITY(1000, 1) NOT NULL,
	Quantity INT NOT NULL,
	UnitPrice DECIMAL NOT NULL,
	SubTotal DECIMAL NOT NULL,

	OrderId INT FOREIGN KEY REFERENCES FoodOrder(Id) NOT NULL,
	ProductId INT FOREIGN KEY REFERENCES Product(Id) NOT NULL,
	EntityId INT NULL,

	CONSTRAINT FK_OrderProduct_Entity FOREIGN KEY (EntityId) REFERENCES Entity(Id)
)
