using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Model
{
    public class ForgotPasswordViewModels
    {
        public string UserName { get; set; }

        public int OTP { get; set; }

        public string New_Password { get; set; }
    }
}
