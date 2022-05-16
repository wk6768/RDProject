using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDProject.Models
{
    public class SerialNumber
    {
        private int fId;
        private string fTableName;
        private int fYear;
        private int fMonth;
        private int fNo;

        public int FId { get => fId; set => fId = value; }
        public string FTableName { get => fTableName; set => fTableName = value; }
        public int FYear { get => fYear; set => fYear = value; }
        public int FMonth { get => fMonth; set => fMonth = value; }
        public int FNo { get => fNo; set => fNo = value; }

        public override string ToString()
        {
            return $"{{{nameof(FId)}={FId.ToString()}, {nameof(FTableName)}={FTableName}, {nameof(FYear)}={FYear.ToString()}, {nameof(FMonth)}={FMonth.ToString()}, {nameof(FNo)}={FNo.ToString()}}}";
        }
    }
}
