using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Model
{
    public class AssessmentDetails
    {
        public int AssessmentID {get; set;}

        public string AssessmentName { get; set; }

        public DateTime AssessmentDate { get; set; }

        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public int DurationMin { get; set; }
        public bool IsActive { get; set; }
    }
}
