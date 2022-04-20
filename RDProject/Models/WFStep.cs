using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;

namespace RDProject.Models
{
    public class WFStep : BindableBase
    {
        private long stepId;
        private long instanceId;
        private int stepNo;
        private string bookMark;
        private string subBy;
        private DateTime? subTime;
        private string remark;
        private int status;         //0未审批，1已审批，2已驳回
        

        public long StepId { get => stepId; set { stepId = value; RaisePropertyChanged(); } }
        public long InstanceId { get => instanceId; set { instanceId = value; RaisePropertyChanged(); } }
        public int StepNo { get => stepNo; set { stepNo = value; RaisePropertyChanged(); } }
        public string BookMark { get => bookMark; set { bookMark = value; RaisePropertyChanged(); } }
        public string Remark { get => remark; set { remark = value; RaisePropertyChanged(); } }
        public string SubBy { get => subBy; set { subBy = value; RaisePropertyChanged(); } }
        public DateTime? SubTime { get => subTime; set { subTime = value; RaisePropertyChanged(); } }
        public int Status { get => status; set { status = value; RaisePropertyChanged(); } }

        public override string ToString()
        {
            return $"{{{nameof(StepId)}={StepId.ToString()}, {nameof(InstanceId)}={InstanceId.ToString()}, {nameof(StepNo)}={StepNo.ToString()}, {nameof(BookMark)}={BookMark}, {nameof(Remark)}={Remark}, {nameof(SubBy)}={SubBy}, {nameof(SubTime)}={SubTime.ToString()}, {nameof(Status)}={Status.ToString()}}}";
        }
    }
}
