using System;
using System.Activities;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ActivityLibrary.Activities.RDTrial
{
    public sealed class 激光切割工序NPI : NativeActivity
    {
        public OutArgument<bool> Answer_激光切割工序NPI { get; set; }

        protected override void Execute(NativeActivityContext context)
        {
            Debug.WriteLine("流入:激光切割工序NPI");
            Bookmark bookmark = context.CreateBookmark("激光切割工序NPI", DoSome);
        }

        private void DoSome(NativeActivityContext context, Bookmark bookmark, object value)
        {
            Debug.WriteLine("执行:激光切割工序NPI");
            context.SetValue(Answer_激光切割工序NPI, Convert.ToBoolean(value));
        }

        protected override bool CanInduceIdle => true;
    }
}
