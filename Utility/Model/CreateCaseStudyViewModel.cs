using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Model
{
    public class CreateCaseStudyViewModel
    {
        public int AssessmentID { get; set; }
        public string caseStudy { get; set; }        
        public string caseStudySolutions_1 { get; set; }
        public string caseStudySolutions_2 { get; set; }
        public string caseStudySolutions_3 { get; set; }
        public string caseStudySolutions_4 { get; set; }
        public int CompID_1 { get; set; }
        public int CompID_2 { get; set; }
        public int CompID_3 { get; set; }
        public int CompID_4 { get; set; }
        public string CreatedBy { get; set; }
        public List<AssessmentDetails> assessmentDetails { get; set; }
        public List<CompetencyDetails> competencyDetails { get; set; }
    }
}
