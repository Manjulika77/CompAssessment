using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Model
{
    public class ValidateUserNameViewModels
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int RoleID { get; set; }
        public int OTP { get; set; }
        public DateTime LastPassChangeDate { get; set; }
        public bool isValidated { get; set; }
    }
}
