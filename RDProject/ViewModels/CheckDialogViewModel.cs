using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using Prism.Services.Dialogs;

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

        }
    }
}
