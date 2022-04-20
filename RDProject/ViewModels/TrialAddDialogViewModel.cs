﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using Prism.Commands;
using RDProject.Models;

namespace RDProject.ViewModels
{
    public class TrialAddDialogViewModel : BindableBase, IDialogAware
    {
        public string Title => "新建项";

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
            
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            
        }

        public TrialAddDialogViewModel()
        {
            TrialEntry = new TrialEntry() { FBeginDate=DateTime.Now, FEndDate = DateTime.Now };
            TrialEntries = new List<TrialEntry>();
            AddCommand = new DelegateCommand(Add);
            BackCommand = new DelegateCommand(Back);
        }



        private TrialEntry trialEntry;

        public TrialEntry TrialEntry
        {
            get { return trialEntry; }
            set { trialEntry = value; RaisePropertyChanged(); }
        }

        private List<TrialEntry> trialEntries;

        public List<TrialEntry> TrialEntries
        {
            get { return trialEntries; }
            set { trialEntries = value; RaisePropertyChanged(); }
        }


        public DelegateCommand AddCommand { get; private set; }
        public DelegateCommand BackCommand { get; private set; }

        private void Add()
        {
            TrialEntries.Add(TrialEntry);
            TrialEntry = new TrialEntry() { FBeginDate = DateTime.Now, FEndDate = DateTime.Now };
        }

        private void Back()
        {
            var keys = new DialogParameters();
            keys.Add("TrialEntries", TrialEntries);
            RequestClose?.Invoke(new DialogResult(ButtonResult.OK, keys));
        }
    }
}