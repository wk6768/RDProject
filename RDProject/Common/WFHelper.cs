using System;
using System.Activities;
using System.Activities.DurableInstancing;
using System.Collections.Generic;
using System.Runtime.DurableInstancing;
using System.Text;
using System.Windows;

namespace RDProject.Common
{
    public static class WFHelper
    {
        private static string ConnString()
        {
            return "server=192.168.5.80;database=yanfa_test_1;uid=sa;pwd=123";
        }

        /// <summary>
        /// 开始一个活动
        /// </summary>
        /// <param name="activity"></param>
        /// <returns></returns>
        public static string Run(Activity activity)
        {
            WorkflowApplication app = new WorkflowApplication(activity);

            app.PersistableIdle= a => PersistableIdleAction.Unload;

            //持久化
            InstanceStore store = new SqlWorkflowInstanceStore(ConnString());
            app.InstanceStore = store;
            app.Run();
            
            return app.Id.ToString();
        }

        /// <summary>
        /// 开始一个活动并传递参数
        /// </summary>
        /// <param name="activity"></param>
        /// <param name="keys"></param>
        /// <returns></returns>
        public static string Run(Activity activity, Dictionary<string,object> keys)
        {
            WorkflowApplication app = new WorkflowApplication(activity, keys);

            app.PersistableIdle= a => PersistableIdleAction.Unload;

            InstanceStore store = new SqlWorkflowInstanceStore(ConnString());
            app.InstanceStore = store;
            app.Run();

            return app.Id.ToString();
        }

        /// <summary>
        /// 继续执行活动
        /// </summary>
        /// <param name="activity"></param>
        /// <param name="guid"></param>
        /// <param name="bookmarkName"></param>
        /// <param name="value"></param>
        public static void Resume(Activity activity, string guid, string bookmarkName, object value)
        {
            try
            {
                WorkflowApplication app = new WorkflowApplication(activity);

                app.PersistableIdle = a => PersistableIdleAction.Unload;

                InstanceStore store = new SqlWorkflowInstanceStore(ConnString());
                app.InstanceStore = store;

                app.Load(Guid.Parse(guid));

                app.ResumeBookmark(bookmarkName, value);
            }catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
