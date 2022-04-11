using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using Prism.Commands;
using Prism.Regions;
using Prism.Services.Dialogs;
using RDProject.Common;
using RDProject.Models;
using RDProject.Services.Interface;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Activities;
using ActivityLibrary.Activities.RDTrial;


namespace RDProject.ViewModels
{
    public class TrialFormViewModel : BindableBase, INavigationAware
    {
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            User = navigationContext.Parameters["User"] as Employee;
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }

        private readonly IDialogService dialogService;
        private readonly IEmployeeService employeeService;
        private readonly ITrialService trialService;

        public TrialFormViewModel(IDialogService dialogService, IEmployeeService employeeService, ITrialService trialService)
        {
            this.dialogService = dialogService;
            this.employeeService = employeeService;
            this.trialService = trialService;
            Trial = new Trial() { FDate=DateTime.Now, FCompany="一车间" };
            GetEmployeesList();

            TrialEntries = new ObservableCollection<TrialEntry>();
            Employees = new ObservableCollection<Employee>();

            AddTrialEntryCommand = new DelegateCommand<string>(AddTrialEntry);
            DeleteTrialEntryCommand = new DelegateCommand<string>(DeleteTrialEntry);
            QuerySubmittedCommand = new DelegateCommand<object>(QuerySubmitted);
            SaveCommand = new DelegateCommand(Save);
            SendCommand = new DelegateCommand(Send);
            NextStepCommand = new DelegateCommand<string>(NextStep);
        }

        /// <summary>
        /// 发起人
        /// </summary>
        private Employee user;

        public Employee User
        {
            get { return user; }
            set { user = value; RaisePropertyChanged(); }
        }


        /// <summary>
        /// 表头
        /// </summary>
        private Trial trial;

        public Trial Trial
        {
            get { return trial; }
            set { trial = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 表体
        /// </summary>
        private ObservableCollection<TrialEntry> trialEntries;

        public ObservableCollection<TrialEntry> TrialEntries
        {
            get { return trialEntries; }
            set { trialEntries = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 后台获取的用户名
        /// </summary>
        private List<Employee> EmployeeList { get; set; }
        public async void GetEmployeesList()
        {
            EmployeeList = await employeeService.GetEmployeeList();
        }
        /// <summary>
        /// 下拉列表用户信息
        /// </summary>
        private ObservableCollection<Employee> employees;
        public ObservableCollection<Employee> Employees
        {
            get { return employees; }
            set { employees = value; RaisePropertyChanged(); }
        }

        public DelegateCommand<string> AddTrialEntryCommand { get; private set; }
        public DelegateCommand<string> DeleteTrialEntryCommand { get; private set; }
        public DelegateCommand<object> QuerySubmittedCommand { get;private set; }
        public DelegateCommand SaveCommand { get; private set; }
        public DelegateCommand SendCommand { get; private set; }
        public DelegateCommand<string> NextStepCommand { get; private set; }

        private void AddTrialEntry(string obj)
        {
            dialogService.ShowDialog(obj, callback =>
            {
                if(callback.Result == ButtonResult.OK)
                {
                    var result = callback.Parameters.GetValue<List<TrialEntry>>("TrialEntries");
                    TrialEntries.AddRange(result);
                }
            });
        }

        private void DeleteTrialEntry(string obj)
        {
            Debug.WriteLine(Trial.ToString());
        }
        
        private void QuerySubmitted(object obj)
        {
            string name = ((DevExpress.Xpf.Editors.AutoSuggestEdit)obj).EditText;
            //Debug.WriteLine(name);
            List<Employee> list = EmployeeList.Where(e => e.Name.Contains(name)).ToList();
            if (list != null && list.Count > 0)
            {
                Employees = new ObservableCollection<Employee>(list);
            }
        }

        private void Save()
        {
            //先检查必填项是否填了

            //保存
            (long fHeadId, string message) = trialService.SaveTrialForm(Trial, TrialEntries.ToList());
            if(message == null)
            {
                Debug.WriteLine($"表头ID：{fHeadId}");
            }
            List<TrialEntry> list;
            (Trial, list) = trialService.GetTrialFullData(fHeadId);
            TrialEntries = new ObservableCollection<TrialEntry>(list);
        }


        //WorkflowApplication app = null;
        private void Send()
        {
            var activity = new 研发项目试产记录表();
            Dictionary<string, object> keys = new Dictionary<string, object>();
            keys.Add("ParamHasCNC", Trial.FHasCNC);
            keys.Add("ParamHasCoating", Trial.FHasCoating);
            keys.Add("ParamHasLaser", Trial.FHasLaser);
            keys.Add("ParamHasAssembly", Trial.FHasAssembly);
            //app = new WorkflowApplication(activity, keys);
            //app.PersistableIdle = a => PersistableIdleAction.Unload;
            //app.Run();
            //Debug.WriteLine(app.Id.ToString());

            //开启一个工作流，获取工作流ID
            var instanceId = WFHelper.Run(activity, keys);
            Debug.WriteLine(instanceId);

            //保存工作流和审批步骤
            var steps = CreateSteps();


        }

        private void NextStep(string obj)
        {
            //app.Load(Guid.Parse(obj));
            //var arr = Answer.Split(',');
            //Dictionary<string , object> keys = new Dictionary<string , object>();
            //keys.Add("IsPass", Convert.ToBoolean(arr[0]));
            //keys.Add("BookMarkName", arr[1]);
            //app.ResumeBookmark(BookmarkName, keys);
        }


        private string bookmarkName;

        public string BookmarkName
        {
            get { return bookmarkName; }
            set { bookmarkName = value; RaisePropertyChanged(); }
        }

        private string answer;

        public string Answer
        {
            get { return answer; }
            set { answer = value; RaisePropertyChanged();}
        }

        /// <summary>
        /// 根据现有条件安排审批节点
        /// </summary>
        private List<WFStep> CreateSteps()
        {
            List<WFStep> steps = new List<WFStep>();
            steps.Add(new WFStep() { BookMark = "提交申请", SubBy = User.Name });
            steps.Add(new WFStep() { BookMark = "附件上传", SubBy = "陆冬夏" });
            steps.Add(new WFStep() { BookMark = "抄送节点", SubBy = "赵鹏" });

            if (Convert.ToBoolean(Trial.FHasCNC) == true)
                steps.Add(new WFStep() { BookMark = "CNC工序NPI", SubBy = Trial.FCNCNPI });
            if (Convert.ToBoolean(Trial.FHasCoating) == true)
                steps.Add(new WFStep() { BookMark = "喷涂工序NPI", SubBy = Trial.FCoatingNPI });
            if (Convert.ToBoolean(Trial.FHasLaser) == true)
                steps.Add(new WFStep() { BookMark = "激光切割工序NPI", SubBy = Trial.FLaserNPI });
            if (Convert.ToBoolean(Trial.FHasAssembly) == true)
                steps.Add(new WFStep() { BookMark = "组装工序NPI", SubBy = Trial.FAssemblyNPI });
            
            steps.Add(new WFStep() { BookMark = "生产部负责人", SubBy = "刘乐" });
            steps.Add(new WFStep() { BookMark = "品保部负责人", SubBy = "胡顺林" });
            steps.Add(new WFStep() { BookMark = "项目确认", SubBy = User.Name });
            steps.Add(new WFStep() { BookMark = "审批节点", SubBy = "赵鹏" });
            steps.Add(new WFStep() { BookMark = "研发部负责人", SubBy = "郝明友" });
            steps.Add(new WFStep() { BookMark = "抄送财务", SubBy = "财务" });

            return steps;
        }

        
    }
}
