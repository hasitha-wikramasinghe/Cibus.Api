CREATE VIEW [dbo].[VwRolePermissions] AS 
SELECT 
UserRole.UserId,
RolePermission.RoleId, 
RolePermission.PermissionId, 
Permission.PermissionName 
FROM [dbo].[RolePermission]
INNER JOIN [dbo].[Permission] ON RolePermission.PermissionId = Permission.id
INNER JOIN [dbo].[UserRole] ON RolePermission.RoleId = UserRole.RoleId;