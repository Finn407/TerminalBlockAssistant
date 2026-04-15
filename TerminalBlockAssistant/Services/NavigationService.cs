using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerminalBlockAssistant.Stores;
using TerminalBlockAssistant.ViewModels;

namespace TerminalBlockAssistant.Services
{
    public class NavigationService
    {
        private readonly NavigationStore _navigationStore;
        private Func<ViewModelBase> _createViewModel;
        public NavigationService(NavigationStore navigationStore, Func<ViewModelBase> createViewModel)
        {
            _navigationStore = navigationStore;
            _createViewModel = createViewModel;
        }
        public void Navigate()
        {
            _navigationStore.CurrentViewModel = _createViewModel();
        }
    }
}
