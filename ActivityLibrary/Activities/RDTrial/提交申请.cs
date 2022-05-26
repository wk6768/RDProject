using System;
using System.Activities;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ActivityLibrary.Activities.RDTrial
{
    public sealed class 提交申请 : NativeActivity
    {
        public InArgument<bool> ParamHasCNC { get; set; }
        public InArgument<bool> ParamHasCoating { get; set; }
        public InArgument<bool> ParamHasLaser { get; set; }
        public InArgument<bool> ParamHasAssembly { get; set; }
        public InArgument<bool> ParamHasFactoryOne { get; set; }
        public InArgument<bool> ParamHasFactoryTwo { get; set; }

        public OutArgument<bool> VarHasCNC { get; set; }
        public OutArgument<bool> VarHasCoating { get; set; }
        public OutArgument<bool> VarHasLaser { get; set; }
        public OutArgument<bool> VarHasAssembly { get; set; }
        public OutArgument<bool> VarHasFactoryOne { get; set; }
        public OutArgument<bool> VarHasFactoryTwo { get; set; }

        // 如果活动返回值，则从 CodeActivity<TResult>
        // 并从 Execute 方法返回该值。
        protected override void Execute(NativeActivityContext context)
        {
            var v1 = context.GetValue<bool>(ParamHasCNC);
            var v2 = context.GetValue<bool>(ParamHasCoating);
            var v3 = context.GetValue<bool>(ParamHasLaser);
            var v4 = context.GetValue<bool>(ParamHasAssembly);
            var v5 = context.GetValue<bool>(ParamHasFactoryOne);
            var v6 = context.GetValue<bool>(ParamHasFactoryTwo);

            context.SetValue(VarHasCNC, v1);
            context.SetValue(VarHasCoating, v2);
            context.SetValue(VarHasLaser, v3);
            context.SetValue(VarHasAssembly, v4);
            context.SetValue(VarHasFactoryOne, v5);
            context.SetValue(VarHasFactoryTwo, v6);

            Debug.WriteLine("提交申请");
            Debug.WriteLine(v1 + " " + v2 + " " + v3 + " " + v4 + " " + v5 + " " + v6);
            Bookmark bookmark = context.CreateBookmark("提交申请", DoSome);
        }

        private void DoSome(NativeActivityContext context, Bookmark bookmark, object value)
        {
            Debug.WriteLine("执行:提交申请");
        }

        protected override bool CanInduceIdle => true;
    }
}
