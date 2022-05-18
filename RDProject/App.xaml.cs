using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using Prism.DryIoc;
using Prism.Ioc;
using RDProject.Views;
using RDProject.ViewModels;
using RDProject.Services;
using RDProject.Services.Interface;
using RDProject.Common;

namespace RDProject
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainView>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterScoped(typeof(MyDbContext), () => new MyDbContext());
            containerRegistry.RegisterScoped<IEmployeeService, EmployeeService>();
            containerRegistry.RegisterScoped<ITrialService, TrialService>();
            containerRegistry.RegisterScoped<IWFService, WFService>();
            containerRegistry.RegisterScoped<ISerialNumberService, SerialNumberService>();
            containerRegistry.RegisterScoped<IManpowerService, ManpowerService>();

            containerRegistry.RegisterDialog<LoginDialog, LoginDialogViewModel>();
            containerRegistry.RegisterDialog<ChangePwdDialog, ChangePwdDialogViewModel>();
            containerRegistry.RegisterDialog<TrialAddDialog, TrialAddDialogViewModel>();
            containerRegistry.RegisterDialog<CheckDialog, CheckDialogViewModel>();
            containerRegistry.RegisterDialog<ManpowerAddDialog, ManpowerAddDialogViewModel>();

            containerRegistry.RegisterForNavigation<EmployeeControl, EmployeeControlViewModel>();
            containerRegistry.RegisterForNavigation<TrialForm, TrialFormViewModel>();
            containerRegistry.RegisterForNavigation<MyTrialForm, MyTrialFormViewModel>();
            containerRegistry.RegisterForNavigation<TrialStatistics, TrialStatisticsViewModel>();
            containerRegistry.RegisterForNavigation<ManpowerForm, ManpowerFormViewModel>();

            containerRegistry.RegisterForNavigation<WelcomePage, WelcomePageViewModel>();
            containerRegistry.RegisterForNavigation<EmptyPage, EmptyPageViewModel>();
        }
    }
}
