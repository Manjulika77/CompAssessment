using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Model
{
    public class CreateAssessmentViewModel
    {
        public string AssessmentName { get; set; }
        public string AssessmentDate { get; set; }
        public string AssessmentTime { get; set; }
        public int Duration { get; set; }
        public bool IsActive { get; set; }
    }
}
