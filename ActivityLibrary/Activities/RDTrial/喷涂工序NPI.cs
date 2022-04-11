using System;
using System.Activities;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ActivityLibrary.Activities.RDTrial
{
    public sealed class 喷涂工序NPI : NativeActivity
    {
        public OutArgument<bool> Answer_喷涂工序NPI { get; set; }

        protected override void Execute(NativeActivityContext context)
        {
            Debug.WriteLine("流入:喷涂工序NPI");
            Bookmark bookmark = context.CreateBookmark("喷涂工序NPI", DoSome);
        }

        private void DoSome(NativeActivityContext context, Bookmark bookmark, object value)
        {
            Debug.WriteLine("执行:喷涂工序NPI");
            context.SetValue(Answer_喷涂工序NPI, Convert.ToBoolean(value));
        }

        protected override bool CanInduceIdle => true;
    }
}
