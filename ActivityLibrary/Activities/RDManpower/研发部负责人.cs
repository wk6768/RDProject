using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ActivityLibrary.Activities.RDManpower
{
    public sealed class 研发部负责人 : NativeActivity
    {
        // 定义一个字符串类型的活动输入参数
        public InArgument<string> Text { get; set; }

        // 如果活动返回值，则从 CodeActivity<TResult>
        // 并从 Execute 方法返回该值。
        protected override void Execute(NativeActivityContext context)
        {
            // 获取 Text 输入参数的运行时值
            string text = context.GetValue(this.Text);
        }
    }
}
