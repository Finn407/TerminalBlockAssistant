using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            _terminalBlockViewModel.GenerateTextBoxes();
        }

        public SetIndividualTextBoxesCommand(ITextBoxInputService textBoxInputService, ICountService countService, TerminalBlockViewModel terminalBlockViewModel) 
        {
            _textBoxInputService = textBoxInputService;
            _countService = countService;
            _terminalBlockViewModel = terminalBlockViewModel;
        }
    }
}
