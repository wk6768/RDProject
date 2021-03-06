using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;

namespace RDProject.Models.VO 
{
    public class MyTitle : BindableBase
    {
        private long fHeadId;
        private string fTitle;
        private int fStatus;

        public long FHeadId { get => fHeadId; set { fHeadId = value; RaisePropertyChanged(); } }
        public string FTitle { get => fTitle; set { fTitle = value; RaisePropertyChanged(); } }
        public int FStatus { get => fStatus; set { fStatus = value; RaisePropertyChanged(); } }

        public override string ToString()
        {
            return $"{{{nameof(FHeadId)}={FHeadId.ToString()}, {nameof(FTitle)}={FTitle}, {nameof(FStatus)}={FStatus.ToString()}}}";
        }
    }
}
