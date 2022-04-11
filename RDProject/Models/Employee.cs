using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;

namespace RDProject.Models
{
    public class Employee : BindableBase
    {
        private string id;
        private string name;
        private string empName;
        private string pwd;
        private string userGroup;
        private bool isDeleted;

        public string Id { get => id; set { id = value; RaisePropertyChanged(); } }
        public string Name { get => name; set{ name = value; RaisePropertyChanged(); } }
        public string EmpName { get => empName; set { empName = value;RaisePropertyChanged(); } }
        public string Pwd { get => pwd; set { pwd = value;RaisePropertyChanged(); } }
        public string UserGroup { get => userGroup; set { userGroup = value;RaisePropertyChanged(); } }
        public bool IsDeleted { get => isDeleted; set { isDeleted = value; RaisePropertyChanged(); } }
    }
}
