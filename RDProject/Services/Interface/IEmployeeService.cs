using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RDProject.Models;

namespace RDProject.Services.Interface
{
    public interface IEmployeeService
    {
        /// <summary>
        /// 获取用户基本信息
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        Employee GetLoginUser(Employee employee);

        /// <summary>
        /// 获取所有用户列表
        /// </summary>
        /// <returns></returns>
        Task<List<Employee>> GetEmployeeList();

        /// <summary>
        /// 插入用户
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        Task<int> AddEmployee(Employee employee);

        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        int ChangePwd(Employee employee);

        /// <summary>
        /// 根据姓名获取用户列表
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<List<Employee>> GetEmployeeListByName(string name);
    }
}
