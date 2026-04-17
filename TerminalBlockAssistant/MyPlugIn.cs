using Aucotec.EngineeringBase.Client.Runtime;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.AddIn;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Threading;
using TerminalBlockAssistant.Services;
using TerminalBlockAssistant.Stores;
using TerminalBlockAssistant.ViewModels;
using Wpf.Ui.Appearance;

namespace TerminalBlockAssistant
{
    /// <summary>
    /// Implements Wizard TerminalBlockAssistant
    /// </summary>
    [AddIn("TerminalBlockAssistant", Description = "", Publisher = "f.rademaker")]
    public class MyPlugIn : PlugInWizard
    {
        /// <summary>
        /// Runs the wizard.
        /// </summary>
        /// <param name="myApplication">Application object instance</param>	
        private IServiceCollection _services;
        private NavigationStore _navigationStore;
        private IEngineeringBaseService _engineeringBaseService;
        private ICountService _countService;
        private ISubmitService _submitService;
        private ITextBoxInputService _textBoxInputService;
        public override void Run(Aucotec.EngineeringBase.Client.Runtime.Application myApplication)
        {

            _services = new ServiceCollection();
            _services.AddSingleton(myApplication);
            _services.AddSingleton<IEngineeringBaseService, EngineeringBaseService>();
            _services.AddSingleton<NavigationStore>();
            _services.AddTransient<MainViewModel>();
            _services.AddSingleton<ICountService, CountService>();
            _services.AddSingleton<ISubmitService, SubmitService>();
            _services.AddSingleton<ITextBoxInputService, TextBoxInputService>();

            IServiceProvider serviceProvider = _services.BuildServiceProvider();

            _engineeringBaseService = serviceProvider.GetService<IEngineeringBaseService>();
            _navigationStore = serviceProvider.GetService<NavigationStore>();
            _countService = serviceProvider.GetService<ICountService>();
            _submitService = serviceProvider.GetService<ISubmitService>();
            _textBoxInputService = serviceProvider.GetService<ITextBoxInputService>();  
            _navigationStore.CurrentViewModel = createTerminalBlockViewModel();



            MainWindow window = new MainWindow();


            window.DataContext = serviceProvider.GetService<MainViewModel>();

            ApplicationThemeManager.Apply(ApplicationTheme.Dark);

            window.ShowDialog();



            // Make a synchronously shutdown
            if (!AppDomain.CurrentDomain.IsDefaultAppDomain())
                Dispatcher.CurrentDispatcher.InvokeShutdown();
        }
        public TerminalBlockViewModel createTerminalBlockViewModel()
        {
            return new TerminalBlockViewModel(_engineeringBaseService,_navigationStore, _countService,_submitService,_textBoxInputService);
        }
    }

}

