using cibus.application.Interfaces.BusinessLogics;
using cibus.application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cibus.application.BusinessLogics
{
    public class PermissionBL : IPermissionBL
    {
        private readonly IPermissionRepository _permissionRepo;
        public PermissionBL(IPermissionRepository permissionRepo)
        {
            _permissionRepo = permissionRepo;
        }
        public Task<IEnumerable<string>> GetPermissionsByUserIdAsync(int userId)
        {
            return _permissionRepo.GetPermissionsByUserIdAsync(userId);
        }
    }
}
