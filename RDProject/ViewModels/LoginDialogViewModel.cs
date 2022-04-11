using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using Prism.Commands;
using RDProject.Services.Interface;
using RDProject.Models;

namespace RDProject.ViewModels
{
    public class LoginDialogViewModel : BindableBase, IDialogAware
    {
        private readonly IEmployeeService employeeService;

        public string Title => "用户登录";

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

        public LoginDialogViewModel(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
            Employee = new Employee();
            LoginCommand = new DelegateCommand(Login);
        }


        private Employee employee;

        public Employee Employee
        {
            get { return employee; }
            set { employee = value; RaisePropertyChanged(); }
        }

        private string message;

        public string Message
        {
            get { return message; }
            set { message = value; RaisePropertyChanged(); }
        }



        public DelegateCommand LoginCommand { get; private set; }

        private void Login()
        {
            var result = employeeService.GetLoginUser(Employee);

            if(result == null || string.IsNullOrWhiteSpace(result.Name))
            {
                Message = "用户名或密码错误";
                return;
            }
            var keys = new DialogParameters();
            keys.Add("User", result);
            RequestClose?.Invoke(new DialogResult(ButtonResult.OK, keys));
        }
    }
}
