﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RDProject.Models;
using RDProject.Services.Interface;
using RDProject.Common;

namespace RDProject.Services
{
    public class TrialService : ITrialService
    {
        private readonly MyDbContext ctx;

        public TrialService(MyDbContext ctx)
        {
            this.ctx = ctx;
        }

        public (long,string) SaveTrialForm(Trial trial, List<TrialEntry> trialEntries)
        {
            using(var tran = ctx.Database.BeginTransaction())
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
                catch(Exception ex)
                {
                    tran.Rollback();
                    return (-1, ex.Message);
                }
                
            }
            
        }

        public (Trial, List<TrialEntry>) GetTrialFullData(long fHeadId)
        {
            var trial = ctx.Trials.Where(t => t.FHeadId == fHeadId).FirstOrDefault();
            var trialEnties = ctx.TrialEntries.Where(t => t.FHeadId == fHeadId).ToList();
            return (trial, trialEnties);
        }


    }
}
