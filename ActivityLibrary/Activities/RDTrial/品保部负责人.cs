using System;
using System.Activities;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ActivityLibrary.Activities.RDTrial
{
    public sealed class 品保部负责人 : NativeActivity
    {
        public OutArgument<bool> Answer_品保部负责人 { get; set; }
        public OutArgument<string> BookMarkName { get; set; }
        protected override void Execute(NativeActivityContext context)
        {
            Debug.WriteLine("流入:品保部负责人");
            Bookmark bookmark = context.CreateBookmark("品保部负责人", DoSome);
        }

        private void DoSome(NativeActivityContext context, Bookmark bookmark, object value)
        {
            Debug.WriteLine("执行:品保部负责人");


            Dictionary<string, object> keys = (Dictionary<string, object>)value;
            keys.TryGetValue("IsPass", out object b);
            keys.TryGetValue("BookMarkName", out object m);

            context.SetValue(Answer_品保部负责人, Convert.ToBoolean(b));
            context.SetValue(BookMarkName, Convert.ToString(m));
        }

        protected override bool CanInduceIdle => true;
    }
}
