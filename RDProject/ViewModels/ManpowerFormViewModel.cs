using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Commands;
using Prism.Services.Dialogs;
using RDProject.Services.Interface;
using RDProject.Models;
using RDProject.Models.VO;
using System.Collections.ObjectModel;
using DevExpress.Xpf.WindowsUI;
using System.Windows;
using System.Diagnostics;
using ActivityLibrary.Activities.RDManpower;
using RDProject.Common;
using Prism.Events;
using RDProject.Event;

namespace RDProject.ViewModels
{
    public class ManpowerFormViewModel : BindableBase, INavigationAware
    {
        private readonly IDialogService dialogService;
        private readonly IManpowerService manpowerService;
        private readonly IWFService wfService;
        private readonly IEventAggregator aggregator;

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }

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
                }
                #endregion
            }
        }

        private void InitNewForm()
        {
            Manpower = new Manpower()
            {
                FDate = DateTime.Now,
                //FCompany = "广东紫文星电子科技有限公司",
                FCreateUser = User.Name,
                FTitle = $"研发人员工时统计表-{User.DeptName}-{User.Name}-{DateTime.Now.ToString("D")}",
                FStatus = 0,
            };

            ManpowerEntries = new ObservableCollection<ManpowerEntry>();

            Instance = new WFInstance();

            Steps = new ObservableCollection<WFStep>();
        }

        private void InitOldForm(long fHeadID)
        {
            List<ManpowerEntry> list;
            (Manpower, list) = manpowerService.GetManpowerFullData(fHeadID);
            ManpowerEntries = new ObservableCollection<ManpowerEntry>(list);

            List<WFStep> list2;
            (Instance, list2) = wfService.GetInstanceByTableNameAndHeadID("Manpower", fHeadID);
            if (list2 == null)
            {
                //有表单，但是没有启用审批流
                list2 = new List<WFStep>();
            }
            Steps = new ObservableCollection<WFStep>(list2);
        }

        public ManpowerFormViewModel(IDialogService dialogService, IManpowerService manpowerService, IWFService wfService, IEventAggregator aggregator)
        {
            AddManpowerEntryCommand = new DelegateCommand<string>(AddManpowerEntry);
            DeleteManpowerEntryCommand = new DelegateCommand<object>(DeleteManpowerEntry);
            AddManpowerCommand = new DelegateCommand(AddManpower);
            SaveCommand = new DelegateCommand(Save);
            SendCommand = new DelegateCommand(Send);
            CheckCommand = new DelegateCommand(Check);
            UpdateStepCommand = new DelegateCommand<WFStep>(UpdateStep);
            UpdateManpowerEntryCommand = new DelegateCommand<ManpowerEntry>(UpdateManpowerEntry);
            this.dialogService = dialogService;
            this.manpowerService = manpowerService;
            this.wfService = wfService;
            this.aggregator = aggregator;
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


        #region 功能权限控制
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

        #endregion

        /// <summary>
        /// 发起人
        /// </summary>
        private Employee user;

        public Employee User
        {
            get { return user; }
            set { user = value; RaisePropertyChanged(); }
        }

        private Manpower manpower;

        public Manpower Manpower
        {
            get { return manpower; }
            set { manpower = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<ManpowerEntry> manpowerEntries;
        public ObservableCollection<ManpowerEntry> ManpowerEntries
        {
            get { return manpowerEntries; }
            set { manpowerEntries = value; RaisePropertyChanged(); }
        }


        public DelegateCommand<string> AddManpowerEntryCommand { get; private set; }
        public DelegateCommand<object> DeleteManpowerEntryCommand { get; private set; }
        public DelegateCommand AddManpowerCommand { get; private set; }
        public DelegateCommand SaveCommand { get; private set; }
        public DelegateCommand SendCommand { get; private set; }
        public DelegateCommand CheckCommand { get; private set; }
        public DelegateCommand<WFStep> UpdateStepCommand { get; private set; }
        public DelegateCommand<ManpowerEntry> UpdateManpowerEntryCommand { get;private set; }

        private void AddManpower()
        {
            //初始化一个空表单
            InitNewForm();
        }

        private void AddManpowerEntry(string obj)
        {
            dialogService.ShowDialog(obj, callback =>
            {
                if (callback.Result == ButtonResult.OK)
                {
                    if (callback.Parameters.ContainsKey("ManpowerEntries"))
                    {
                        var result = callback.Parameters.GetValue<List<ManpowerEntry>>("ManpowerEntries");
                        ManpowerEntries.AddRange(result);
                    }
                }
            });
        }


        private void DeleteManpowerEntry(object obj)
        {
            var entry = obj as ManpowerEntry;
            ManpowerEntries.Remove(entry);
        }

        private void Save()
        {
            if(string.IsNullOrWhiteSpace(Manpower.FCompany) || string.IsNullOrWhiteSpace(Manpower.FDate.ToShortDateString()))
            {
                WinUIMessageBox.Show("单位、日期 为必填项", "提示", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.None, MessageBoxOptions.None);
                return;
            }

            //如果编号FHeadID为null或者0，则保存；如果FHeadID 大于0，则更新
            //保存
            switch (Manpower.FHeadId)
            {
                case 0:
                    Debug.WriteLine("新增表单");
                    (Manpower, ManpowerEntries) = manpowerService.SaveManpowerPageAndReturnFullData(Manpower, ManpowerEntries);
                    if (Manpower.FHeadId > 0)
                    {
                        WinUIMessageBox.Show("保存成功", "提示", MessageBoxButton.OK, MessageBoxImage.Asterisk, MessageBoxResult.None, MessageBoxOptions.None);
                    }
                    break;
                default:
                    Debug.WriteLine("更新表单");
                    (Manpower, ManpowerEntries) = manpowerService.UpdateManpowerPageAndReturnFullData(Manpower, ManpowerEntries);
                    WinUIMessageBox.Show("修改成功", "提示", MessageBoxButton.OK, MessageBoxImage.Asterisk, MessageBoxResult.None, MessageBoxOptions.None);
                    break;
            }
            
        }

        private void Send()
        {
            if (Steps.Count > 0)
            {
                WinUIMessageBox.Show("重复发起审批", "提示", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.None, MessageBoxOptions.None);
                return;
            }
            var activity = new 研发人员工时统计表();

            //开启一个工作流，获取工作流ID
            var guid = WFHelper.Run(activity);
            Debug.WriteLine(guid);

            //更新表单为审核状态
            Manpower.FStatus = 1;
            (Manpower, _) = manpowerService.UpdateManpowerPageAndReturnFullData(Manpower, null);

            //构建步骤表
            Steps = CreateSteps();

            //构建审批表
            var step = Steps.Where(s => s.Status != 1).FirstOrDefault();//下一步
            WFInstance instance = new WFInstance()
            {
                TableName = "Manpower",
                InstanceGuid = guid,
                HeadId = Manpower.FHeadId,
                SubBy = Manpower.FCreateUser,
                Status = 1,
                NextName = step == null ? null : step.SubBy,
            };

            //保存
            (instance, Steps) = wfService.SaveInstance(instance, Steps);

            //更新工作流
            _ = Task.Run(async () =>
            {
                await Task.Delay(5000);
                WFHelper.Resume(
                    activity,
                    guid,
                    "提交申请",
                    null
                );
            });
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
                    if (step2 == null)
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
                    Manpower.FStatus = 3;
                    (Manpower, _) = manpowerService.UpdateManpowerPageAndReturnFullData(Manpower, null);
                }

                //设置下一审批人
                var step3 = Steps.Where(s => s.Status != 1).FirstOrDefault();
                Instance.NextName = step3 == null ? null : step3.SubBy;

                //更新审批表和步骤表
                (Instance, Steps) = wfService.UpdateInstance(Instance, Steps);


                //执行工作流
                Dictionary<string, object> keys2 = new Dictionary<string, object>();
                keys2.Add("IsPass", true);
                keys2.Add("BookMarkName", step.BookMark);
                WFHelper.Resume(
                        new 研发人员工时统计表(),
                        Instance.InstanceGuid,
                        step.BookMark,
                        keys2
                    );
            }
            //驳回
            if (CheckResultReject == true)
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
                        new 研发人员工时统计表(),
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
                foreach (var item in rejectSteps)
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

        private ObservableCollection<WFStep> CreateSteps()
        {
            var stepNo = 1;
            List<WFStep> steps = new List<WFStep>();
            steps.Add(new WFStep() { BookMark = "提交申请", SubBy = User.Name, SubTime = DateTime.Now, Remark = "发起流程", StepNo = stepNo++, Status = 1 });
            steps.Add(new WFStep() { BookMark = "研发部负责人", SubBy = "郝明友", StepNo = stepNo++ });

            steps = steps.OrderBy(s => s.StepNo).ToList();
            return new ObservableCollection<WFStep>(steps);
        }

        private void UpdateStep(WFStep obj)
        {
            wfService.UpdateStep(obj);
        }


        private void UpdateManpowerEntry(ManpowerEntry obj)
        {
            obj.FTotalHours = obj.FAttendanceHours + obj.FNormalOvertimeHours
                + obj.FWeekendOvertimeHours + obj.FHolidayOvertimeHours;
            obj.FVarianceHours = obj.FTotalHours - obj.FRD28Hours - obj.FRD30Hours
                - obj.FRD31Hours - obj.FRD32Hours - obj.FRD33Hours - obj.FRD34Hours;
            manpowerService.UpdateManpowerEntry(obj);
        }

    }
}
