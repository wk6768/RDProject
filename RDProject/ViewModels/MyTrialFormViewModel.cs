using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using Prism.Commands;
using Prism.Regions;
using Prism.Events;
using RDProject.Models;
using RDProject.Models.VO;
using RDProject.Services.Interface;
using RDProject.Event;
using System.Diagnostics;
using DevExpress.Xpf.WindowsUI;
using System.Windows;

namespace RDProject.ViewModels
{
    public class MyTrialFormViewModel : BindableBase, INavigationAware
    {

        public async void OnNavigatedTo(NavigationContext navigationContext)
        {
            regionManager.Regions["FormShowControl"].RequestNavigate("EmptyPage");
            User = navigationContext.Parameters["User"] as Employee;
            await GetData1();
            await GetData2();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        //获取当前用户所有待审核表单
        async Task GetData1()
        {
            //var list = trialService.GetTrialTitleByCreateUser(User.Name);
            var list1 = await wfService.GetTrialTitleByUserNameAsync(User.Name, 1);
            TrialTitles1 = new ObservableCollection<TrialTitle>(list1);
        }
        //获取所有与当前用户有关的表单
        async Task GetData2()
        {
            var list2 = await wfService.GetTrialTitleByUserNameAsync(User.Name);
            TrialTitles2 = new ObservableCollection<TrialTitle>(list2);
        }

        public MyTrialFormViewModel(IRegionManager regionManager, ITrialService trialService, IWFService wfService, IEventAggregator aggregator)
        {
            this.regionManager = regionManager;
            this.trialService = trialService;
            this.wfService = wfService;
            this.aggregator = aggregator;
            SelectIndexCommand = new DelegateCommand<object>(SelectIndex);
            SearchCommand = new DelegateCommand<object[]>(Search);

            //刷新待审批列表
            aggregator.GetEvent<RefreshTrialTitleListEvent>().Subscribe(async () =>
            {
                await GetData1();
            });
        }

        private readonly IRegionManager regionManager;
        private readonly ITrialService trialService;
        private readonly IWFService wfService;
        private readonly IEventAggregator aggregator;

        /// <summary>
        /// 该用户
        /// </summary>
        private Employee user;

        public Employee User
        {
            get { return user; }
            set { user = value; RaisePropertyChanged(); }
        }

        //右侧列表，一共有三个
        private ObservableCollection<TrialTitle> trialTitles1;

        public ObservableCollection<TrialTitle> TrialTitles1
        {
            get { return trialTitles1; }
            set { trialTitles1 = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<TrialTitle> trialTitles2;

        public ObservableCollection<TrialTitle> TrialTitles2
        {
            get { return trialTitles2; }
            set { trialTitles2 = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<TrialTitle> trialTitles3;

        public ObservableCollection<TrialTitle> TrialTitles3
        {
            get { return trialTitles3; }
            set { trialTitles3 = value; RaisePropertyChanged(); }
        }


        public DelegateCommand<object> SelectIndexCommand { get; private set; }
        public DelegateCommand<object[]> SearchCommand { get; private set; }

        /// <summary>
        /// 点击列表跳转
        /// </summary>
        /// <param name="obj"></param>
        private void SelectIndex(object obj)
        {
            if (obj == null)
            {
                return;
            }
            Debug.WriteLine(obj);
            var trialTitle = (TrialTitle)obj;
            var keys = new NavigationParameters();
            keys.Add("User", User);
            keys.Add("FHeadID", trialTitle.FHeadId);
            regionManager.Regions["FormShowControl"].RequestNavigate("TrialForm", keys);
        }

        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="objs"></param>
        private void Search(object[] objs)
        {
            //Debug.WriteLine(objs);
            foreach(var obj in objs)
            {
                if (obj == null)
                {
                    WinUIMessageBox.Show("请输入搜索条件", "提示", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.None, MessageBoxOptions.None);
                    return;
                }
            }

            var type = objs[0].ToString();
            var title = objs[1].ToString();
            
            int status;
            List<TrialTitle> list;

            switch (type)
            {
                case "草稿":
                    status = 0;
                    list = trialService.GetTrialTitleByTitleAndStatus(title, status);
                    break;
                case "已发起":
                    status = 1;
                    list = trialService.GetTrialTitleByTitleAndStatus(title, status);
                    break;
                case "已结束":
                    status = 3;
                    list = trialService.GetTrialTitleByTitleAndStatus(title, status);
                    break;
                default:
                    list = trialService.GetTrialTitleByTitle(title);
                    break;

            }
            TrialTitles3 = new ObservableCollection<TrialTitle>(list);
        }
    }

    
}
