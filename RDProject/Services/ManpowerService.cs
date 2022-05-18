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
    public class ManpowerService : IManpowerService
    {
        private readonly MyDbContext ctx;

        public ManpowerService(MyDbContext ctx)
        {
            this.ctx = ctx;
        }

        public (Manpower, ObservableCollection<ManpowerEntry>) SaveManpowerPageAndReturnFullData(Manpower manpower, ObservableCollection<ManpowerEntry> manpowerEntries)
        {
            using(var tran = ctx.Database.BeginTransaction())
            {
                ctx.Manpowers.Add(manpower);
                ctx.SaveChanges();
                
                foreach(var entry in manpowerEntries)
                {
                    entry.FHeadId = manpower.FHeadId;
                }

                ctx.manpowerEntries.AddRange(manpowerEntries);
                ctx.SaveChanges();

                tran.Commit();

                return (manpower, manpowerEntries);
            }
        }

        public (Manpower, ObservableCollection<ManpowerEntry>) UpdateManpowerPageAndReturnFullData(Manpower manpower, ObservableCollection<ManpowerEntry> manpowerEntries)
        {
            using(var tran = ctx.Database.BeginTransaction())
            {
                ctx.Manpowers.Update(manpower);
                ctx.manpowerEntries.UpdateRange(manpowerEntries);

                ctx.SaveChanges();

                tran.Commit();

                return (manpower, manpowerEntries);
            }
        }
    }
}
