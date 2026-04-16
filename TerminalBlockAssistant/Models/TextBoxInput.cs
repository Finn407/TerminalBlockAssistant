using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerminalBlockAssistant.Models
{
    public class TextBoxInput
    {
        public string _value { get; set; }
        public TextBoxInput(string value) 
        {
            _value = value;
        }
    }
}
