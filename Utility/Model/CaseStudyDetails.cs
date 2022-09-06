using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Model
{
    public class CaseStudyDetails
    {
        public int CSID { get; set; }
        public string CaseStudy { get; set; }
        public int AssessmentID { get; set; }
        public bool ReviewStatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ReviewComment { get; set; }
    }
}
