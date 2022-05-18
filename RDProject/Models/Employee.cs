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
        private string deptName;
        private string pwd;
        private string userGroup;
        private bool isDeleted;

        public string Id { get => id; set { id = value; RaisePropertyChanged(); } }
        public string Name { get => name; set{ name = value; RaisePropertyChanged(); } }
        public string DeptName { get => deptName; set { deptName = value;RaisePropertyChanged(); } }
        public string Pwd { get => pwd; set { pwd = value;RaisePropertyChanged(); } }
        public string UserGroup { get => userGroup; set { userGroup = value;RaisePropertyChanged(); } }
        public bool IsDeleted { get => isDeleted; set { isDeleted = value; RaisePropertyChanged(); } }

        public override string ToString()
        {
            return $"{{{nameof(Id)}={Id}, {nameof(Name)}={Name}, {nameof(DeptName)}={DeptName}, {nameof(Pwd)}={Pwd}, {nameof(UserGroup)}={UserGroup}, {nameof(IsDeleted)}={IsDeleted.ToString()}}}";
        }
    }
}
