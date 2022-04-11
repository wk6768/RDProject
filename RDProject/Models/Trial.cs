using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using Microsoft.EntityFrameworkCore;

namespace RDProject.Models
{
    public class Trial : BindableBase
    {
        private long fHeadId;
        private DateTime fDate;
        private string fBillNo;
        private string fRDNo;
        private string fProductName;
        private string fWorkerOrderDescription;
        private string fCompany;
        private bool fHasCNC;
        private bool fHasCoating;
        private bool fHasLaser;
        private bool fHasAssembly;
        private string fCNCNPI;
        private string fCoatingNPI;
        private string fLaserNPI;
        private string fAssemblyNPI;
        private string fAssemblyFactory;
        private string fInformation;
        private string fRequire;
        private DateTime fCreateDate;

        public long FHeadId { get => fHeadId; set { fHeadId = value; RaisePropertyChanged(); } }
        public DateTime FDate { get => fDate; set { fDate = value; RaisePropertyChanged(); } }
        public string FBillNo { get => fBillNo; set { fBillNo = value;RaisePropertyChanged(); } }
        public string FRDNo { get => fRDNo; set { fRDNo = value;RaisePropertyChanged(); } }
        public string FProductName { get => fProductName; set { fProductName = value;RaisePropertyChanged(); } }
        public string FWorkerOrderDescription { get => fWorkerOrderDescription; set { fWorkerOrderDescription = value;RaisePropertyChanged(); } }
        public string FCompany { get => fCompany; set { fCompany = value; RaisePropertyChanged(); } }
        public bool FHasCNC { get => fHasCNC; set { fHasCNC = value;RaisePropertyChanged(); } }
        public bool FHasCoating { get => fHasCoating; set { fHasCoating = value;RaisePropertyChanged(); } }
        public bool FHasLaser { get => fHasLaser; set { fHasLaser = value;RaisePropertyChanged(); } }
        public bool FHasAssembly { get => fHasAssembly; set { fHasAssembly = value;RaisePropertyChanged(); } }
        public string FCNCNPI { get => fCNCNPI; set { fCNCNPI = value;RaisePropertyChanged(); } }
        public string FCoatingNPI { get => fCoatingNPI; set { fCoatingNPI = value;RaisePropertyChanged(); } }
        public string FLaserNPI { get => fLaserNPI; set { fLaserNPI = value;RaisePropertyChanged(); } }
        public string FAssemblyNPI { get => fAssemblyNPI; set { fAssemblyNPI = value;RaisePropertyChanged(); } }
        public string FAssemblyFactory { get => fAssemblyFactory; set { fAssemblyFactory = value;RaisePropertyChanged(); } }
        public string FInformation { get => fInformation; set { fInformation = value;RaisePropertyChanged(); } }
        public string FRequire { get => fRequire; set { fRequire = value;RaisePropertyChanged(); } }
        public DateTime FCreateDate { get => fCreateDate; set { fCreateDate = value;RaisePropertyChanged(); } }

        public override string ToString()
        {
            return $"{{{nameof(FHeadId)}={FHeadId.ToString()}, {nameof(FDate)}={FDate.ToString()}, {nameof(FBillNo)}={FBillNo}, {nameof(FRDNo)}={FRDNo}, {nameof(FProductName)}={FProductName}, {nameof(FWorkerOrderDescription)}={FWorkerOrderDescription}, {nameof(FCompany)}={FCompany}, {nameof(FHasCNC)}={FHasCNC.ToString()}, {nameof(FHasCoating)}={FHasCoating.ToString()}, {nameof(FHasLaser)}={FHasLaser.ToString()}, {nameof(FHasAssembly)}={FHasAssembly.ToString()}, {nameof(FCNCNPI)}={FCNCNPI}, {nameof(FCoatingNPI)}={FCoatingNPI}, {nameof(FLaserNPI)}={FLaserNPI}, {nameof(FAssemblyNPI)}={FAssemblyNPI}, {nameof(FAssemblyFactory)}={FAssemblyFactory}, {nameof(FInformation)}={FInformation}, {nameof(FRequire)}={FRequire}, {nameof(FCreateDate)}={FCreateDate.ToString()}}}";
        }
    }
}
