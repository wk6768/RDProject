﻿using System;
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
using DevExpress.Xpf.Core;
using DevExpress.Xpf.WindowsUI;
using System.Windows;

namespace RDProject.ViewModels
{
    public class TrialFormViewModel : BindableBase, INavigationAware
    {
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            User = navigationContext.Parameters["User"] as Employee;
            var fHeadID = (long)navigationContext.Parameters["FHeadID"];
            if (fHeadID <= 0)
            {
                //如果小于0，则是新增表单
                InitNewTrialForm();
            } else if (fHeadID > 0)
            {
                //如果大于0，则是浏览表单
                InitOldTrialForm(fHeadID);
            }
            GetEmployeesList();
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
        private readonly IWFService wfService;

        public TrialFormViewModel(IDialogService dialogService, IEmployeeService employeeService, ITrialService trialService, IWFService wfService)
        {
            this.dialogService = dialogService;
            this.employeeService = employeeService;
            this.trialService = trialService;
            this.wfService = wfService;

            Employees = new ObservableCollection<Employee>();

            AddTrialEntryCommand = new DelegateCommand<string>(AddTrialEntry);
            DeleteTrialEntryCommand = new DelegateCommand<string>(DeleteTrialEntry);
            QuerySubmittedCommand = new DelegateCommand<object>(QuerySubmitted);
            SaveCommand = new DelegateCommand(Save);
            SendCommand = new DelegateCommand(Send);
            NextStepCommand = new DelegateCommand<string>(NextStep);
            CheckCommand = new DelegateCommand(Check);
        }

       


        /// <summary>
        /// 初始化空白表单
        /// </summary>
        void InitNewTrialForm()
        {
            Trial = new Trial()
            {
                FDate = DateTime.Now,
                FCompany = "一车间",
                FCreateUser = User.Name,
                FTitle = $"研发项目试产记录表-{User.EmpName}-{User.Name}-{DateTime.Now.ToString("D")}"
            };

            TrialEntries = new ObservableCollection<TrialEntry>();

            Instance = new WFInstance();

            Steps = new ObservableCollection<WFStep>();
        }

