using Aucotec.EngineeringBase.Client.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TerminalBlockAssistant.Models;
using TerminalBlockAssistant.Services;

namespace TerminalBlockAssistant.Commands
{
    public class CreateTerminalBlockCommand : CommandBase
    {
        private readonly IEngineeringBaseService _engineeringBaseService;
        private Aucotec.EngineeringBase.Client.Runtime.Application _application;
        private ICountService _countService;
        private ISubmitService _submitService;
        private ITextBoxInputService _textBoxInputService;
        private string ErrorMessage = "";
        ObjectItem selectedItem;
        ObjectItem selectedMaterial = null;
        public override void Execute(object parameter)
        {
            _application = _engineeringBaseService.GetApplication();
            selectedItem = _application.Selection.FirstOrDefault();
            int count = _countService.GetCount();
            
            foreach (ObjectItem obj in _submitService.ObjectItems()) 
            {
                if (obj.Name == _submitService.MaterialName()) 
                {
                    selectedMaterial = obj;
                }
            }
            if (!(selectedMaterial is null))
            {
                if (selectedItem.TypeId == ObjectType.DevTerminalBlock)
                {
                    if (_countService.GetIndividualCount() > 0)
                    {
                        //Wenn Index Eingabe erfolgt ist
                        for (int i = _countService.GetIndividualCount(); i < count + _countService.GetIndividualCount(); i++)
                        {
                            ErrorMessage += this.createTerminalBlock(i.ToString());
                        }
                    }
                    else if (_textBoxInputService.GetTextBoxInputs().Count > 0)
                    {
                        //Wenn individuelle Benennung gestartet wurde
                        foreach (TextBoxInput input in _textBoxInputService.GetTextBoxInputs())
                        {
                            if (input != null)
                            {
                                ErrorMessage += this.createTerminalBlock(input._value);
                            }
                            else
                            {
                                ErrorMessage += $"Die Eingabe Nr.{input._value} ist leer\n";
                            }
                        }
                    }
                    else
                    {
                        //default
                        for (int i = 0; i < count; i++)
                        {
                            ErrorMessage += this.createTerminalBlock(i.ToString());
                        }
                    }
                    if (ErrorMessage != "")
                    {
                        MessageBox.Show(ErrorMessage, "Fehler",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                        ErrorMessage = "";
                    }
                    _countService.SetCount(count);
                }
                else
                {
                    MessageBox.Show("Es wurde keine Klemme ausgewählt", "Fehler",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                }
            }
            else 
            {
                MessageBox.Show("Es wurde kein Klemmmaterial ausgewählt", "Fehler",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
        public CreateTerminalBlockCommand(IEngineeringBaseService engineeringBaseService, ICountService countService,ISubmitService submitService,ITextBoxInputService textBoxInputService) 
        {
            _engineeringBaseService = engineeringBaseService;
            _countService = countService;
            _submitService = submitService;
            _textBoxInputService = textBoxInputService;
        }
        public string createTerminalBlock(string index) 
        {
            string result = "";
            if ((_application.Selection.FirstOrDefault().Children.FirstOrDefault(x => x.Name == $"Klemme_{index}")) is null)
            {
                ObjectItem newItem = selectedMaterial.CopyTo(selectedItem);
                var attr = newItem.Attributes[4];
                attr.Value = $"Klemme_{index}";
                newItem.Store();
            }
            else
            {
                result += $"Die Klemme {index} wurde bereits angelegt\n";
            }
            return result;
        }
    }
}
