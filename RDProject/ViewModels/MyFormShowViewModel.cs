using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using Prism.Regions;
using RDProject.Models;
using RDProject.Services.Interface;

namespace RDProject.ViewModels
{
    public class MyFormShowViewModel : BindableBase , INavigationAware
    {
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            var FHeadId = navigationContext.Parameters.GetValue<long>("FHeadId");

            List<TrialEntry> list;
            (Trial, list) = trialService.GetTrialFullData(FHeadId);
            TrialEntries = new ObservableCollection<TrialEntry>(list);

            (Instance, Steps) = wfService.GetInstanceByTableNameAndHeadID("Trial", FHeadId);
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }

        public MyFormShowViewModel(ITrialService trialService, IWFService wfService)
        {
            this.trialService = trialService;
            this.wfService = wfService;
        }

        private readonly ITrialService trialService;
        private readonly IWFService wfService;


        private Trial trial;
        public Trial Trial
        {
            get { return trial; }
            set { trial = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<TrialEntry> trialEntries;
        public ObservableCollection<TrialEntry> TrialEntries
        {
            get { return trialEntries; }
            set { trialEntries = value; RaisePropertyChanged(); }
        }

        private WFInstance instance;
        public WFInstance Instance
        {
            get { return instance; }
            set { instance = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<WFStep> steps;

        public ObservableCollection<WFStep> Steps
        {
            get { return steps; }
            set { steps = value; }
        }


    }
}
