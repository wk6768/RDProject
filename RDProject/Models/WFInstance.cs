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
        private int status;             //0已保存、草稿、初始状态  1正在审批、审批完成  2驳回  3审批结束
        private long headId;
        private string subBy;
        private string nextName;
        private DateTime subTime;

        public long InstanceId { get => instanceId; set { instanceId = value; RaisePropertyChanged(); } }
        public string TableName { get => tableName; set { tableName = value; RaisePropertyChanged(); } }
        public string InstanceGuid { get => instanceGuid; set { instanceGuid = value; RaisePropertyChanged(); } }
        public int Status { get => status; set { status = value; RaisePropertyChanged(); } }
        public long HeadId { get => headId; set { headId = value; RaisePropertyChanged(); } }
        public string SubBy { get => subBy; set { subBy = value; RaisePropertyChanged(); } }
        public DateTime SubTime { get => subTime; set { subTime = value; RaisePropertyChanged(); } }
        public string NextName { get => nextName; set { nextName = value; RaisePropertyChanged(); } }

        public override string ToString()
        {
            return $"{{{nameof(InstanceId)}={InstanceId.ToString()}, {nameof(TableName)}={TableName}, {nameof(InstanceGuid)}={InstanceGuid}, {nameof(Status)}={Status.ToString()}, {nameof(HeadId)}={HeadId.ToString()}, {nameof(SubBy)}={SubBy}, {nameof(SubTime)}={SubTime.ToString()}, {nameof(NextName)}={NextName}}}";
        }
    }
}
