using Dapper;
using cibus.application.Interfaces.Context;
using cibus.application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cibus.application.Authentication
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly IDapperContext _context;
        public PermissionRepository(IDapperContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<string>> GetPermissionsByUserIdAsync(int userId)
        {
            var getPermissionNamesByUserId = AuthenticationScripts.GetPermissionNamesByUserId;
            using (var connection = _context.CreateConnection())
            {
                var permissions = await connection.QueryAsync<string>(getPermissionNamesByUserId,
                    new
                    {
                        userId
                    });
                return permissions;
            }
        }
    }
}
