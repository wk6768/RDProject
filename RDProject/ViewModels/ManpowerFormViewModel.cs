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

namespace RDProject.ViewModels
{
    public class ManpowerFormViewModel : BindableBase, INavigationAware
    {
        private readonly IDialogService dialogService;
        private readonly IManpowerService manpowerService;
        private readonly IWFService wfService;

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
                FTitle = $"研发项目试产记录表-{User.DeptName}-{User.Name}-{DateTime.Now.ToString("D")}",
                FStatus = 0,
            };

            ManpowerEntries = new ObservableCollection<ManpowerEntry>();

            Instance = new WFInstance();

            Steps = new ObservableCollection<WFStep>();
        }

        private void InitOldForm(long fHeadID)
        {
            throw new NotImplementedException();
        }

        public ManpowerFormViewModel(IDialogService dialogService, IManpowerService manpowerService, IWFService wfService)
        {
            AddManpowerEntryCommand = new DelegateCommand<string>(AddManpowerEntry);
            DeleteManpowerEntryCommand = new DelegateCommand<object>(DeleteManpowerEntry);
            AddManpowerCommand = new DelegateCommand(AddManpower);
            SaveCommand = new DelegateCommand(Save);
            SendCommand = new DelegateCommand(Send);
            CheckCommand = new DelegateCommand(Check);
            this.dialogService = dialogService;
            this.manpowerService = manpowerService;
            this.wfService = wfService;
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


        private void AddManpower()
        {
            //初始化一个空表单
            InitNewForm();
        }

        private void AddManpowerEntry(string obj)
        {
            dialogService.ShowDialog(obj, callback =>
            {
                if (callback.Parameters.ContainsKey("ManpowerEntries"))
                {
                    var result = callback.Parameters.GetValue<List<ManpowerEntry>>("ManpowerEntries");
                    ManpowerEntries.AddRange(result);
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
            throw new NotImplementedException();
        }

        private void Check()
        {
            throw new NotImplementedException();
        }
    }
}
