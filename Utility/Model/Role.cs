using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Model
{
    public class Role
    {
        public int RoleID { get; set; }

        public string RoleName { get; set; }

        public bool isActive { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
