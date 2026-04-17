using Aucotec.EngineeringBase.Client.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TerminalBlockAssistant.Models;
using TerminalBlockAssistant.Services;
using TerminalBlockAssistant.Stores;

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
        public override void Execute(object parameter)
        {

            _application = _engineeringBaseService.GetApplication();
            ObjectItem selectedItem = _application.Selection.FirstOrDefault();
            int count = _countService.GetCount();
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
                    if (_countService.GetIndividualCount() > 0)
                    {
                        for (int i = _countService.GetIndividualCount(); i < count + _countService.GetIndividualCount(); i++)
                        {
                            if ((_application.Selection.FirstOrDefault().Children.FirstOrDefault(x => x.Name == $"Klemme_{i}")) is null)
                            {
                                ObjectItem newItem = selectedMaterial.CopyTo(selectedItem);
                                var attr = newItem.Attributes[4];
                                attr.Value = $"Klemme_{i}";
                                newItem.Store();
                            }
                            else
                            {
                                ErrorMessage += $"Die Klemme {i} wurde bereits angelegt\n";
                            }
                        }
                    }
                    else if (_textBoxInputService.GetTextBoxInputs().Count > 0) 
                    {
                        foreach (TextBoxInput input in _textBoxInputService.GetTextBoxInputs())
                        {
                            if ((_application.Selection.FirstOrDefault().Children.FirstOrDefault(x => x.Name == $"Klemme_{input._value}")) is null)
                            {
                                ObjectItem newItem = selectedMaterial.CopyTo(selectedItem);
                                var attr = newItem.Attributes[4];
                                attr.Value = $"Klemme_{input._value}";
                                newItem.Store();
                            }
                            else
                            {
                                ErrorMessage += $"Die Klemme mit der Klemmnummer {input._value} existiert bereits\n";
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < count; i++)
                        {
                            if ((_application.Selection.FirstOrDefault().Children.FirstOrDefault(x => x.Name == $"Klemme_{i}")) is null)
                            {
                                ObjectItem newItem = selectedMaterial.CopyTo(selectedItem);
                                var attr = newItem.Attributes[4];
                                attr.Value = $"Klemme_{i}";
                                newItem.Store();
                            }
                            else
                            {
                                ErrorMessage += $"Die Klemme {i} wurde bereits angelegt\n";
                            }
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
        }
        public CreateTerminalBlockCommand(IEngineeringBaseService engineeringBaseService, ICountService countService,ISubmitService submitService,ITextBoxInputService textBoxInputService) 
        {
            _engineeringBaseService = engineeringBaseService;
            _countService = countService;
            _submitService = submitService;
            _textBoxInputService = textBoxInputService;
        }  
    }
}
