using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TerminalBlockAssistant.Models;
using TerminalBlockAssistant.Services;
using TerminalBlockAssistant.ViewModels;

namespace TerminalBlockAssistant.Commands
{
    public class SetIndividualTextBoxesCommand : CommandBase
    {
        private readonly ITextBoxInputService _textBoxInputService;
        private readonly ICountService _countService;
        private readonly TerminalBlockViewModel _terminalBlockViewModel;

        public override void Execute(object parameter)
        {

                if (_countService.getIndividualCreateVisibility() == System.Windows.Visibility.Hidden)
                {
                if (_terminalBlockViewModel.GenerateTextBoxes())
                {
                    _terminalBlockViewModel.IndividualCreateVisibility = Visibility.Visible;
                    _terminalBlockViewModel.IndexCreateVisibility = Visibility.Hidden;
                    _terminalBlockViewModel.IndividualText = "";
                }
                }
                else
                {
                    _terminalBlockViewModel.IndividualCreateVisibility = Visibility.Hidden;
                    _terminalBlockViewModel.IndexCreateVisibility = Visibility.Visible;
                    _textBoxInputService.SetTextBoxInputs(new ObservableCollection<TextBoxInput>());
                }
            
            
        }

        public SetIndividualTextBoxesCommand(ITextBoxInputService textBoxInputService, ICountService countService, TerminalBlockViewModel terminalBlockViewModel) 
        {
            _textBoxInputService = textBoxInputService;
            _countService = countService;
            _terminalBlockViewModel = terminalBlockViewModel;
        }
    }
}
