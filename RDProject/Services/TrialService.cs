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

namespace RDProject.Services
{
    public class TrialService : ITrialService
    {
        private readonly MyDbContext ctx;

        public TrialService(MyDbContext ctx)
        {
            this.ctx = ctx;
        }

        public (long, string) SaveTrialPageAndReturnID(Trial trial, ObservableCollection<TrialEntry> trialEntries)
        {
            using (var tran = ctx.Database.BeginTransaction())
            {
                try
                {
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
                    ctx.Trials.Add(trial);
                    ctx.SaveChanges();

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

                    ctx.TrialEntries.UpdateRange(trialEntries);
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

        public List<TrialTitle> GetTrialTitleByCreateUser(string createUser)
        {
            return ctx.Trials.Where(t => t.FCreateUser == createUser).
                Select(t => new TrialTitle() { FHeadId = t.FHeadId, FTitle = t.FTitle}).ToList();
        }

        public List<TrialTitle> GetTrialTitleByTitle(string title)
        {
            return ctx.Trials.Where(t => t.FTitle.Contains(title)).
                Select(t => new TrialTitle() { FHeadId = t.FHeadId, FTitle = t.FTitle }).ToList();
        }
    }
}
