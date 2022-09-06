using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Model
{
    public class ChangePasswordViewModels
    {
        public string UserName { get; set; }
        public string Old_Pass { get; set; }
        public string New_Pass { get; set; }
        public string Confirm_New_Pass { get; set; }
    }
}
