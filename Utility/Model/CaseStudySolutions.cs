using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Model
{
    public class CaseStudySolutions
    {
        public int CSSID { get; set; }
        public string Solution { get; set; }
        public int CSID { get; set; }
        public int CompID { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
