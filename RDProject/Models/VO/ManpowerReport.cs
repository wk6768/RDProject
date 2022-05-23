using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDProject.Models.VO
{
    public class ManpowerReport
    {
        public string FDate { get; set; }
        public string FEmpId { get; set; }
        public string FEmpName { get; set; }
        public string FDeptName { get; set; }
        public decimal FAttendanceHours { get; set; }
        public decimal FNormalOvertimeHours { get; set; }
        public decimal FWeekendOvertimeHours { get; set; }
        public decimal FHolidayOvertimeHours { get; set; }
        public decimal FTotalHours { get; set; }
        public decimal FVarianceHours { get; set; }
        public decimal FRD28Hours { get; set; }
        public decimal FRD30Hours { get; set; }
        public decimal FRD31Hours { get; set; }
        public decimal FRD32Hours { get; set; }
        public decimal FRD33Hours { get; set; }
        public decimal FRD34Hours { get; set; }

        public override string ToString()
        {
            return $"{{{nameof(FDate)}={FDate.ToString()}, {nameof(FEmpId)}={FEmpId}, {nameof(FEmpName)}={FEmpName}, {nameof(FDeptName)}={FDeptName}, {nameof(FAttendanceHours)}={FAttendanceHours.ToString()}, {nameof(FNormalOvertimeHours)}={FNormalOvertimeHours.ToString()}, {nameof(FWeekendOvertimeHours)}={FWeekendOvertimeHours.ToString()}, {nameof(FHolidayOvertimeHours)}={FHolidayOvertimeHours.ToString()}, {nameof(FTotalHours)}={FTotalHours.ToString()}, {nameof(FVarianceHours)}={FVarianceHours.ToString()}, {nameof(FRD28Hours)}={FRD28Hours.ToString()}, {nameof(FRD30Hours)}={FRD30Hours.ToString()}, {nameof(FRD31Hours)}={FRD31Hours.ToString()}, {nameof(FRD32Hours)}={FRD32Hours.ToString()}, {nameof(FRD33Hours)}={FRD33Hours.ToString()}, {nameof(FRD34Hours)}={FRD34Hours.ToString()}}}";
        }
    }
}
