using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using Prism.Commands;
using RDProject.Models;
using RDProject.Services.Interface;
using System.Collections.ObjectModel;

namespace RDProject.ViewModels
{
    public class EmployeeControlViewModel : BindableBase
    {
        public EmployeeControlViewModel(IEmployeeService employeeService)
        {
            Employee = new Employee();
            AddEmployeeCommand = new DelegateCommand(AddEmployee);
            GetEmployeeListCommand = new DelegateCommand(GetEmployeeList);
            this.employeeService = employeeService;
            GetEmployeeList();
        }

        private readonly IEmployeeService employeeService;

        private Employee employee;

        public Employee Employee
        {
            get { return employee; }
            set { employee = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Employee> employeeList;

        public ObservableCollection<Employee> EmployeeList
        {
            get { return employeeList; }
            set { employeeList = value; RaisePropertyChanged(); }
        }


        public DelegateCommand AddEmployeeCommand { get; private set; }
        public DelegateCommand GetEmployeeListCommand { get; private set; }

        private async void AddEmployee()
        {
            int i = await employeeService.AddEmployee(Employee);
            if (i > 0)
            {
                EmployeeList.Add(Employee);
                Employee = new Employee();
            }
                
        }

        private async void GetEmployeeList()
        {
            List<Employee> list = await employeeService.GetEmployeeList();
            EmployeeList = new ObservableCollection<Employee>(list);
        }

    }
}
