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
        (WFInstance instance, List<WFStep> steps) GetInstanceByTableNameAndHeadID(string tableName, long HeadId);

        /// <summary>
        /// 更新审批和审批步骤
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="steps"></param>
        /// <returns></returns>
        (WFInstance instance, ObservableCollection<WFStep> steps) UpdateInstance(WFInstance instance, ObservableCollection<WFStep> steps);

        /// <summary>
        /// 更新一个步骤
        /// </summary>
        /// <param name="step"></param>
        /// <returns></returns>
        int UpdateStep(WFStep step);

        /// <summary>
        /// 根据姓名和表单状态获取该用户待审批列表
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<List<MyTitle>> GetMyTitleByUserNameAsync(string userName, int status, string tableName);

        /// <summary>
        /// 根据姓名获取与该用户相关的所有审批列表
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<List<MyTitle>> GetMyTitleByUserNameAsync(string userName, string tableName);
        
    }
}
