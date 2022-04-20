using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using Prism.Commands;
using Prism.Regions;
using Prism.Services.Dialogs;
using RDProject.Models;
using System.Diagnostics;

namespace RDProject.ViewModels
{
    public class MainViewModel : BindableBase
    {
        public MainViewModel(IRegionManager regionManager, IDialogService dialogService)
        {
            this.regionManager = regionManager;
            this.dialogService = dialogService;

            OpenLoginDialogCommand = new DelegateCommand<string>(OpenLoginDialog);
            ChangePwdCommand = new DelegateCommand<string>(ChangePwd);
            //LogoutCommand = new DelegateCommand(() => User = null);
            LogoutCommand = new DelegateCommand<string>(Logout);

            NavigateEmployeeControlCommand = new DelegateCommand<string>(NavigateEmployeeControl);
            NavigateTrialFormCommand = new DelegateCommand<string>(NavigateTrialForm);
            NavigateMyFormCommand = new DelegateCommand<string>(NavigateMyForm);

        }

        

        private readonly IRegionManager regionManager;
        private readonly IDialogService dialogService;

        private Employee user;

        public Employee User
        {
            get { return user; }
            set { user = value; RaisePropertyChanged(); }
        }

        public DelegateCommand<string> OpenLoginDialogCommand { get; private set; }
        public DelegateCommand<string> ChangePwdCommand { get; private set; }
        public DelegateCommand<string> LogoutCommand { get; private set; }

        public DelegateCommand<string> NavigateEmployeeControlCommand { get; private set; }
        public DelegateCommand<string> NavigateTrialFormCommand { get; private set; }
        public DelegateCommand<string> NavigateMyFormCommand { get; private set; }


        private void OpenLoginDialog(string obj)
        {
            if (User == null || string.IsNullOrEmpty(User.Name))
            {
                dialogService.ShowDialog(obj, callback =>
                {
                    if(callback.Result == ButtonResult.OK)
                        User = callback.Parameters.GetValue<Employee>("User");
                });
            }
        }

        private void ChangePwd(string obj)
        {
            if(User != null && !string.IsNullOrEmpty(User.Name))
            {
                var keys = new DialogParameters();
                keys.Add("User", User);
                dialogService.ShowDialog(obj, keys, callback =>
                {
                    if(callback.Result == ButtonResult.OK)
                    {
                        User.Pwd = callback.Parameters.GetValue<string>("Pwd");
                    }
                });
            }
        }

        private void Logout(string obj)
        {
            User = null;
            regionManager.Regions["MainControl"].RequestNavigate(obj);
        }

        private void NavigateEmployeeControl(string obj)
        {
            regionManager.Regions["MainControl"].RequestNavigate(obj);
        }

        private void NavigateTrialForm(string obj)
        {
            if (User != null && !string.IsNullOrEmpty(User.Name))
            {
                var keys = new NavigationParameters();
                keys.Add("User", User);
                keys.Add("FHeadID", (long)-1);
                regionManager.Regions["MainControl"].RequestNavigate(obj, keys);
            }
        }

        private void NavigateMyForm(string obj)
        {
            if (User != null && !string.IsNullOrEmpty(User.Name))
            {
                var keys = new NavigationParameters();
                keys.Add("User", User);
                regionManager.Regions["MainControl"].RequestNavigate(obj, keys);
            }
        }
    }
}
