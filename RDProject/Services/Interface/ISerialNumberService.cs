using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDProject.Services.Interface
{
    public interface ISerialNumberService
    {
        string GetFBillNo(string TableName);
    }
}
