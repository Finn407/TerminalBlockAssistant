using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerminalBlockAssistant.Models
{
    public class TextBoxInput
    {
        public string Value { get; set; }
        public TextBoxInput(string value) 
        {
            Value = value;
        }
    }
}
