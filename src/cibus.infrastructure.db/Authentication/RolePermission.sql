CREATE TABLE RolePermission
(
	[RoleId] INT FOREIGN KEY REFERENCES Role(Id) NOT NULL,
	[PermissionId] INT FOREIGN KEY REFERENCES Permission(Id) NOT NULL,
	[EntityId] INT NULL,

	[IsDeleted] BIT NOT NULL DEFAULT 0,
	[CreatedBy] INT NULL,
	[CreatedOn] DATE NULL,
	[ModifiedBy] INT NULL,
	[ModifiedOn] DATE NULL,

	CONSTRAINT PK_RolePermission PRIMARY KEY(RoleId, PermissionId),
	CONSTRAINT FK_RolePermission_Entity FOREIGN KEY (EntityId) REFERENCES Entity(Id)
);