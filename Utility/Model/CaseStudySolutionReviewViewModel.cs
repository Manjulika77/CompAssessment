using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Model
{
    public class CaseStudySolutionReviewViewModel
    {
        public int CSID { get; set; }
        public string CaseStudy { get; set; }
        public string CreatedBy { get; set; }
        public List<CaseStudySolutionComptViewModel> CaseSdyCompt { get; set; }
}
}
