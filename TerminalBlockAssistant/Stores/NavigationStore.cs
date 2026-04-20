using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerminalBlockAssistant.ViewModels;

namespace TerminalBlockAssistant.Stores
{
    public class NavigationStore
    {
        private ViewModelBase _currentViewModel;
        public ViewModelBase CurrentViewModel 
        { 
            get 
            { 
                return _currentViewModel; 
            } 
            set 
            {
                _currentViewModel = value;

            } 
        }
        public event Action CurrentViewModelChanged;
        private void OnCurrentViewModelChanged() 
        {
            CurrentViewModelChanged?.Invoke();
        }
    }
}
