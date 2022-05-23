using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RDProject.Models;
using RDProject.Models.VO;

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

        /// <summary>
        /// 通过Manpower的表头ID获取该表单完整内容
        /// </summary>
        /// <param name="fHeadId"></param>
        /// <returns></returns>
        (Manpower, List<ManpowerEntry>) GetManpowerFullData(long fHeadId);


        /// <summary>
        /// 获取制定日期范围内某人的人力工时统计表
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        List<ManpowerReport> GetManpowerReports(DateTime beginDate, DateTime endDate, string empId, string empName);

    }
}
