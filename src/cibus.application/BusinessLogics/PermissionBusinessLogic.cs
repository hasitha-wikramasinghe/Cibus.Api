using cibus.application.Interfaces.BusinessLogics;
using cibus.application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cibus.application.BusinessLogics
{
    public class PermissionBusinessLogic : IPermissionBusinessLogic
    {
        private readonly IPermissionRepository _permissionRepository;
        public PermissionBusinessLogic(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }
        public Task<IEnumerable<string>> GetPermissionsByUserIdAsync(int userId)
        {
            return _permissionRepository.GetPermissionsByUserIdAsync(userId);
        }
    }
}
