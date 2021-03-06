using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RDProject.Models;
using RDProject.Services.Interface;
using RDProject.Common;
using RDProject.Models.VO;
using Microsoft.EntityFrameworkCore;

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

        public async Task<List<MyTitle>> GetMyTitleByUserNameAsync(string userName, int status, string tableName)
        {
            //获取某人，某状态的流程
            switch (tableName)
            {
                case "Trial":
                    return await ctx.WFInstances.Join(ctx.Trials, i => i.HeadId, t => t.FHeadId, (i, t) => new { TableName = i.TableName, Status = i.Status, NextName = i.NextName, FHeadId = t.FHeadId, FTitle = t.FTitle })
                    .Where(b => b.TableName == tableName && b.Status == status && b.NextName == userName)
                    .Select(t => new MyTitle { FHeadId = t.FHeadId, FTitle = t.FTitle + "-" + t.FHeadId, FStatus = t.Status })
                    .ToListAsync();
                case "Manpower":
                    return await ctx.WFInstances.Join(ctx.Manpowers, i => i.HeadId, t => t.FHeadId, (i, t) => new { TableName = i.TableName, Status = i.Status, NextName = i.NextName, FHeadId = t.FHeadId, FTitle = t.FTitle })
                    .Where(b => b.TableName == tableName && b.Status == status && b.NextName == userName)
                    .Select(t => new MyTitle { FHeadId = t.FHeadId, FTitle = t.FTitle + "-" + t.FHeadId, FStatus = t.Status })
                    .ToListAsync();
                default:
                    return null;

            }
        }

        public async Task<List<MyTitle>> GetMyTitleByUserNameAsync(string userName, string tableName)
        {
            //获取与某人相关的所有流程
            //var InstanceIds = await ctx.WFSteps.Where(s => s.SubBy == userName).Select( s => s.InstanceId ).Distinct().ToListAsync();
            //return await ctx.WFInstances.Join(ctx.Trials, i => i.HeadId, t => t.FHeadId, (i, t) => new { TableName = i.TableName, InstanceId = i.InstanceId, Status = i.Status, FHeadId = t.FHeadId, FTitle = t.FTitle })
            //    .Where(b => b.TableName == tableName  && InstanceIds.Contains(b.InstanceId))
            //    .Select(t => new MyTitle { FHeadId = t.FHeadId, FTitle = t.FTitle + "-" + t.FHeadId, FStatus = t.Status })
            //    .ToListAsync();

            var InstanceIds = await ctx.WFSteps.Where(s => s.SubBy == userName).Select(s => s.InstanceId).Distinct().ToListAsync();
            switch (tableName)
            {
                case "Trial":
                    return await ctx.WFInstances.Join(ctx.Trials, i => i.HeadId, t => t.FHeadId, (i, t) => new { TableName = i.TableName, InstanceId = i.InstanceId, Status = i.Status, FHeadId = t.FHeadId, FTitle = t.FTitle })
                        .Where(b => b.TableName == tableName && InstanceIds.Contains(b.InstanceId))
                        .Select(t => new MyTitle { FHeadId = t.FHeadId, FTitle = t.FTitle + "-" + t.FHeadId, FStatus = t.Status })
                        .ToListAsync();
                case "Manpower":
                    return await ctx.WFInstances.Join(ctx.Manpowers, i => i.HeadId, t => t.FHeadId, (i, t) => new { TableName = i.TableName, InstanceId = i.InstanceId, Status = i.Status, FHeadId = t.FHeadId, FTitle = t.FTitle })
                        .Where(b => b.TableName == tableName && InstanceIds.Contains(b.InstanceId))
                        .Select(t => new MyTitle { FHeadId = t.FHeadId, FTitle = t.FTitle + "-" + t.FHeadId, FStatus = t.Status })
                        .ToListAsync();
                default:
                    return null;

            }
        }
    }
}
