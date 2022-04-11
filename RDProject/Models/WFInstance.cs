using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;

namespace RDProject.Models
{
    public class WFInstance : BindableBase
    {
        private long instanceId;
        private string tableName;
        private string instanceGuid;
        private bool instanceStatus;
        private long headId;
        private string subBy;
        private DateTime subTime;

        public long InstanceId { get => instanceId; set { instanceId = value; RaisePropertyChanged(); } }
        public string TableName { get => tableName; set { tableName = value; RaisePropertyChanged(); } }
        public string InstanceGuid { get => instanceGuid; set { instanceGuid = value; RaisePropertyChanged(); } }
        public bool InstanceStatus { get => instanceStatus; set { instanceStatus = value; RaisePropertyChanged(); } }
        public long HeadId { get => headId; set { headId = value; RaisePropertyChanged(); } }
        public string SubBy { get => subBy; set { subBy = value; RaisePropertyChanged(); } }
        public DateTime SubTime { get => subTime; set { subTime = value; RaisePropertyChanged(); } }

        public override string ToString()
        {
            return $"{{{nameof(InstanceId)}={InstanceId.ToString()}, {nameof(TableName)}={TableName}, {nameof(InstanceGuid)}={InstanceGuid}, {nameof(InstanceStatus)}={InstanceStatus.ToString()}, {nameof(HeadId)}={HeadId.ToString()}, {nameof(SubBy)}={SubBy}, {nameof(SubTime)}={SubTime.ToString()}}}";
        }
    }
}
