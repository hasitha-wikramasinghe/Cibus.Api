CREATE OR ALTER VIEW [dbo].[VwUserRoles] AS
SELECT 
UserRole.UserId UserId,
UserRole.RoleId RoleId,
Role.RoleName RoleName
FROM [dbo].[UserRole]
INNER JOIN [dbo].[Role] ON Role.Id = UserRole.RoleId;
GO