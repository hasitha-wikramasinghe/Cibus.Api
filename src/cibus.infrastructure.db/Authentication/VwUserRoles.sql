﻿CREATE VIEW [dbo].[vwUserRoles] AS
SELECT 
UserRole.UserId UserId,
UserRole.RoleId RoleId,
Role.Name RoleName
FROM [dbo].[UserRole]
INNER JOIN [dbo].[Role] ON Role.Id = UserRole.RoleId AND UserRole.EntityId = Role.EntityId;