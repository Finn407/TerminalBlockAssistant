using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerminalBlockAssistant.Models;

namespace TerminalBlockAssistant.Services
{
    public class TextBoxInputService : ITextBoxInputService
    {
        public ObservableCollection<TextBoxInput> TextBoxInputs { get; private set; }
        public ObservableCollection<TextBoxInput> GetTextBoxInputs()
        {
            return TextBoxInputs;
        }

        public void SetTextBoxInputs(ObservableCollection<TextBoxInput> inputs)
        {
            TextBoxInputs = inputs;
        }
    }
}
