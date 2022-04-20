using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using Prism.Commands;
using System.Collections.ObjectModel;
using RDProject.Models;

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
            if (parameters.ContainsKey("Steps"))
            {
                Steps = parameters.GetValue<ObservableCollection<WFStep>>("Steps");
            }
        }

        public CheckDialogViewModel()
        {
            Steps = new ObservableCollection<WFStep>();
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

        private WFStep step;
            
        public WFStep Step
        {
            get { return step; }
            set { step = value; RaisePropertyChanged(); }
        }


        private ObservableCollection<WFStep> steps;

        public ObservableCollection<WFStep> Steps
        {
            get { return steps; }
            set { steps = value; RaisePropertyChanged(); }
        }


        public DelegateCommand OkCommand { get; private set; }

        private void Ok()
        {
            var keys = new DialogParameters();
            keys.Add("CheckResultPass", CheckResultPass);
            keys.Add("CheckResultReject", CheckResultReject);
            keys.Add("Reason", Reason);
            if(Step != null)
            {
                keys.Add("Bookmark", Step.BookMark);
            }
            RequestClose?.Invoke(new DialogResult(ButtonResult.OK, keys));
        }
    }
}
