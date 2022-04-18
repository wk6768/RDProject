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

namespace RDProject.ViewModels
{
    public class MyFormViewModel : BindableBase, INavigationAware
    {

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
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

        //获取当前用户所有表单
        void GetData()
        {
            var list = trialService.GetTrialTitleByCreateUser(User.Name);
            TrialTitles = new ObservableCollection<TrialTitle>(list);
        }

        public MyFormViewModel(IRegionManager regionManager, ITrialService trialService)
        {
            this.regionManager = regionManager;
            this.trialService = trialService;

            SelectIndexCommand = new DelegateCommand<object>(SelectIndex);
            SearchCommand = new DelegateCommand<string>(Search);
        }

        private readonly IRegionManager regionManager;
        private readonly ITrialService trialService;

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
        public DelegateCommand<string> SearchCommand { get; private set; }


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

        private void Search(string obj)
        {
            Debug.WriteLine(obj);
            var list = trialService.GetTrialTitleByTitle(obj);
            TrialTitles = new ObservableCollection<TrialTitle>(list);
        }
    }

    
}
