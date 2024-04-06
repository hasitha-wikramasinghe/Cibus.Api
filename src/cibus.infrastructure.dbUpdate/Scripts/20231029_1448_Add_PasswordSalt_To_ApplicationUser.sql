ALTER TABLE dbo.ApplicationUser
	ADD PasswordSalt BINARY(1000) NOT NULL;
GO