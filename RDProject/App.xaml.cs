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
            containerRegistry.RegisterDialog<LoginDialog, LoginDialogViewModel>();
            containerRegistry.RegisterDialog<ChangePwdDialog, ChangePwdDialogViewModel>();
            containerRegistry.RegisterDialog<TrialAddDialog, TrialAddDialogViewModel>();
            containerRegistry.RegisterDialog<CheckDialog, CheckDialogViewModel>();
            containerRegistry.RegisterForNavigation<EmployeeControl, EmployeeControlViewModel>();
            containerRegistry.RegisterForNavigation<TrialForm, TrialFormViewModel>();
            containerRegistry.RegisterForNavigation<MyForm, MyFormViewModel>();
            containerRegistry.RegisterForNavigation<WelcomePage, WelcomePageViewModel>();
            containerRegistry.RegisterForNavigation<EmptyPage, EmptyPageViewModel>();
        }
    }
}
