using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DevExpress.Xpf.WindowsUI;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using RDProject.Models;

namespace RDProject.ViewModels
{
    public class ManpowerAddDialogViewModel : BindableBase, IDialogAware
    {
        public string Title => "新增明细";

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

        public ManpowerAddDialogViewModel()
        {
            ManpowerEntry = new ManpowerEntry();
            ManpowerEntries = new List<ManpowerEntry>();
            AddCommand = new DelegateCommand(Add);
            BackCommand = new DelegateCommand(Back);
            CancelCommand = new DelegateCommand(Cancel);
        }

        public DelegateCommand AddCommand { get; private set; }
        public DelegateCommand BackCommand { get; private set; }
        public DelegateCommand CancelCommand { get; private set; }

        private ManpowerEntry manpowerEntry;

        public ManpowerEntry ManpowerEntry
        {
            get { return manpowerEntry; }
            set { manpowerEntry = value; RaisePropertyChanged(); }
        }

        private List<ManpowerEntry> manpowerEntries;

        public List<ManpowerEntry> ManpowerEntries
        {
            get { return manpowerEntries; }
            set { manpowerEntries = value; RaisePropertyChanged(); }
        }


        private void Add()
        {
            ManpowerEntry.FTotalHours = ManpowerEntry.FAttendanceHours + ManpowerEntry.FNormalOvertimeHours 
                + ManpowerEntry.FWeekendOvertimeHours + ManpowerEntry.FHolidayOvertimeHours;
            ManpowerEntry.FVarianceHours = ManpowerEntry.FTotalHours - ManpowerEntry.FRD28Hours - ManpowerEntry.FRD30Hours
                - ManpowerEntry.FRD31Hours - ManpowerEntry.FRD32Hours - ManpowerEntry.FRD33Hours - ManpowerEntry.FRD34Hours;
            ManpowerEntries.Add(ManpowerEntry);
            ManpowerEntry = new ManpowerEntry();
        }

        private void Back()
        {
            var result = WinUIMessageBox.Show("是否保存已添加的内容并返回（未添加内容会舍弃）", "提示", MessageBoxButton.OKCancel, MessageBoxImage.Warning, MessageBoxResult.None, MessageBoxOptions.None);
            if (result == MessageBoxResult.OK)
            {
                var keys = new DialogParameters();
                keys.Add("ManpowerEntries", ManpowerEntries);
                RequestClose?.Invoke(new DialogResult(ButtonResult.OK, keys));
            }
        }

        private void Cancel()
        {
            var result = WinUIMessageBox.Show("是否放弃保存", "提示", MessageBoxButton.OKCancel, MessageBoxImage.Warning, MessageBoxResult.None, MessageBoxOptions.None);
            if(result == MessageBoxResult.OK)
            {
                RequestClose?.Invoke(new DialogResult(ButtonResult.Cancel));
            }
        }
    }
}
