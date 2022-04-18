using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using Prism.Commands;

namespace RDProject.ViewModels
{
    public class CheckDialogViewModel : BindableBase, IDialogAware
    {
        public string Title => "审批";

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

        public CheckDialogViewModel()
        {
            OkCommand = new DelegateCommand(Ok);
        }

        private bool checkResultPass;

        public bool CheckResultPass
        {
            get { return checkResultPass; }
            set { checkResultPass = value; RaisePropertyChanged(); }
        }

        private bool checkResultReject;

        public bool CheckResultReject
        {
            get { return checkResultReject; }
            set { checkResultReject = value; RaisePropertyChanged(); }
        }

        private string reason;

        public string Reason
        {
            get { return reason; }
            set { reason = value; RaisePropertyChanged(); }
        }



        public DelegateCommand OkCommand { get; private set; }

        private void Ok()
        {
            var keys = new DialogParameters();
            keys.Add("CheckResult", CheckResultPass);
            keys.Add("Reason", Reason);
            RequestClose?.Invoke(new DialogResult(ButtonResult.OK, keys));
        }
    }
}
