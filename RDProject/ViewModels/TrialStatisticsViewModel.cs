using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using Prism.Commands;
using Prism.Navigation;
using RDProject.Services.Interface;
using RDProject.Models.VO;
using Microsoft.Win32;

namespace RDProject.ViewModels
{
    public class TrialStatisticsViewModel : BindableBase
    {
        private readonly ITrialService trialService;

        public TrialStatisticsViewModel(ITrialService trialService)
        {
            this.trialService = trialService;
            SearchCommand = new DelegateCommand(Search);
            ExportCommand = new DelegateCommand<object>(Export);

            var now = DateTime.Now;
            BeginDate = BeginDate.AddYears(now.Year - 1).AddMonths(now.Month - 1).AddDays(0);
            EndDate = EndDate.AddYears(now.Year - 1).AddMonths(now.Month).AddDays(0);
        }

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

        private List<TrialReport> reports;

        public List<TrialReport> Reports
        {
            get { return reports; }
            set { reports = value; RaisePropertyChanged(); }
        }


        public DelegateCommand SearchCommand { get; private set; }
        public DelegateCommand<object> ExportCommand { get; private set; }

        private void Search()
        {
            Reports = trialService.GetTrialReports(BeginDate, EndDate);
        }

        private void Export(object obj)
        {
            //var gridControl = obj as DevExpress.Xpf.Grid.GridControl;
            //gridControl.View.ShowPrintPreview(gridControl);
            //gridControl.View.ExportToXlsx(@"D:\123.xlsx");
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "xlsx(*.xlsx)|*.xlsx";
            if(saveFileDialog.ShowDialog() == true)
            {
                var path = saveFileDialog.FileName;
            }
        }
    }
}
