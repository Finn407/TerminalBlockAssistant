using Aucotec.EngineeringBase.Client.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TerminalBlockAssistant.Models;
using TerminalBlockAssistant.Services;
using Application = Aucotec.EngineeringBase.Client.Runtime.Application;

namespace TerminalBlockAssistant.Commands
{
    public class SetIndividualNamesCommand : CommandBase
    {
        private readonly ITextBoxInputService _textBoxInputService;
        private readonly ICountService _countService;
        private readonly IEngineeringBaseService _engineeringBaseService;
        private readonly ISubmitService _submitService;
        private Application _application;
        public override void Execute(object parameter)
        {

                foreach (TextBoxInput input in _textBoxInputService.GetTextBoxInputs())
                {
                if (input._value != "")
                {
                    _application = _engineeringBaseService.GetApplication();
                    ObjectItem selectedItem = _application.Selection.FirstOrDefault();
                    ObjectItem selectedMaterial = null;
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
                            if ((_application.Selection.FirstOrDefault().Children.FirstOrDefault(x => x.Name == $"Klemme_{input._value}")) is null)
                            {
                                ObjectItem newItem = selectedMaterial.CopyTo(selectedItem);
                                var attr = newItem.Attributes[4];
                                attr.Value = $"Klemme_{Parse(input._value)}";
                                newItem.Store();
                            }
                            else
                            {
                                MessageBox.Show($"Die Klemme mit der Klemmnummer {input._value} existiert bereits", "Fehler",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
                            }
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
                        MessageBox.Show("Es wurde kein Material ausgewählt", "Fehler",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error);
                    }
                }
                else 
                {
                    MessageBox.Show("Die Eingabe war leer", "Fehler",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error);
                }
  
                }
            
        }
        public SetIndividualNamesCommand(ITextBoxInputService textBoxInputService, ICountService countService, IEngineeringBaseService engineeringBaseService, ISubmitService submitService)  
        {
            _textBoxInputService = textBoxInputService;
            _countService = countService;
            _engineeringBaseService = engineeringBaseService;
            _submitService = submitService;
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
