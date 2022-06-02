using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using Prism.Commands;
using RDProject.Models;
using System.Windows;
using DevExpress.Xpf.WindowsUI;

namespace RDProject.ViewModels
{
    public class TrialAddDialogViewModel : BindableBase, IDialogAware
    {
        public string Title => "新建项";

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {

        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            
        }

        public TrialAddDialogViewModel()
        {
            TrialEntry = new TrialEntry() { FBeginDate=DateTime.Now, FEndDate = DateTime.Now };
            TrialEntries = new List<TrialEntry>();
            AddCommand = new DelegateCommand(Add);
            BackCommand = new DelegateCommand(Back);
            CancelCommand = new DelegateCommand(Cancel);
        }



        private TrialEntry trialEntry;

        public TrialEntry TrialEntry
        {
            get { return trialEntry; }
            set { trialEntry = value; RaisePropertyChanged(); }
        }

        private List<TrialEntry> trialEntries;

        public List<TrialEntry> TrialEntries
        {
            get { return trialEntries; }
            set { trialEntries = value; RaisePropertyChanged(); }
        }


        public DelegateCommand AddCommand { get; private set; }
        public DelegateCommand BackCommand { get; private set; }
        public DelegateCommand CancelCommand { get; private set; }

        private void Add()
        {
            //计算工时
            var timeSpan = TrialEntry.FEndDate - TrialEntry.FBeginDate;
            if(timeSpan.TotalHours <= 0)
            {
                MessageBox.Show("请重新选择投入/结束时间");
                return;
            }
            var hour = (decimal)Math.Round(timeSpan.TotalHours, 1, MidpointRounding.AwayFromZero);
            TrialEntry.FManHours = TrialEntry.FManPower * hour;

            TrialEntries.Add(TrialEntry);
            TrialEntry = new TrialEntry() { FBeginDate = DateTime.Now, FEndDate = DateTime.Now };
        }

        private void Back()
        {
            var result = WinUIMessageBox.Show("是否保存已添加的内容并返回（未添加内容会舍弃）", "提示", MessageBoxButton.OKCancel, MessageBoxImage.Warning, MessageBoxResult.None, MessageBoxOptions.None);
            if (result == MessageBoxResult.OK)
            {
                var keys = new DialogParameters();
                keys.Add("TrialEntries", TrialEntries);
                RequestClose?.Invoke(new DialogResult(ButtonResult.OK, keys));
            }
        }

        private void Cancel()
        {
            var result = WinUIMessageBox.Show("是否放弃保存", "提示", MessageBoxButton.OKCancel, MessageBoxImage.Warning, MessageBoxResult.None, MessageBoxOptions.None);
            if (result == MessageBoxResult.OK)
            {
                RequestClose?.Invoke(new DialogResult(ButtonResult.Cancel));
            }
        }
    }
}
