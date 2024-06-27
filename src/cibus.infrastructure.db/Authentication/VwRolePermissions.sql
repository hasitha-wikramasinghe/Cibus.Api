CREATE VIEW [dbo].[vwRolePermissions] AS 
SELECT 
RolePermission.RoleId, 
RolePermission.PermissionId, 
Role.RoleName,
Permission.PermissionName,
UserRole.UserId UserId
FROM [dbo].[RolePermission]
INNER JOIN [dbo].[UserRole] ON RolePermission.RoleId = UserRole.RoleId AND RolePermission.EntityId = UserRole.EntityId
INNER JOIN [dbo].[Permission] ON RolePermission.PermissionId = Permission.Id AND RolePermission.EntityId = Permission.EntityId
INNER JOIN [dbo].[Role] ON RolePermission.RoleId = Role.Id AND RolePermission.EntityId = Role.EntityId;