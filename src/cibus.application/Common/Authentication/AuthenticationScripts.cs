using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cibus.application.Common.Authentication
{
    public static class AuthenticationScripts
    {
        public const string GetAllApplicationUsersQuery = "SELECT * FROM dbo.ApplicationUser";
        public const string GetApplicationUserByIdQuery = "SELECT * FROM dbo.ApplicationUser WHERE Id = @Id";
        public const string GetApplicationUserByEmailQuery = "SELECT * FROM dbo.ApplicationUser WHERE Email = @Email";
        public const string GetUserRolesByUserIdQuery = "SELECT * FROM dbo.vwUserRoles WHERE UserId = @UserId";

        public const string CreateUserCommand = "INSERT INTO dbo.ApplicationUser (Email, PasswordHash, FirstName, LastName, NIC, DOB, CreatedBy, CreatedOn, ModifiedBy, ModifiedOn, ClientId, PasswordSalt) " +
                "VALUES (@Email, @PasswordHash, @FirstName, @LastName, @NIC, @DOB, @CreatedBy, @CreatedOn, @ModifiedBy, @ModifiedOn, @ClientId, @PasswordSalt)" +
                "SELECT CAST(SCOPE_IDENTITY() AS INT)";
    }
}
