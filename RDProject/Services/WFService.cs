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

        public void SaveWFSteps(ObservableCollection<WFStep> steps)
        {
            ctx.WFSteps.AddRange(steps);
        }
    }
}
