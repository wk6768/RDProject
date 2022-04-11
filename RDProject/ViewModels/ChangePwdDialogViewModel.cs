using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using RDProject.Models;
using RDProject.Services.Interface;

namespace RDProject.ViewModels
{
    public class ChangePwdDialogViewModel : BindableBase, IDialogAware
    {

        public string Title => "修改密码";

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
            if (parameters.ContainsKey("User"))
            {
                User = parameters.GetValue<Employee>("User");
            }
        }


        public ChangePwdDialogViewModel(IEmployeeService employeeService)
        {
            Employee = new Employee();
            ChangePwdCommand = new DelegateCommand(ChangePwd);
            this.employeeService = employeeService;
        }

        private Employee employee;
        public Employee Employee
        {
            get { return employee; }
            set { employee = value; RaisePropertyChanged(); }
        }

        private Employee User;

        private string pwd1;
        public string Pwd1
        {
            get { return pwd1; }
            set { pwd1 = value; RaisePropertyChanged(); }
        }

        private string pwd2;
        public string Pwd2
        {
            get { return pwd2; }
            set { pwd2 = value; RaisePropertyChanged(); }
        }

        private string message;
        private readonly IEmployeeService employeeService;

        public string Message
        {
            get { return message; }
            set { message = value; RaisePropertyChanged(); }
        }
        public DelegateCommand ChangePwdCommand { get; private set; }

        private void ChangePwd()
        {
            if (!User.Id.Equals(Employee.Id) || !User.Pwd.Equals(Employee.Pwd))
            {
                Message = "用户名和密码不正确";
                return;
            }
            if (!Pwd1.Equals(Pwd2))
            {
                Message = "两次输入的密码不一致";
                return;
            }

            int result = employeeService.ChangePwd(new Employee { Id = User.Id, Pwd = Pwd1});
            if(result > 0)
            {
                var keys = new DialogParameters();
                keys.Add("Pwd", Pwd1);
                RequestClose?.Invoke(new DialogResult(ButtonResult.OK, keys));
            }
        }

    }
}
