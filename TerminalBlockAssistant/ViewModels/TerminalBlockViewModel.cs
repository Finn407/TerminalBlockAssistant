using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TerminalBlockAssistant.Commands;
using TerminalBlockAssistant.Services;
using TerminalBlockAssistant.Stores;

namespace TerminalBlockAssistant.ViewModels
{
    public class TerminalBlockViewModel:ViewModelBase
    {
        private readonly IEngineeringBaseService _engineeringBaseService;
        private readonly NavigationStore _navigationStore;
        private readonly ICountService _countService;
        private string _text;
        public string Text { get => _text; set { _text = value; _countService.SetCount(Parse(value)); OnPropertyChanged(nameof(Text)); } }
        public ICommand CreateTerminalBlockCommand { get; }


        public TerminalBlockViewModel(IEngineeringBaseService engineeringBaseService, NavigationStore navigationStore,ICountService countService) 
        {
            _engineeringBaseService = engineeringBaseService;
            _navigationStore = navigationStore;
            _countService = countService;
            CreateTerminalBlockCommand = new CreateTerminalBlockCommand(_engineeringBaseService,_navigationStore,_countService);
        }
        public int Parse(string input)
        {
            if (input != null)
            {
                if (int.TryParse(input, out int result))
                {
                    return result;
                }
                else
                {
                    MessageBox.Show("Bitte geben Sie eine Zahl ein", "Fehler",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                    return 0;
                }
            }
            return 0;
        }
    }
}
