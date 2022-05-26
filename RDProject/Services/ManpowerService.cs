using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RDProject.Models;
using RDProject.Services.Interface;
using RDProject.Common;
using RDProject.Models.VO;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace RDProject.Services
{
    public class ManpowerService : IManpowerService
    {
        private readonly MyDbContext ctx;

        public ManpowerService(MyDbContext ctx)
        {
            this.ctx = ctx;
        }

        public (Manpower, List<ManpowerEntry>) GetManpowerFullData(long fHeadId)
        {
            var manpower = ctx.Manpowers.Where(t => t.FHeadId == fHeadId).FirstOrDefault();
            var manpowerEnties = ctx.ManpowerEntries.Where(t => t.FHeadId == fHeadId).ToList();
            return (manpower, manpowerEnties);
        }

        public (Manpower, ObservableCollection<ManpowerEntry>) SaveManpowerPageAndReturnFullData(Manpower manpower, ObservableCollection<ManpowerEntry> manpowerEntries)
        {
            using(var tran = ctx.Database.BeginTransaction())
            {
                try
                {
                    ctx.Manpowers.Add(manpower);
                    ctx.SaveChanges();

                    foreach (var entry in manpowerEntries)
                    {
                        entry.FHeadId = manpower.FHeadId;
                    }

                    ctx.ManpowerEntries.AddRange(manpowerEntries);
                    ctx.SaveChanges();

                    tran.Commit();

                    return (manpower, manpowerEntries);
                }
                catch
                {
                    tran.Rollback();
                    return (null, null);
                }
                
            }
        }

        public (Manpower, ObservableCollection<ManpowerEntry>) UpdateManpowerPageAndReturnFullData(Manpower manpower, ObservableCollection<ManpowerEntry> manpowerEntries)
        {
            using(var tran = ctx.Database.BeginTransaction())
            {
                try
                {
                    ctx.Manpowers.Update(manpower);
                    ctx.SaveChanges();

                    if (manpowerEntries != null)
                    {
                        foreach (ManpowerEntry manpowerEntry in manpowerEntries)
                        {
                            manpowerEntry.FHeadId = manpower.FHeadId;
                        }
                        ctx.ManpowerEntries.UpdateRange(manpowerEntries);
                        ctx.SaveChanges();
                    }

                    tran.Commit();

                    return (manpower, manpowerEntries);
                }
                catch
                {
                    tran.Rollback();
                    return (null, null);
                }
                
            }
        }

        public List<ManpowerReport> GetManpowerReports(DateTime beginDate, DateTime endDate, string empId, string empName)
        {
            string sql = @"
select c.FDate ,c.FEmpId,c.FEmpName,c.FDeptName,
                SUM(c.FAttendanceHours) FAttendanceHours,SUM(c.FNormalOvertimeHours) FNormalOvertimeHours,SUM(c.FWeekendOvertimeHours) FWeekendOvertimeHours,
                SUM(c.FHolidayOvertimeHours) FHolidayOvertimeHours,SUM(c.FTotalHours) FTotalHours,SUM(c.FVarianceHours) FVarianceHours,
                SUM(c.FRD28Hours) FRD28Hours, SUM(c.FRD30Hours) FRD30Hours, SUM(c.FRD31Hours) FRD31Hours, SUM(c.FRD32Hours) FRD32Hours, 
                SUM(c.FRD33Hours) FRD33Hours, SUM(c.FRD34Hours) FRD34Hours
                from
(
select convert(varchar(7),b.FDate,111) FDate ,a.FEmpId,a.FEmpName,a.FDeptName,
               a.FAttendanceHours ,a.FNormalOvertimeHours,a.FWeekendOvertimeHours,
                a.FHolidayOvertimeHours ,a.FTotalHours,a.FVarianceHours,
                a.FRD28Hours, a.FRD30Hours, a.FRD31Hours, a.FRD32Hours, 
                a.FRD33Hours, a.FRD34Hours
                from ManpowerEntry a
                left join Manpower b on a.FHeadId = b.FHeadId 
                where b.FDate between @beginDate and @endDate and FEmpId like @empId and FDeptName like @empName
) 
c group by FDate,FEmpId,FEmpName,FDeptName";

            var begin = new SqlParameter("beginDate", beginDate);
            var end = new SqlParameter("endDate", endDate);
            var id = new SqlParameter("empId", string.IsNullOrWhiteSpace(empId) ? "%" : $"%{empId}%");
            var name = new SqlParameter("empName", string.IsNullOrWhiteSpace(empName) ? "%" : $"%{empName}%");

            var result = ctx.ManpowerReports.FromSqlRaw(sql, begin, end, id, name).ToList();
            return result;
        }

        public int UpdateManpowerEntry(ManpowerEntry manpowerEntry)
        {
            ctx.ManpowerEntries.Update(manpowerEntry);
            return ctx.SaveChanges();
        }

        public List<MyTitle> GetMyTitleByTitle(string title)
        {
            return ctx.Manpowers.Where(t => t.FTitle.Contains(title)).
                Select(t => new MyTitle() { FHeadId = t.FHeadId, FTitle = t.FTitle, FStatus = t.FStatus }).ToList();
        }

        public List<MyTitle> GetMyTitleByTitleAndStatus(string title, int status)
        {
            return ctx.Manpowers.Where(t => t.FStatus == status && t.FTitle.Contains(title)).
                Select(t => new MyTitle() { FHeadId = t.FHeadId, FTitle = t.FTitle, FStatus = t.FStatus }).ToList();
        }
    }
}
