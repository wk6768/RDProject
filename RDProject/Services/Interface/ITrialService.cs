﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        (long, string) SaveTrialPageAndReturnID(Trial trial, ObservableCollection<TrialEntry> trialEntries);

        /// <summary>
        /// 插入Trial表单和对应的明细,返回表单和对应的明细
        /// </summary>
        /// <param name="trial"></param>
        /// <param name="trialEntries"></param>
        /// <returns></returns>
        (Trial, ObservableCollection<TrialEntry>) SaveTrialPageAndReturnFullData(Trial trial, ObservableCollection<TrialEntry> trialEntries);

        /// <summary>
        /// 通过Trial的表头ID获取该表单完整内容
        /// </summary>
        /// <param name="fHeadId"></param>
        /// <returns></returns>
        (Trial, List<TrialEntry>) GetTrialFullData(long fHeadId);

        /// <summary>
        /// 通过用户名获取该用户所有表单
        /// </summary>
        /// <param name="createUser"></param>
        /// <returns></returns>
        List<Trial> GetTrialsByCreateUser(string createUser);
    }
}
