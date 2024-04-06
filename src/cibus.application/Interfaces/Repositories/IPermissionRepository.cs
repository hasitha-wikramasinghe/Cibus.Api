using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cibus.application.Interfaces.Repositories
{
    public interface IPermissionRepository
    {
        Task<IEnumerable<string>> GetPermissionsByUserIdAsync(int userId);
    }
}
