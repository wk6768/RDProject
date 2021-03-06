using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using Prism.Commands;
using Prism.Regions;
using Prism.Services.Dialogs;
using Prism.Events;
using RDProject.Common;
using RDProject.Models;
using RDProject.Services.Interface;
using RDProject.Event;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Activities;
using ActivityLibrary.Activities.RDTrial;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.WindowsUI;
using System.Windows;
using Microsoft.Win32;

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
                InitNewForm();

                #region 新建表单时的权限控制
                if (User.UserGroup.Equals("管理员"))
                {
                    ColumnVisibility = true;    //是否显示隐藏列
                    Readonly_Head = false;     //表单头是否只读
                    Readonly_Entry = false;//表单明细是否只读
                    Readonly_WFStep = false;    //步骤是否只读
                    WonoInfoEdit = false;       //工单信息是否只读

                    SaveButtonEnable = true;    //保存表单内容的按钮
                    SendButtonEnable = true;    //发起流程的按钮
                    EditButtonEnable = true;    //新建、删除明细的按钮
                    NewButtonEnable = true;     //新建表单的按钮

                    
                }
                else
                {
                    ColumnVisibility = false;
                    Readonly_Head = false;
                    Readonly_Entry = false;
                    Readonly_WFStep = true;
                    WonoInfoEdit = false;

                    SaveButtonEnable = true;
                    SendButtonEnable = true;
                    EditButtonEnable = true;
                    NewButtonEnable = true;

                   
                }
                #endregion

            }
            else if (fHeadID > 0)
            {
                //如果大于0，则是浏览表单
                InitOldForm(fHeadID);

                #region 浏览表单时的权限控制
                if (User.UserGroup.Equals("管理员"))
                {
                    ColumnVisibility = true;
                    Readonly_Head = false;
                    Readonly_Entry = false;
                    Readonly_WFStep = false;
                    WonoInfoEdit = false;

                    SaveButtonEnable = true;
                    SendButtonEnable = true;
                    EditButtonEnable = true;
                    NewButtonEnable = false;

                }
                else if (User.UserGroup.Equals("NPI"))
                {
                    ColumnVisibility = false;
                    Readonly_Head = true;
                    Readonly_Entry = false;
                    Readonly_WFStep = true;
                    WonoInfoEdit = true;

                    SaveButtonEnable = true;
                    SendButtonEnable = false;
                    EditButtonEnable = true;
                    NewButtonEnable = false;
                }
                else
                {
                    ColumnVisibility = false;
                    Readonly_Head = true;
                    Readonly_Entry = true;
                    Readonly_WFStep = true;

                    SaveButtonEnable = false;
                    SendButtonEnable = false;
                    EditButtonEnable = false;
                    NewButtonEnable = false;

                    if (User.Id.Equals("14120"))
                    {
                        SaveButtonEnable = true;
                        WonoInfoEdit = false;
                    }
                    else
                    {
                        WonoInfoEdit = true;
                    }
                }
                #endregion
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
        private readonly IEventAggregator aggregator;

        public TrialFormViewModel(IDialogService dialogService, IEmployeeService employeeService, ITrialService trialService, IWFService wfService, IEventAggregator aggregator)
        {
            this.dialogService = dialogService;
            this.employeeService = employeeService;
            this.trialService = trialService;
            this.wfService = wfService;
            this.aggregator = aggregator;
            Employees = new ObservableCollection<Employee>();

            AddTrialCommand = new DelegateCommand(AddTrial);
            AddTrialEntryCommand = new DelegateCommand<string>(AddTrialEntry);
            DeleteTrialEntryCommand = new DelegateCommand<TrialEntry>(DeleteTrialEntry);
            QuerySubmittedCommand = new DelegateCommand<object>(QuerySubmitted);
            SaveCommand = new DelegateCommand(Save);
            SendCommand = new DelegateCommand(Send);
            CheckCommand = new DelegateCommand(Check);
            UpdateTrialEntryCommand = new DelegateCommand<TrialEntry>(UpdateTrialEntry);
            UpdateStepCommand = new DelegateCommand<WFStep>(UpdateStep);
            ExportCommand = new DelegateCommand(Export);
        }


        /// <summary>
        /// 初始化空白表单
        /// </summary>
        void InitNewForm()
        {
            Trial = new Trial()
            {
                FDate = DateTime.Now,
                //FAssemblyFactory = "一车间",
                //FCompany = "广东紫文星电子科技有限公司",
                FCreateUser = User.Name,
                FTitle = $"研发项目试产记录表-{User.DeptName}-{User.Name}-{DateTime.Now.ToString("D")}",
                FStatus = 0,
            };

            TrialEntries = new ObservableCollection<TrialEntry>();

            Instance = new WFInstance();

            Steps = new ObservableCollection<WFStep>();
        }

        void InitOldForm(long fHeadID)
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
        /// 审批意见
        /// </summary>
        private string answer;

        public string Answer
        {
            get { return answer; }
            set { answer = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 控制部分字段的显示（只有管理员端显示）
        /// </summary>
        private bool columnVisibility;

        public bool ColumnVisibility
        {
            get { return columnVisibility; }
            set { columnVisibility = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 控制部分字段只读
        /// </summary>
        private bool readonly_Head;

        public bool Readonly_Head
        {
            get { return readonly_Head; }
            set { readonly_Head = value; RaisePropertyChanged(); }
        }

        private bool readonly_Entry;

        public bool Readonly_Entry
        {
            get { return readonly_Entry; }
            set { readonly_Entry = value; RaisePropertyChanged(); }
        }

        private bool readonly_WFStep;

        public bool Readonly_WFStep
        {
            get { return readonly_WFStep; }
            set { readonly_WFStep = value; RaisePropertyChanged(); }
        }

        private bool saveButtonEnable;

        public bool SaveButtonEnable
        {
            get { return saveButtonEnable; }
            set { saveButtonEnable = value; RaisePropertyChanged(); }
        }

        private bool sendButtonEnable;

        public bool SendButtonEnable
        {
            get { return sendButtonEnable; }
            set { sendButtonEnable = value; RaisePropertyChanged(); }
        }

        private bool editButtonEnable;

        public bool EditButtonEnable
        {
            get { return editButtonEnable; }
            set { editButtonEnable = value; RaisePropertyChanged(); }
        }

        private bool newButtonEnable;

        public bool NewButtonEnable
        {
            get { return newButtonEnable; }
            set { newButtonEnable = value; RaisePropertyChanged(); }
        }

        private bool wonoInfoEdit;

        public bool WonoInfoEdit
        {
            get { return wonoInfoEdit; }
            set { wonoInfoEdit = value; RaisePropertyChanged(); }
        }



        public DelegateCommand AddTrialCommand { get; private set; }
        public DelegateCommand<string> AddTrialEntryCommand { get; private set; }
        public DelegateCommand<TrialEntry> DeleteTrialEntryCommand { get; private set; }
        public DelegateCommand<object> QuerySubmittedCommand { get;private set; }
        public DelegateCommand SaveCommand { get; private set; }
        public DelegateCommand SendCommand { get; private set; }
        public DelegateCommand CheckCommand { get; private set; }
        public DelegateCommand<TrialEntry> UpdateTrialEntryCommand { get; private set; }
        public DelegateCommand<WFStep> UpdateStepCommand { get; private set; }
        public DelegateCommand ExportCommand { get; private set; }

        private void AddTrial()
        {
            //初始化一个空表单
            InitNewForm();
        }

        private void AddTrialEntry(string obj)
        {
            dialogService.ShowDialog(obj, callback =>
            {
                if(callback.Result == ButtonResult.OK)
                {
                    if (callback.Parameters.ContainsKey("TrialEntries"))
                    {
                        var result = callback.Parameters.GetValue<List<TrialEntry>>("TrialEntries");
                        TrialEntries.AddRange(result);
                    }
                }
            });
        }

        private void DeleteTrialEntry(TrialEntry obj)
        {
            TrialEntries.Remove(obj);
            (Trial, TrialEntries) = trialService.UpdateTrialPageAndReturnFullData(Trial, TrialEntries);
        }

        private void UpdateTrialEntry(TrialEntry obj)
        {
            trialService.UpdateTrialEntry(obj);
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
            if (string.IsNullOrWhiteSpace(Trial.FRDNo) || string.IsNullOrWhiteSpace(Trial.FProductName) || string.IsNullOrWhiteSpace(Trial.FCompany) )
            {
                WinUIMessageBox.Show("研发项目编号，产品名称，公司 为必填项", "提示",  MessageBoxButton.OK, MessageBoxImage.Warning,MessageBoxResult.None, MessageBoxOptions.None);
                return;
            }
            if (Trial.FHasCNC && string.IsNullOrWhiteSpace(Trial.FCNCNPI))
            {
                WinUIMessageBox.Show("CNC工序NPI为 必填项", "提示", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.None, MessageBoxOptions.None);
                return;
            }
            if (Trial.FHasCoating && string.IsNullOrWhiteSpace(Trial.FCoatingNPI))
            {
                WinUIMessageBox.Show("喷涂工序NPI 为必填项", "提示", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.None, MessageBoxOptions.None);
                return;
            }
            if (Trial.FHasLaser && string.IsNullOrWhiteSpace(Trial.FLaserNPI))
            {
                WinUIMessageBox.Show("激光切割工序NPI 为必填项", "提示", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.None, MessageBoxOptions.None);
                return;
            }
            if (Trial.FHasAssembly && string.IsNullOrWhiteSpace(Trial.FAssemblyNPI))
            {
                WinUIMessageBox.Show("组装工序NPI 为必填项", "提示", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.None, MessageBoxOptions.None);
                return;
            }
            if (Trial.FHasAssembly && string.IsNullOrWhiteSpace(Trial.FAssemblyFactory))
            {
                WinUIMessageBox.Show("组装车间 为必填项", "提示", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.None, MessageBoxOptions.None);
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


        private void Send()
        {
            if(Steps.Count > 0)
            {
                WinUIMessageBox.Show("重复发起审批", "提示", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.None, MessageBoxOptions.None);
                return;
            }
            var activity = new 研发项目试产记录表();
            Dictionary<string, object> keys = new Dictionary<string, object>();
            keys.Add("ParamHasCNC", Trial.FHasCNC);
            keys.Add("ParamHasCoating", Trial.FHasCoating);
            keys.Add("ParamHasLaser", Trial.FHasLaser);
            keys.Add("ParamHasAssembly", Trial.FHasAssembly);

            //如果有组装工序
            if (Trial.FHasAssembly)
            {
                keys.Add("ParamHasFactoryOne", Trial.FAssemblyFactory.Equals("一车间"));
                keys.Add("ParamHasFactoryTwo", Trial.FAssemblyFactory.Equals("二车间"));
            }
            else
            {
                keys.Add("ParamHasFactoryOne", false);
                keys.Add("ParamHasFactoryTwo", false);
            }

            //如果有喷涂，则有二车间
            if (keys.ContainsKey("ParamHasFactoryTwo"))
            {
                keys["ParamHasFactoryTwo"] = Trial.FHasCoating;
            }
            else
            {
                keys.Add("ParamHasFactoryTwo", Trial.FHasCoating);
            }
            



            //开启一个工作流，获取工作流ID
            var guid = WFHelper.Run(activity, keys);
            Debug.WriteLine(guid);

            //更新表单为审核状态
            Trial.FStatus = 1;
            (Trial, _) = trialService.UpdateTrialPageAndReturnFullData(Trial, null);

            //构建步骤表
            Steps = CreateSteps();

            //构建审批表
            var step = Steps.Where(s => s.Status != 1).FirstOrDefault();//下一步
            WFInstance instance = new WFInstance()
            {
                TableName = "Trial",
                InstanceGuid = guid,
                HeadId = Trial.FHeadId,
                SubBy = Trial.FCreateUser,
                Status = 1,
                NextName = step == null ? null : step.SubBy,
            };

            //保存
            (instance, Steps) = wfService.SaveInstance(instance, Steps);


            //更新工作流
            _ = Task.Run(async () =>
            {
                await Task.Delay(5000);
                _ = WFHelper.Resume(
                    activity,
                    guid,
                    "提交申请",
                    null
                );
            });

            //发送邮件
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
                Console.WriteLine("审批--" + CheckResultPass + CheckResultReject + Reason + Bookmark);
            });

            //审批，就是 调用Instance，并更新Steps
            //如果审批通过
            if (CheckResultPass == true)
            {
                //执行工作流
                Dictionary<string, object> keys2 = new Dictionary<string, object>();
                keys2.Add("IsPass", true);
                keys2.Add("BookMarkName", step.BookMark);
                var result = WFHelper.Resume(
                        new 研发项目试产记录表(),
                        Instance.InstanceGuid,
                        step.BookMark,
                        keys2
                    );
                if(result == false)
                {
                    return;
                }

                //审批步骤和更新审批
                //审批步骤
                step.Status = 1;
                step.SubTime = DateTime.Now;
                step.Remark = Reason;
                //如果下一步骤为抄送节点，则直接更新下一节点
                var flag = true;
                while (flag)
                {
                    var step2 = Steps.Where(s => s.Status == 0).FirstOrDefault();
                    if(step2 == null)
                    {
                        break;
                    }
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

                //审批表
                //如果当前节点为最后一个不为抄送节点的 节点,则表示流程结束, 更新 审批表、表单 的状态为3
                var last = Steps.Where(s => !s.BookMark.Contains("抄送")).OrderBy(s => s.StepNo).LastOrDefault();
                if (step.StepNo == last.StepNo)
                {
                    Instance.Status = 3;
                    Trial.FStatus = 3;
                    (Trial, _) = trialService.UpdateTrialPageAndReturnFullData(Trial, null);
                }

                //设置下一审批人
                var step3 = Steps.Where(s => s.Status != 1).FirstOrDefault();
                Instance.NextName = step3 == null ? null : step3.SubBy;

                //更新审批表和步骤表
                (Instance, Steps) = wfService.UpdateInstance(Instance, Steps);


                //执行工作流
                //Dictionary<string, object> keys2 = new Dictionary<string, object>();
                //keys2.Add("IsPass", true);
                //keys2.Add("BookMarkName", step.BookMark);
                //WFHelper.Resume(
                //        new 研发项目试产记录表(),
                //        Instance.InstanceGuid,
                //        step.BookMark,
                //        keys2
                //    );
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

                var result = WFHelper.Resume(
                    new 研发项目试产记录表(),
                    Instance.InstanceGuid,
                    step.BookMark,
                    keys2
                );
                if (result == false)
                {
                    return;
                }


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

                Instance.NextName = rejectStep.SubBy;
                //更新
                (Instance, Steps) = wfService.UpdateInstance(Instance, Steps);
                
            }

            //发布消息，刷新右侧列表
            aggregator.GetEvent<RefreshMyTitleListEvent>().Publish();
        }


        /// <summary>
        /// 根据现有条件安排审批节点
        /// </summary>
        private ObservableCollection<WFStep> CreateSteps()
        {
            var stepNo = 1;
            List<WFStep> steps = new List<WFStep>();
            steps.Add(new WFStep() { BookMark = "提交申请", SubBy = User.Name , SubTime = DateTime.Now, Remark = "发起流程", StepNo = stepNo++, Status = 1});
            steps.Add(new WFStep() { BookMark = "附件上传", SubBy = "陆冬夏", StepNo = stepNo++ });
            steps.Add(new WFStep() { BookMark = "抄送节点", SubBy = "赵鹏", StepNo = stepNo++ });

            if (Trial.FHasCNC)
            {
                steps.Add(new WFStep() { BookMark = "CNC工序NPI", SubBy = Trial.FCNCNPI, StepNo = stepNo++ });
            }

            if (Trial.FHasCoating)
            {
                steps.Add(new WFStep() { BookMark = "喷涂工序NPI", SubBy = Trial.FCoatingNPI, StepNo = stepNo++ });
            }

            if (Trial.FHasLaser)
            {
                steps.Add(new WFStep() { BookMark = "激光切割工序NPI", SubBy = Trial.FLaserNPI, StepNo = stepNo++ });
            }

            if (Trial.FHasAssembly)
            {
                steps.Add(new WFStep() { BookMark = "组装工序NPI", SubBy = Trial.FAssemblyNPI, StepNo = stepNo++ });
                if (Trial.FAssemblyFactory.Equals("一车间"))
                {
                    steps.Add(new WFStep() { BookMark = "生产部负责人", SubBy = "刘乐", StepNo = stepNo++ });
                }
                if (Trial.FAssemblyFactory.Equals("二车间"))
                {
                    steps.Add(new WFStep() { BookMark = "二厂生产部负责人", SubBy = "李上波", StepNo = stepNo++ });
                }
            }

            if (Trial.FHasCoating)
            {
                steps.Add(new WFStep() { BookMark = "二厂生产部负责人", SubBy = "李上波", StepNo = stepNo++ });
            }

            steps.Add(new WFStep() { BookMark = "品保部负责人", SubBy = "胡顺林", StepNo = stepNo++ });
            steps.Add(new WFStep() { BookMark = "项目确认", SubBy = User.Name, StepNo = stepNo++ });
            steps.Add(new WFStep() { BookMark = "审批节点", SubBy = "赵鹏", StepNo = stepNo++ });
            steps.Add(new WFStep() { BookMark = "研发部负责人", SubBy = "郝明友", StepNo = stepNo++ });
            steps.Add(new WFStep() { BookMark = "抄送财务", SubBy = "财务", StepNo = stepNo++ });

            steps = steps.OrderBy(s => s.StepNo).ToList();
            return new ObservableCollection<WFStep>(steps);
        }

        private void UpdateStep(WFStep obj)
        {
            wfService.UpdateStep(obj);
        }

        private void Export()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "xlsx(*.xlsx)|*.xlsx";
            if (saveFileDialog.ShowDialog() == true)
            {
                var path = saveFileDialog.FileName;
                List<Object> list = new List<object>();
                list.Add(Trial);
                list.Add(TrialEntries);
                ExcelExport.Do(ExcelExport.ExportType.TrialAndEntry, list, path);
            }
           
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
