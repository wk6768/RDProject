using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RDProject.Models;
using RDProject.Services.Interface;
using RDProject.Common;

namespace RDProject.Services
{
    public class WFService : IWFService
    {
        private readonly MyDbContext ctx;

        public WFService(MyDbContext ctx)
        {
            this.ctx = ctx;
        }



        public (WFInstance instance, ObservableCollection<WFStep> steps) SaveInstance(WFInstance instance, ObservableCollection<WFStep> steps)
        {
            using(var tran = ctx.Database.BeginTransaction())
            {
                try
                {
                    ctx.WFInstances.Add(instance);
                    ctx.SaveChanges();

                    foreach(var step in steps)
                    {
                        step.InstanceId = instance.InstanceId;
                    }
                    ctx.WFSteps.AddRange(steps);
                    ctx.SaveChanges();

                    tran.Commit();
                    return (instance, steps);
                }
                catch
                {
                    return (null, null);
                }
            }
        }

        public (WFInstance instance, List<WFStep> steps) GetInstanceByTableNameAndHeadID(string tableName, long headId)
        {
            var instance = ctx.WFInstances.Where(i => i.TableName == tableName && i.HeadId == headId).FirstOrDefault();
            if (instance == null)
            {
                //有表单，但是没有启用审批流
                return (null, null);
            }
            var steps = ctx.WFSteps.Where(s => s.InstanceId == instance.InstanceId).OrderBy(s => s.StepNo).ToList();
            return (instance, steps);
        }

        public (WFInstance instance, ObservableCollection<WFStep> steps) UpdateInstance(WFInstance instance, ObservableCollection<WFStep> steps)
        {
            using (var tran = ctx.Database.BeginTransaction())
            {
                try
                {
                    ctx.WFInstances.Update(instance);
                    ctx.SaveChanges();

                    ctx.WFSteps.UpdateRange(steps);
                    ctx.SaveChanges();

                    tran.Commit();
                    return (instance, steps);
                }
                catch
                {
                    return (null, null);
                }
            }
        }

        public int UpdateStep(WFStep step)
        {
            ctx.WFSteps.Update(step);
            return ctx.SaveChanges();
        }
    }
}
