using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cibus.domain.Entities
{
    public class Permission
    {
        public int Id { get; set; }
        public string PermissionName { get; set; }

        public int? EntityId { get; set; }
    }
}
