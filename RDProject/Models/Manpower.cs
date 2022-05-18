using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;

namespace RDProject.Models
{
    public class Manpower : BindableBase
    {
        private long fHeadId;
        private string fCreateUser;
        private DateTime fCreateDate;
        private string fTitle;
        private int fStatus;                        //0已保存、草稿、初始状态  1正在审批、审批完成  2驳回、废弃  3审批结束

        private DateTime fDate;
        private string fCompany;

        public long FHeadId { get => fHeadId; set { fHeadId = value; RaisePropertyChanged(); } }
        public string FCreateUser { get => fCreateUser; set { fCreateUser = value; RaisePropertyChanged(); } }
        public DateTime FCreateDate { get => fCreateDate; set { fCreateDate = value; RaisePropertyChanged(); } }
        public string FTitle { get => fTitle; set { fTitle = value; RaisePropertyChanged(); } }
        public int FStatus { get => fStatus; set { fStatus = value; RaisePropertyChanged(); } } 
        public DateTime FDate { get => fDate; set { fDate = value; RaisePropertyChanged(); } }
        public string FCompany { get => fCompany; set { fCompany = value; RaisePropertyChanged(); } }

        public override string ToString()
        {
            return $"{{{nameof(FHeadId)}={FHeadId.ToString()}, {nameof(FCreateUser)}={FCreateUser}, {nameof(FCreateDate)}={FCreateDate.ToString()}, {nameof(FTitle)}={FTitle}, {nameof(FStatus)}={FStatus.ToString()}, {nameof(FDate)}={FDate.ToString()}, {nameof(FCompany)}={FCompany}}}";
        }
    }
}