        void InitOldTrialForm(long fHeadID)
        {
            List<TrialEntry> list;
            (Trial, list) = trialService.GetTrialFullData(fHeadID);
            TrialEntries = new ObservableCollection<TrialEntry>(list);

            List<WFStep> list2;
            (Instance, list2) = wfService.GetInstanceByTableNameAndHeadID("Trial", fHeadID);
            if(list2 == null)
            {
                //有表单，但是没有启用审批流
                list2 = new List<WFStep>();
            }
            Steps = new ObservableCollection<WFStep>(list2);
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

        /// <summary>
        /// 流程
        /// </summary>
        private WFInstance instance;

        public WFInstance Instance
        {
            get { return instance; }
            set { instance = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 步骤
        /// </summary>
        private ObservableCollection<WFStep> steps;

        public ObservableCollection<WFStep> Steps
        {
            get { return steps; }
            set { steps = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 节点名称
        /// </summary>
        private string bookmarkName;

        public string BookmarkName
        {
            get { return bookmarkName; }
            set { bookmarkName = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 是否通过
        /// </summary>
        private string answer;

        public string Answer
        {
            get { return answer; }
            set { answer = value; RaisePropertyChanged(); }
        }

        public DelegateCommand<string> AddTrialEntryCommand { get; private set; }
        public DelegateCommand<string> DeleteTrialEntryCommand { get; private set; }
        public DelegateCommand<object> QuerySubmittedCommand { get;private set; }
        public DelegateCommand SaveCommand { get; private set; }
        public DelegateCommand SendCommand { get; private set; }
        public DelegateCommand<string> NextStepCommand { get; private set; }

        public DelegateCommand CheckCommand { get; private set; }

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
            #region 检查必填项
            //先检查必填项是否填了
            if (string.IsNullOrWhiteSpace(Trial.FRDNo) || string.IsNullOrWhiteSpace(Trial.FProductName) || string.IsNullOrWhiteSpace(Trial.FCompany))
            {
                WinUIMessageBox.Show("研发项目编号，产品名称，厂别 为必填项", "提示",  MessageBoxButton.OK, MessageBoxImage.Warning,MessageBoxResult.None, MessageBoxOptions.None);
                return;
            }
            if (Trial.FHasCNC == true && string.IsNullOrWhiteSpace(Trial.FCNCNPI))
            {
                WinUIMessageBox.Show("CNC工序NPI为 必填项", "提示", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.None, MessageBoxOptions.None);
                return;
            }
            if (Trial.FHasCoating == true && string.IsNullOrWhiteSpace(Trial.FCoatingNPI))
            {
                WinUIMessageBox.Show("喷涂工序NPI 为必填项", "提示", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.None, MessageBoxOptions.None);
                return;
            }
            if (Trial.FHasLaser == true && string.IsNullOrWhiteSpace(Trial.FLaserNPI))
            {
                WinUIMessageBox.Show("激光切割工序NPI 为必填项", "提示", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.None, MessageBoxOptions.None);
                return;
            }
            if (Trial.FHasAssembly == true && string.IsNullOrWhiteSpace(Trial.FAssemblyNPI))
            {
                WinUIMessageBox.Show("组装工序NPI 为必填项", "提示", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.None, MessageBoxOptions.None);
                return;
            }
            #endregion


            //如果编号FHeadID为null或者0，则保存；如果FHeadID 大于0，则更新
            //保存
            switch (Trial.FHeadId)
            {
                case 0:
                    Debug.WriteLine("新增表单");
                    (Trial, TrialEntries) = trialService.SaveTrialPageAndReturnFullData(Trial, TrialEntries);
                    if (Trial.FHeadId > 0)
                    {
                        WinUIMessageBox.Show("保存成功", "提示", MessageBoxButton.OK, MessageBoxImage.Asterisk, MessageBoxResult.None, MessageBoxOptions.None);
                    }
                    break;
                default:
                    Debug.WriteLine("更新表单");
                    (Trial, TrialEntries) = trialService.UpdateTrialPageAndReturnFullData(Trial, TrialEntries);
                    WinUIMessageBox.Show("修改成功", "提示", MessageBoxButton.OK, MessageBoxImage.Asterisk, MessageBoxResult.None, MessageBoxOptions.None);
                    break;
            }


            //Steps = CreateSteps();
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
            var guid = WFHelper.Run(activity, keys);
            Debug.WriteLine(guid);


            //构建审批表
            WFInstance instance = new WFInstance()
            {
                TableName = "Trial",
                InstanceGuid = guid,
                HeadId = Trial.FHeadId,
                SubBy = Trial.FCreateUser,
            };
            //构建审批步骤
            Steps = CreateSteps();
            //保存
            (instance, Steps) = wfService.SaveInstance(instance, Steps);

            //发送邮件


            //提交后清空
            InitNewTrialForm();
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

        private void Check()
        {
            //查找当前由谁审批
            var step = Steps.Where(s => s.Status != 1).FirstOrDefault();
            if (step == null)
            {
                //step为空，表示没有启用审批流或者已审批完成
                WinUIMessageBox.Show($"该表单没有启用审批流或者已完成审批", "提示", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.None, MessageBoxOptions.None);
                return;
            }
            //名字对不上，则提示
            if (!User.Name.Equals(step.SubBy))
            {
                WinUIMessageBox.Show($"账号不正确,当前节点审批人是 {step.SubBy}", "提示", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.None, MessageBoxOptions.None);
                return;
            }

            bool CheckResultPass = false;       //通过
            bool CheckResultReject = false;     //驳回
            string Reason = null;               
            string Bookmark = null;             

            var keys1 = new DialogParameters();
            keys1.Add("Steps", Steps);
            dialogService.ShowDialog("CheckDialog", keys1, callback =>
            {
                CheckResultPass = callback.Parameters.GetValue<bool>("CheckResultPass");
                CheckResultReject = callback.Parameters.GetValue<bool>("CheckResultReject");
                Reason = callback.Parameters.GetValue<string>("Reason");
                if (callback.Parameters.ContainsKey("Bookmark"))
                {
                    Bookmark = callback.Parameters.GetValue<string>("Bookmark");
                }
            });

            //审批，就是 调用Instance，并更新Steps
            //如果审批通过
            if (CheckResultPass == true)
            {
                //执行工作流
                Dictionary<string, object> keys2 = new Dictionary<string, object>();
                keys2.Add("IsPass", true);
                keys2.Add("BookMarkName", step.BookMark);
                WFHelper.Resume(
                        new 研发项目试产记录表(),
                        Instance.InstanceGuid,
                        step.BookMark,
                        keys2
                    );
                //更新审批和审批步骤
                step.Status = 1;
                step.SubTime = DateTime.Now;
                step.Remark = Reason;

                //如果下一步骤为抄送节点，则直接更新下一节点
                var flag = true;
                while (flag)
                {
                    var step2 = Steps.Where(s => s.Status == 0).FirstOrDefault();
                    if (step2.BookMark.Contains("抄送"))
                    {
                        step2.Status = 1;
                        step2.SubTime = DateTime.Now;
                        step2.Remark = "抄送";
                    }
                    else
                    {
                        break;
                    }
                }

                (Instance, Steps) = wfService.UpdateInstance(Instance, Steps);
            }
            //驳回
            if(CheckResultReject == true)
            {

                //如果驳回的节点为空，则找上一个不为抄送节点的节点
                if (Bookmark == null)
                {
                    var lastStep = Steps.Where(s => s.StepNo < step.StepNo && !s.BookMark.Contains("抄送")).OrderByDescending(s => s.StepNo).FirstOrDefault();
                    Bookmark = lastStep.BookMark;
                }

                //执行工作流
                Dictionary<string, object> keys2 = new Dictionary<string, object>();
                keys2.Add("IsPass", false);
                keys2.Add("BookMarkName", Bookmark);
                

                WFHelper.Resume(
                        new 研发项目试产记录表(),
                        Instance.InstanceGuid,
                        step.BookMark,
                        keys2
                    );

                
                //更新审批和审批步骤
                step.Status = 2; //2表示在此节点驳回
                step.SubTime = DateTime.Now;
                step.Remark = Reason;

                //找到驳回的节点
                var rejectStep = Steps.Where(s => s.BookMark == Bookmark).First();
                //找到驳回的节点之后的所有节点(s.Status != 2 不包含已被驳回的节点)
                var rejectSteps = Steps.Where(s => s.StepNo >= rejectStep.StepNo && s.Status != 2).ToList();
                //将这些中间节点全部更新为未审核
                foreach(var item in rejectSteps)
                {
                    item.Status = 0;
                }
                //更新
                (Instance, Steps) = wfService.UpdateInstance(Instance, Steps);
                
            }
        }


        /// <summary>
        /// 根据现有条件安排审批节点
        /// </summary>
        private ObservableCollection<WFStep> CreateSteps()
        {
            var stepNo = 1;
            List<WFStep> steps = new List<WFStep>();
            steps.Add(new WFStep() { BookMark = "提交申请", SubBy = User.Name , SubTime = DateTime.Now, StepNo = stepNo++, Status = 1});
            steps.Add(new WFStep() { BookMark = "附件上传", SubBy = "陆冬夏", StepNo = stepNo++ });
            steps.Add(new WFStep() { BookMark = "抄送节点", SubBy = "赵鹏", StepNo = stepNo++ });

            if (Convert.ToBoolean(Trial.FHasCNC) == true)
                steps.Add(new WFStep() { BookMark = "CNC工序NPI", SubBy = Trial.FCNCNPI, StepNo = stepNo++ });
            if (Convert.ToBoolean(Trial.FHasCoating) == true)
                steps.Add(new WFStep() { BookMark = "喷涂工序NPI", SubBy = Trial.FCoatingNPI, StepNo = stepNo++ });
            if (Convert.ToBoolean(Trial.FHasLaser) == true)
                steps.Add(new WFStep() { BookMark = "激光切割工序NPI", SubBy = Trial.FLaserNPI, StepNo = stepNo++ });
            if (Convert.ToBoolean(Trial.FHasAssembly) == true)
                steps.Add(new WFStep() { BookMark = "组装工序NPI", SubBy = Trial.FAssemblyNPI, StepNo = stepNo++ });
            
            steps.Add(new WFStep() { BookMark = "生产部负责人", SubBy = "刘乐", StepNo = stepNo++ });
            steps.Add(new WFStep() { BookMark = "品保部负责人", SubBy = "胡顺林", StepNo = stepNo++ });
            steps.Add(new WFStep() { BookMark = "项目确认", SubBy = User.Name, StepNo = stepNo++ });
            steps.Add(new WFStep() { BookMark = "审批节点", SubBy = "赵鹏", StepNo = stepNo++ });
            steps.Add(new WFStep() { BookMark = "研发部负责人", SubBy = "郝明友", StepNo = stepNo++ });
            steps.Add(new WFStep() { BookMark = "抄送财务", SubBy = "财务", StepNo = stepNo++ });

            steps = steps.OrderBy(s => s.StepNo).ToList();
            return new ObservableCollection<WFStep>(steps);
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        private void SendMail()
        {
            //取不是该用户的第一个步骤（该用户下一位）
            var step = Steps.OrderBy(s => s.StepId).Where(s => s.SubBy != User.Name).First();
            //获取下一步骤后，就可以发邮件给他了

        }

    }
}
