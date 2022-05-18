using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;

namespace RDProject.Models
{
    public class ManpowerEntry : BindableBase
    {
        private long fEntryId;
        private long fHeadId;
        private DateTime fCreateDate;

        private string fEmpId;
        private string fEmpName;
        private string fDeptName;
        private decimal fAttendanceHours;
        private decimal fNormalOvertimeHours;
        private decimal fWeekendOvertimeHours;
        private decimal fHolidayOvertimeHours;
        private decimal fTotalHours;
        private decimal fVarianceHours;
        private decimal fRD28Hours;
        private decimal fRD30Hours;
        private decimal fRD31Hours;
        private decimal fRD32Hours;
        private decimal fRD33Hours;
        private decimal fRD34Hours;

        public long FEntryId { get => fEntryId; set { fEntryId = value; RaisePropertyChanged(); } }
        public long FHeadId { get => fHeadId; set { fHeadId = value; RaisePropertyChanged(); } }
        public DateTime FCreateDate { get => fCreateDate; set { fCreateDate = value; RaisePropertyChanged(); } }
        public string FEmpId { get => fEmpId; set { fEmpId = value; RaisePropertyChanged(); } }
        public string FEmpName { get => fEmpName; set { fEmpName = value; RaisePropertyChanged(); } }
        public string FDeptName { get => fDeptName; set { fDeptName = value; RaisePropertyChanged(); } }
        public decimal FAttendanceHours { get => fAttendanceHours; set { fAttendanceHours = value; RaisePropertyChanged(); } }
        public decimal FNormalOvertimeHours { get => fNormalOvertimeHours; set { fNormalOvertimeHours = value; RaisePropertyChanged(); } }
        public decimal FWeekendOvertimeHours { get => fWeekendOvertimeHours; set { fWeekendOvertimeHours = value; RaisePropertyChanged(); } }
        public decimal FHolidayOvertimeHours { get => fHolidayOvertimeHours; set { fHolidayOvertimeHours = value; RaisePropertyChanged(); } }
        public decimal FTotalHours { get => fTotalHours; set { fTotalHours = value; RaisePropertyChanged(); } }
        public decimal FVarianceHours { get => fVarianceHours; set { fVarianceHours = value; RaisePropertyChanged(); } }
        public decimal FRD28Hours { get => fRD28Hours; set { fRD28Hours = value; RaisePropertyChanged(); } }
        public decimal FRD30Hours { get => fRD30Hours; set { fRD30Hours = value; RaisePropertyChanged(); } }
        public decimal FRD31Hours { get => fRD31Hours; set { fRD31Hours = value; RaisePropertyChanged(); } }
        public decimal FRD32Hours { get => fRD32Hours; set { fRD32Hours = value; RaisePropertyChanged(); } }
        public decimal FRD33Hours { get => fRD33Hours; set { fRD33Hours = value; RaisePropertyChanged(); } }
        public decimal FRD34Hours { get => fRD34Hours; set { fRD34Hours = value; RaisePropertyChanged(); } }

        public override string ToString()
        {
            return $"{{{nameof(FEntryId)}={FEntryId.ToString()}, {nameof(FHeadId)}={FHeadId.ToString()}, {nameof(FCreateDate)}={FCreateDate.ToString()}, {nameof(FEmpId)}={FEmpId}, {nameof(FEmpName)}={FEmpName}, {nameof(FDeptName)}={FDeptName}, {nameof(FAttendanceHours)}={FAttendanceHours.ToString()}, {nameof(FNormalOvertimeHours)}={FNormalOvertimeHours.ToString()}, {nameof(FWeekendOvertimeHours)}={FWeekendOvertimeHours.ToString()}, {nameof(FHolidayOvertimeHours)}={FHolidayOvertimeHours.ToString()}, {nameof(FTotalHours)}={FTotalHours.ToString()}, {nameof(FVarianceHours)}={FVarianceHours.ToString()}, {nameof(FRD28Hours)}={FRD28Hours.ToString()}, {nameof(FRD30Hours)}={FRD30Hours.ToString()}, {nameof(FRD31Hours)}={FRD31Hours.ToString()}, {nameof(FRD32Hours)}={FRD32Hours.ToString()}, {nameof(FRD33Hours)}={FRD33Hours.ToString()}, {nameof(FRD34Hours)}={FRD34Hours.ToString()}}}";
        }
    }
}
