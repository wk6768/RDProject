using System;
using System.Activities;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ActivityLibrary.Activities.RDTrial
{
    public sealed class 抄送财务 : CodeActivity
    {
        protected override void Execute(CodeActivityContext context)
        {
            Debug.WriteLine("流入:抄送财务");
        }
    }
}
