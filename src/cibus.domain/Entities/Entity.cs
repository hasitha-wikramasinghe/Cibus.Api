using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cibus.domain.Entities
{
    public class Entity
    {
        public int Id { get; set; }
        public string EntityName { get; set; }
        public string Currency { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int DeletedBy { get; set; }
        public DateTime DeletedOn { get; set; }

        public List<ApplicationUser> ApplicationUsers { get; set; } = new List<ApplicationUser>();
    }
}
