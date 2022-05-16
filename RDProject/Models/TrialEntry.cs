using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;

namespace RDProject.Models
{
    public class TrialEntry : BindableBase
    {
        private long fEntryId;
        private long fHeadId;
        private string fWorkOrder;
        private string fStation;
        private string fProcessName;
        private DateTime fBeginDate;
        private DateTime fEndDate;
        private int fAmount;
        private decimal fManPower;
        private decimal fManHours;
        private DateTime fCreateDate;

        public long FEntryId { get => fEntryId; set { fEntryId = value; RaisePropertyChanged(); } }
        public long FHeadId { get => fHeadId; set { fHeadId = value; RaisePropertyChanged(); } }
        public string FWorkOrder { get => fWorkOrder; set { fWorkOrder = value; RaisePropertyChanged(); } }
        public string FProcessName { get => fProcessName; set { fProcessName = value; RaisePropertyChanged(); } }
        public DateTime FBeginDate { get => fBeginDate; set { fBeginDate = value; RaisePropertyChanged(); } }
        public DateTime FEndDate { get => fEndDate; set { fEndDate = value; RaisePropertyChanged(); } }
        public int FAmount { get => fAmount; set { fAmount = value; RaisePropertyChanged(); } }
        public decimal FManPower { get => fManPower; set { fManPower = value; RaisePropertyChanged(); } }
        public decimal FManHours { get => fManHours; set { fManHours = value; RaisePropertyChanged(); } }
        public DateTime FCreateDate { get => fCreateDate; set { fCreateDate = value; RaisePropertyChanged(); } }
        public string FStation { get => fStation; set { fStation = value; RaisePropertyChanged(); } }

        public override string ToString()
        {
            return $"{{{nameof(FEntryId)}={FEntryId.ToString()}, {nameof(FHeadId)}={FHeadId.ToString()}, {nameof(FWorkOrder)}={FWorkOrder}, {nameof(FProcessName)}={FProcessName}, {nameof(FBeginDate)}={FBeginDate.ToString()}, {nameof(FEndDate)}={FEndDate.ToString()}, {nameof(FAmount)}={FAmount.ToString()}, {nameof(FManPower)}={FManPower.ToString()}, {nameof(FManHours)}={FManHours.ToString()}, {nameof(FCreateDate)}={FCreateDate.ToString()}, {nameof(FStation)}={FStation}}}";
        }
    }
}
