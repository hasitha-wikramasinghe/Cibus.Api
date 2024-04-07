Exec sp_rename 'dbo.ApplicationUser.HashedPassword', 'PasswordHash', 'COLUMN';
GO

ALTER TABLE dbo.ApplicationUser
	ALTER COLUMN PasswordSalt VARBINARY(MAX) NOT NULL;
GO

ALTER TABLE dbo.ApplicationUser		
	ALTER COLUMN PasswordHash VARBINARY(MAX) NOT NULL;
GO
