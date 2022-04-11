using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RDProject.Models;

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
        (long, string) SaveTrialForm(Trial trial, List<TrialEntry> trialEntries);

        /// <summary>
        /// 通过Trial的表头ID获取该表单完整内容
        /// </summary>
        /// <param name="fHeadId"></param>
        /// <returns></returns>
        (Trial, List<TrialEntry>) GetTrialFullData(long fHeadId);
    }
}
