using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cibus.application.Interfaces.BusinessLogics
{
    public interface IPermissionBusinessLogic
    {
        Task<IEnumerable<string>> GetPermissionsByUserIdAsync(int userId);
    }
}
