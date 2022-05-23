using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Mvvm;
using RDProject.Services.Interface;
using RDProject.Models.VO;

namespace RDProject.ViewModels
{
    public class ManpowerStatisticsViewModel : BindableBase
    {
        public ManpowerStatisticsViewModel(IManpowerService manpowerService)
        {
            this.manpowerService = manpowerService;
            SearchCommand = new DelegateCommand(Search);
            ExportCommand = new DelegateCommand<object>(Export);

            var now = DateTime.Now;
            BeginDate = BeginDate.AddYears(now.Year - 1).AddMonths(now.Month - 1).AddDays(0);
            EndDate = EndDate.AddYears(now.Year - 1).AddMonths(now.Month).AddDays(0);
        }

       

        private readonly IManpowerService manpowerService;


        private DateTime beginDate;

        public DateTime BeginDate
        {
            get { return beginDate; }
            set { beginDate = value; RaisePropertyChanged(); }
        }

        private DateTime endDate;

        public DateTime EndDate
        {
            get { return endDate; }
            set { endDate = value; RaisePropertyChanged(); }
        }

        private string empId;

        public string EmpId
        {
            get { return empId; }
            set { empId = value; RaisePropertyChanged(); }
        }

        private string empName;

        public string EmpName
        {
            get { return empName; }
            set { empName = value; RaisePropertyChanged(); }
        }


        private List<ManpowerReport> reports;
        
        public List<ManpowerReport> Reports
        {
            get { return reports; }
            set { reports = value; RaisePropertyChanged(); }
        }


        public DelegateCommand SearchCommand { get; private set; }
        public DelegateCommand<object> ExportCommand { get; private set; }

        private void Search()
        {
            Reports = manpowerService.GetManpowerReports(BeginDate, EndDate, EmpId, EmpName);
        }

        private void Export(object obj)
        {
            var gridControl = obj as DevExpress.Xpf.Grid.GridControl;
            gridControl.View.ShowRibbonPrintPreview(gridControl);
        }
    }
}
