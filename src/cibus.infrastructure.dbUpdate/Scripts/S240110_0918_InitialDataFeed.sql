Insert Into Entity ([Name]) values ('Cibus');
Go

Insert Into ApplicationUser (Email, PasswordHash, PasswordSalt, FirstName, LastName, NIC, DOB, EntityId) 
Select 
'hasitha.wikramasinghe@gmail.com', 
CONVERT(VARBINARY(MAX), '0x64CCD09FB911311CFDAFC2974C264CDE9F7C9428CEC6F8AD511FB080040C5299C8FE3F6EF9B7429A6FB24E7E139872FC1C86305D93F14A9FBAF2F1162A2519F6', 1), 
CONVERT(VARBINARY(MAX), '0xC5090DDF38FF9803DBA7053986983E8A624C7099611B760FDBC5AA1DA8E02E89F959D3FB7CB9E6A4856468C74FC25A0083E5C93C0542FCBE9A44BC4642A318EDF3666F4477F3464D749A68165C0D254611F20A21D7940A2770E8B15954166E7E495DB66582A94DC710F139DC055E296034C813919C036BB54784AB40F01F2DC5', 1),
'Hasitha', 
'Wikramasinghe', 
'980223175V', 
'1998-01-22', 
Id
From Entity Where [Name] = 'Cibus';
Go

CREATE TABLE #TempPermissions (
	Id INT,
    PermissionName VARCHAR(255)
);
Go

INSERT INTO #TempPermissions (Id, PermissionName)
VALUES 
('1000', 'AccessUser'),
('1001', 'WriteUser'),
('1002', 'ReadUser'),
('1003', 'UpdateUser'),
('1004', 'DeleteUser');
Go

SET IDENTITY_INSERT Permission ON;
Insert Into Permission ([Id], [Name], [EntityId]) 
Select TP.Id, TP.PermissionName, U.EntityId
From #TempPermissions As TP
Cross Join ApplicationUser As U
Where U.Email = 'hasitha.wikramasinghe@gmail.com';
Go

SET IDENTITY_INSERT Permission OFF;

Drop Table #TempPermissions;
Go

CREATE TABLE #TempRoles (
	Id INT,
    RoleName VARCHAR(255)
);
Go

INSERT INTO #TempRoles ([Id], [RoleName])
VALUES 
('1000', 'SuperAdmin'),
('1001', 'Manager'),
('1002', 'Receptionist');
Go

SET IDENTITY_INSERT [dbo].[Role] ON;

Insert Into [dbo].[Role] ([Id], [Name], [EntityId]) 
Select TR.Id, TR.RoleName, U.EntityId
From #TempRoles As TR
Cross Join ApplicationUser As U
Where U.Email = 'hasitha.wikramasinghe@gmail.com';
Go

SET IDENTITY_INSERT [dbo].[Role] OFF;

Drop Table #TempRoles;
Go

CREATE TABLE #TempUserRole (
    RoleId INT
);
Go

INSERT INTO #TempUserRole (RoleId)
VALUES 
(1000),
(1001),
(1002);
Go

Insert Into [dbo].[UserRole] (UserId, RoleId, EntityId) 
Select U.Id, TUR.RoleId, U.EntityId
From #TempUserRole As TUR
Cross Join ApplicationUser As U
Where U.Email = 'hasitha.wikramasinghe@gmail.com';
Go

Drop Table #TempUserRole;
Go

-- Initially giving AccessUser permission to default user
Insert Into [dbo].[RolePermission] (RoleId, PermissionId, EntityId) 
Select 1000, 1000, U.EntityId
From ApplicationUser As U
Where U.Email = 'hasitha.wikramasinghe@gmail.com';
Go