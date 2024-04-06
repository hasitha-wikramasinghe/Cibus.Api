using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cibus.infrastructure.Authentication
{
    public enum Permission
    {
        AccessUser = 1000,
        WriteUser = 1001,
        ReadUser = 1002,
        UpdateUser = 1003,
        DeleteUser = 1004
    }
}
