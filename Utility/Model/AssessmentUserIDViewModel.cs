using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Model
{
    public class AssessmentUserIDViewModel
    {
        public int AssessmentID { get; set; }
        public List<AssessmentDetails> assessmentList { get; set; }
        
        public List<UserIDViewModel> userIDList { get; set; }
    }
}
