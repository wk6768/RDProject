using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RDProject.Models;

namespace RDProject.Services.Interface
{
    public interface IManpowerService
    {
        /// <summary>
        /// 插入Manpower表单和对应的明细,返回表单和对应的明细
        /// </summary>
        /// <param name="manpower"></param>
        /// <param name="manpowerEntries"></param>
        /// <returns></returns>
        (Manpower, ObservableCollection<ManpowerEntry>) SaveManpowerPageAndReturnFullData(Manpower manpower , ObservableCollection<ManpowerEntry> manpowerEntries);

        /// <summary>
        /// 更新Manpower表单和对应的明细,返回表单和对应的明细
        /// </summary>
        /// <param name="manpower"></param>
        /// <param name="manpowerEntries"></param>
        /// <returns></returns>
        (Manpower, ObservableCollection<ManpowerEntry>) UpdateManpowerPageAndReturnFullData(Manpower manpower, ObservableCollection<ManpowerEntry> manpowerEntries);
    }
}
