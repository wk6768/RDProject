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
        /// 保存审批步骤
        /// </summary>
        /// <param name="steps"></param>
        /// <returns></returns>
        void SaveWFSteps(ObservableCollection<WFStep> steps);
    }
}
