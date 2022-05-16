using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RDProject.Services.Interface;
using RDProject.Common;

namespace RDProject.Services
{
    public class SerialNumberService : ISerialNumberService
    {
        private readonly MyDbContext ctx;

        public SerialNumberService(MyDbContext ctx)
        {
            this.ctx = ctx;
        }

        public string GetFBillNo(string TableName)
        {
            var serial = ctx.SerialNumbers.Where(s => s.FTableName == TableName).FirstOrDefault();
            
            var now = DateTime.Now;
            var year = now.Year;
            var month = now.Month;

            if(serial.FYear == year && serial.FMonth != month)
            {
                serial.FMonth = month;
                serial.FNo = 1;
            }
            if(serial.FYear != year && serial.FMonth != month)
            {
                serial.FYear = year;
                serial.FMonth = month;
                serial.FNo = 1;
            }

            var FBillNo = $"{serial.FYear + string.Format("{0:00}", serial.FMonth) + string.Format("{0:00000}", serial.FNo)}";

            serial.FNo += 1;
            ctx.SaveChanges();

            return FBillNo;
        }
    }
}
