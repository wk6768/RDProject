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
    public interface ITrialService
    {
        /// <summary>
        /// 插入Trial表单和对应的明细,返回表头ID和error message
        /// </summary>
        /// <param name="trial"></param>
        /// <param name="trialEntries"></param>
        /// <returns></returns>
        (long, string) SaveTrialPageAndReturnID(Trial trial, ObservableCollection<TrialEntry> trialEntries);

        /// <summary>
        /// 插入Trial表单和对应的明细,返回表单和对应的明细
        /// </summary>
        /// <param name="trial"></param>
        /// <param name="trialEntries"></param>
        /// <returns></returns>
        (Trial, ObservableCollection<TrialEntry>) SaveTrialPageAndReturnFullData(Trial trial, ObservableCollection<TrialEntry> trialEntries);

        /// <summary>
        /// 更新Trial表单和对应的明细,返回表单和对应的明细
        /// </summary>
        /// <param name="trial"></param>
        /// <param name="trialEntries"></param>
        /// <returns></returns>
        (Trial, ObservableCollection<TrialEntry>) UpdateTrialPageAndReturnFullData(Trial trial, ObservableCollection<TrialEntry> trialEntries);

        /// <summary>
        /// 通过Trial的表头ID获取该表单完整内容
        /// </summary>
        /// <param name="fHeadId"></param>
        /// <returns></returns>
        (Trial, List<TrialEntry>) GetTrialFullData(long fHeadId);

        /// <summary>
        /// 通过Trial的表头ID获取该表单明细
        /// </summary>
        /// <param name="fHeadId"></param>
        /// <returns></returns>
        List<TrialEntry> GetTrialEntryData(long fHeadId);

        /// <summary>
        /// 通过用户名获取该用户所有表单
        /// </summary>
        /// <param name="createUser"></param>
        /// <returns></returns>
        List<Trial> GetTrialsByCreateUser(string createUser);

        /// <summary>
        /// 通过标题模糊查询表单
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        List<Trial> GetTrialsByTitle(string title);

        /// <summary>
        /// 通过用户名获取该用户所有表单的ID和标题
        /// </summary>
        /// <param name="createUser"></param>
        /// <returns></returns>
        List<MyTitle> GetMyTitleByCreateUser(string createUser);

        /// <summary>
        /// 通过标题获取所有复符合条件的表单的ID和标题
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        List<MyTitle> GetMyTitleByTitle(string title);

        /// <summary>
        /// 通过标题和表单状态获取所有复符合条件的表单的ID和标题
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        List<MyTitle> GetMyTitleByTitleAndStatus(string title, int status);

        /// <summary>
        /// 更新一条明细
        /// </summary>
        /// <param name="trialEntry"></param>
        /// <returns></returns>
        int UpdateTrialEntry(TrialEntry trialEntry);

        /// <summary>
        /// 查询某时间区间的报表信息
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        List<TrialReport> GetTrialReports(DateTime beginDate, DateTime endDate);
    }
}
