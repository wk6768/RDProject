using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RDProject.Models;
using RDProject.Services.Interface;
using RDProject.Common;
using System.Collections.ObjectModel;
using RDProject.Models.VO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace RDProject.Services
{
    public class TrialService : ITrialService
    {
        private readonly MyDbContext ctx;
        private readonly ISerialNumberService serialNumberService;

        public TrialService(MyDbContext ctx, ISerialNumberService serialNumberService)
        {
            this.ctx = ctx;
            this.serialNumberService = serialNumberService;
        }

        public (long, string) SaveTrialPageAndReturnID(Trial trial, ObservableCollection<TrialEntry> trialEntries)
        {
            using (var tran = ctx.Database.BeginTransaction())
            {
                try
                {
                    var FBillNo = serialNumberService.GetFBillNo("Trial");
                    trial.FBillNo = FBillNo;

                    ctx.Trials.Add(trial);
                    ctx.SaveChanges();

                    foreach (TrialEntry trialEntry in trialEntries)
                    {
                        trialEntry.FHeadId = trial.FHeadId;
                    }
                    ctx.TrialEntries.AddRange(trialEntries);
                    ctx.SaveChanges();

                    tran.Commit();
                    return (trial.FHeadId, null);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    return (-1, ex.Message);
                }

            }

        }

        public (Trial, ObservableCollection<TrialEntry>) SaveTrialPageAndReturnFullData(Trial trial, ObservableCollection<TrialEntry> trialEntries)
        {
            using (var tran = ctx.Database.BeginTransaction())
            {
                try
                {
                    var FBillNo = serialNumberService.GetFBillNo("Trial");
                    trial.FBillNo = FBillNo;

                    ctx.Trials.Add(trial);
                    ctx.SaveChanges();

                    //trial.FTitle = trial.FTitle += $"-{trial.FHeadId}";

                    foreach (TrialEntry trialEntry in trialEntries)
                    {
                        trialEntry.FHeadId = trial.FHeadId;
                    }
                    ctx.TrialEntries.AddRange(trialEntries);
                    ctx.SaveChanges();

                    tran.Commit();
                    return (trial, trialEntries);
                }
                catch
                {
                    tran.Rollback();
                    return (null, null);
                }

            }
        }

        public (Trial, ObservableCollection<TrialEntry>) UpdateTrialPageAndReturnFullData(Trial trial, ObservableCollection<TrialEntry> trialEntries)
        {
            using (var tran = ctx.Database.BeginTransaction())
            {
                try
                {
                    ctx.Trials.Update(trial);
                    ctx.SaveChanges();

                    if(trialEntries != null)
                    {
                        foreach (TrialEntry trialEntry in trialEntries)
                        {
                            trialEntry.FHeadId = trial.FHeadId;
                        }
                        ctx.TrialEntries.UpdateRange(trialEntries);
                        ctx.SaveChanges();
                    }
                    
                    tran.Commit();
                    return (trial, trialEntries);
                }
                catch
                {
                    tran.Rollback();
                    return (null, null);
                }

            }
        }

        public (Trial, List<TrialEntry>) GetTrialFullData(long fHeadId)
        {
            var trial = ctx.Trials.Where(t => t.FHeadId == fHeadId).FirstOrDefault();
            var trialEnties = ctx.TrialEntries.Where(t => t.FHeadId == fHeadId).ToList();
            return (trial, trialEnties);
        }

        public List<TrialEntry> GetTrialEntryData(long fHeadId)
        {
            var trialEnties = ctx.TrialEntries.Where(t => t.FHeadId == fHeadId).ToList();
            return trialEnties;
        }


        public List<Trial> GetTrialsByCreateUser(string createUser)
        {
            return ctx.Trials.Where(t => t.FCreateUser == createUser).ToList();
        }

        public List<Trial> GetTrialsByTitle(string title)
        {
            return ctx.Trials.Where(t => t.FTitle.Contains(title)).ToList();
        }

        public List<MyTitle> GetMyTitleByCreateUser(string createUser)
        {
            return ctx.Trials.Where(t => t.FCreateUser == createUser).
                Select(t => new MyTitle() { FHeadId = t.FHeadId, FTitle = t.FTitle}).ToList();
        }

        public List<MyTitle> GetMyTitleByTitle(string title)
        {
            return ctx.Trials.Where(t => t.FTitle.Contains(title)).
                Select(t => new MyTitle() { FHeadId = t.FHeadId, FTitle = t.FTitle, FStatus = t.FStatus }).ToList();
        }

        public List<MyTitle> GetMyTitleByTitleAndStatus(string title, int status)
        {
            return ctx.Trials.Where(t => t.FStatus == status && t.FTitle.Contains(title)).
                Select(t => new MyTitle() { FHeadId = t.FHeadId, FTitle = t.FTitle, FStatus = t.FStatus }).ToList();
        }

        public int UpdateTrialEntry(TrialEntry trialEntry)
        {
            ctx.TrialEntries.Update(trialEntry);
            return ctx.SaveChanges();
        }

        public List<TrialReport> GetTrialReports(DateTime beginDate, DateTime endDate)
        {
            var begin = new SqlParameter("beginDate", beginDate);
            var end = new SqlParameter("endDate", endDate);
            var result = ctx.TrialReports.FromSqlRaw(
                @"select b.FDate,b.FBillNo,b.FRDNo,a.FWorkOrder,a.FStation,SUM(a.FAmount) FAmount,SUM(a.FManHours) FManHours,MIN(a.FBeginDate) FBeginDate,MAX(a.FEndDate) FEndDate from TrialEntry a
                left join Trial b on a.FHeadId = b.FHeadId 
                where b.FDate between @beginDate and @endDate 
                GROUP BY b.FDate,b.FBillNo,b.FRDNo,a.FWorkOrder,a.FStation", begin, end)
                .ToList();
            return result;
        }
    }
}
