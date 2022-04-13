using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RDProject.Models;

namespace RDProject.Services.Interface
{
    public interface IWFService
    {
        /// <summary>
        /// 保存审批和审批步骤
        /// </summary>
        /// <param name="instance"></param>
        (WFInstance instance, ObservableCollection<WFStep> steps) SaveInstance(WFInstance instance, ObservableCollection<WFStep> steps);

        /// <summary>
        /// 根据表名和表头ID获取审批表和审批步骤
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="HeadId"></param>
        /// <returns></returns>
        (WFInstance instance, ObservableCollection<WFStep> steps) GetInstanceByTableNameAndHeadID(string tableName, long HeadId);
    }
}
