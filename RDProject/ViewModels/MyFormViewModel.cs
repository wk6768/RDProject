using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using Prism.Commands;
using Prism.Regions;
using RDProject.Models;
using RDProject.Models.VO;
using RDProject.Services.Interface;
using System.Diagnostics;
using DevExpress.Xpf.WindowsUI;
using System.Windows;

namespace RDProject.ViewModels
{
    public class MyFormViewModel : BindableBase, INavigationAware
    {

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            regionManager.Regions["FormShowControl"].RequestNavigate("EmptyPage");
            User = navigationContext.Parameters["User"] as Employee;
            GetData();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        //获取当前用户所有待审核表单
        void GetData()
        {
            //var list = trialService.GetTrialTitleByCreateUser(User.Name);
            var list = wfService.GetTrialTitleByCreateUser(User.Name, 1);
            TrialTitles = new ObservableCollection<TrialTitle>(list);
        }

        public MyFormViewModel(IRegionManager regionManager, ITrialService trialService, IWFService wfService)
        {
            this.regionManager = regionManager;
            this.trialService = trialService;
            this.wfService = wfService;
            SelectIndexCommand = new DelegateCommand<object>(SelectIndex);
            SearchCommand = new DelegateCommand<object[]>(Search);
        }

        private readonly IRegionManager regionManager;
        private readonly ITrialService trialService;
        private readonly IWFService wfService;

        /// <summary>
        /// 该用户
        /// </summary>
        private Employee user;

        public Employee User
        {
            get { return user; }
            set { user = value; RaisePropertyChanged(); }
        }

        //右侧列表
        private ObservableCollection<TrialTitle> trialTitles;

        public ObservableCollection<TrialTitle> TrialTitles
        {
            get { return trialTitles; }
            set { trialTitles = value; RaisePropertyChanged(); }
        }


        public DelegateCommand<object> SelectIndexCommand { get; private set; }
        public DelegateCommand<object[]> SearchCommand { get; private set; }


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

        private void Search(object[] objs)
        {
            Debug.WriteLine(objs);
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
                default:
                    list = trialService.GetTrialTitleByTitle(title);
                    break;

            }
            TrialTitles = new ObservableCollection<TrialTitle>(list);
        }
    }

    
}
