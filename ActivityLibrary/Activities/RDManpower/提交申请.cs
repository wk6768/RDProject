using System;
using System.Activities;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ActivityLibrary.Activities.RDManpower
{
    public sealed class 提交申请 : NativeActivity
    {
 
        protected override void Execute(NativeActivityContext context)
        {
            Debug.WriteLine("提交申请");
            Bookmark bookmark = context.CreateBookmark("提交申请", DoSome);
        }

        private void DoSome(NativeActivityContext context, Bookmark bookmark, object value)
        {
            Debug.WriteLine("执行:提交申请");
        }

        protected override bool CanInduceIdle => true;
    }
}
