using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RDProject.Common;
using RDProject.Models;
using RDProject.Services.Interface;

namespace RDProject.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly MyDbContext ctx;

        public EmployeeService(MyDbContext ctx)
        {
            this.ctx = ctx;
        }

        public Employee GetLoginUser(Employee employee)
        {
            return ctx.Employees.Where(e => e.Id == employee.Id && e.Pwd == employee.Pwd && e.IsDeleted == false ).FirstOrDefault();
        }

        public async Task<List<Employee>> GetEmployeeList()
        {
            return await ctx.Employees.Where(e => e.IsDeleted == false).ToListAsync();
        }

        public async Task<int> AddEmployee(Employee employee)
        {
            ctx.Employees.Add(employee);
            return await ctx.SaveChangesAsync();
        }

        public int ChangePwd(Employee employee)
        {
            var result = ctx.Employees.Where(e => e.Id == employee.Id).FirstOrDefault();
            result.Pwd = employee.Pwd;
            return ctx.SaveChanges();
        }

        public async Task<List<Employee>> GetEmployeeListByName(string name)
        {
            return await ctx.Employees.Where(e => e.Name.Contains(name) == true).ToListAsync();
        }
    }
}
