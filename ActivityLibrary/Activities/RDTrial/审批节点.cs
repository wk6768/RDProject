using System;
using System.Activities;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ActivityLibrary.Activities.RDTrial
{
    public sealed class 审批节点 : NativeActivity
    {
        public OutArgument<bool> Answer_审批节点 { get; set; }
        public OutArgument<string> BookMarkName { get; set; }

        protected override void Execute(NativeActivityContext context)
        {
            Debug.WriteLine("流入:审批节点");
            Bookmark bookmark = context.CreateBookmark("审批节点", DoSome);
        }

        private void DoSome(NativeActivityContext context, Bookmark bookmark, object value)
        {
            Debug.WriteLine("执行:审批节点");

            Dictionary<string, object> keys = (Dictionary<string, object>)value;
            keys.TryGetValue("IsPass", out object b);
            keys.TryGetValue("BookMarkName", out object m);

            context.SetValue(Answer_审批节点, Convert.ToBoolean(b));
            context.SetValue(BookMarkName, Convert.ToString(m));
        }

        protected override bool CanInduceIdle => true;
    }
}
