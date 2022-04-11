using System;
using System.Activities;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ActivityLibrary.Activities.RDTrial
{
    public sealed class 生产部负责人 : NativeActivity
    {
        public OutArgument<bool> Answer_生产部负责人 { get; set; }

        protected override void Execute(NativeActivityContext context)
        {
            Debug.WriteLine("流入:生产部负责人");
            Bookmark bookmark = context.CreateBookmark("生产部负责人", DoSome);
        }

        private void DoSome(NativeActivityContext context, Bookmark bookmark, object value)
        {
            Debug.WriteLine("执行:生产部负责人");
            context.SetValue(Answer_生产部负责人, Convert.ToBoolean(value));
        }

        protected override bool CanInduceIdle => true;
    }
}
