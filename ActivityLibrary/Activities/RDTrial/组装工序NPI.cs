using System;
using System.Activities;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ActivityLibrary.Activities.RDTrial
{
    public sealed class 组装工序NPI : NativeActivity
    {
        public OutArgument<bool> Answer_组装工序NPI { get; set; }

        protected override void Execute(NativeActivityContext context)
        {
            Debug.WriteLine("流入:组装工序NPI");
            Bookmark bookmark = context.CreateBookmark("组装工序NPI", DoSome);
        }

        private void DoSome(NativeActivityContext context, Bookmark bookmark, object value)
        {
            Debug.WriteLine("执行:组装工序NPI");
            context.SetValue(Answer_组装工序NPI, Convert.ToBoolean(value));
        }

        protected override bool CanInduceIdle => true;
    }
}
