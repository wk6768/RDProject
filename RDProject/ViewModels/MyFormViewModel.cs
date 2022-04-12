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

        public MyFormViewModel(IRegionManager regionManager, ITrialService trialService )
        {
            this.regionManager = regionManager;
            this.trialService = trialService;

            SelectIndexCommand = new DelegateCommand<object>(SelectIndex);
        }

        private void SelectIndex(object obj)
        {
            Debug.WriteLine(obj);
            var trial = (Trial)obj;
            var keys = new NavigationParameters();
            keys.Add("Trial", trial);
            regionManager.Regions["FormShowControl"].RequestNavigate("MyFormShow", keys);
        }

        private readonly IRegionManager regionManager;
        private readonly ITrialService trialService;


        public DelegateCommand<object> SelectIndexCommand { get; private set; }


        /// <summary>
        /// 该用户
        /// </summary>
        private Employee user;

        public Employee User
        {
            get { return user; }
            set { user = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Trial> trials;
        public ObservableCollection<Trial> Trials
        {
            get { return trials; }
            set { trials = value; RaisePropertyChanged(); }
        }

        void GetData()
        {
            var list = trialService.GetTrialsByCreateUser(User.Name);
            Trials = new ObservableCollection<Trial>(list);
        }

        
        
    }
}
