CREATE VIEW [dbo].[VwRolePermissions] AS 
SELECT 
RolePermission.RoleId, 
RolePermission.PermissionId, 
Role.RoleName,
Permission.PermissionName 
FROM [dbo].[RolePermission]
INNER JOIN [dbo].[Permission] ON RolePermission.PermissionId = Permission.Id AND RolePermission.EntityId = Permission.EntityId
INNER JOIN [dbo].[Role] ON RolePermission.RoleId = Role.Id AND RolePermission.EntityId = Role.EntityId;