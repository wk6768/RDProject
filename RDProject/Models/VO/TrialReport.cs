using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDProject.Models.VO
{
    public class TrialReport
    {
        public DateTime FDate { get; set; }
        public string FBillNo { get; set; }
        public string FRDNo { get; set; }
        public string FWorkOrder { get; set; }
        public string FStation { get; set; }
        public int FAmount { get; set; }
        public decimal FManHours { get; set; }
        public DateTime FBeginDate { get; set; }
        public DateTime FEndDate { get; set; }
    }
}
