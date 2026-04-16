using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerminalBlockAssistant.Models;

namespace TerminalBlockAssistant.Services
{
    public interface ITextBoxInputService
    {
        ObservableCollection<TextBoxInput> GetTextBoxInputs();
        void SetTextBoxInputs(ObservableCollection<TextBoxInput> inputs);
    }
}
