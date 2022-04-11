using System;
using System.Activities;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ActivityLibrary.Activities.RDTrial
{
    public sealed class CNC工序NPI : NativeActivity
    {
        public OutArgument<bool> Answer_CNC工序NPI { get; set; }
        public OutArgument<string> BookMarkName { get; set; }
        protected override void Execute(NativeActivityContext context)
        {
            Debug.WriteLine("流入:CNC工序NPI");
            Bookmark bookmark = context.CreateBookmark("CNC工序NPI", DoSome);
        }

        private void DoSome(NativeActivityContext context, Bookmark bookmark, object value)
        {
            Debug.WriteLine("执行:CNC工序NPI");
            

            Dictionary<string, object> keys = (Dictionary<string, object>)value;
            keys.TryGetValue("IsPass", out object b);
            keys.TryGetValue("BookMarkName", out object m);

            context.SetValue(Answer_CNC工序NPI, Convert.ToBoolean(b));
            context.SetValue(BookMarkName, Convert.ToString(m));
        }

        protected override bool CanInduceIdle => true;
    }
}
