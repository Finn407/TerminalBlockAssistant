using Aucotec.EngineeringBase.Client.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TerminalBlockAssistant.Services;

namespace TerminalBlockAssistant.Commands
{
    public class SetTerminalBlocksWithIndexCommand : CommandBase
    {
        private readonly IEngineeringBaseService _engineeringBaseService;
        private Aucotec.EngineeringBase.Client.Runtime.Application _application;
        private ICountService _countService;
        private ISubmitService _submitService;
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
                    for (int i = 0; i+_countService.GetIndividualCount() < count + _countService.GetIndividualCount(); i++)
                    {


                        if ((_application.Selection.FirstOrDefault().Children.FirstOrDefault(x => x.Name == $"Klemme_{i+_countService.GetIndividualCount()}")) is null)
                        {
                            ObjectItem newItem = selectedMaterial.CopyTo(selectedItem);
                            var attr = newItem.Attributes[4];
                            attr.Value = $"Klemme_{i+_countService.GetIndividualCount()}";
                            newItem.Store();
                        }
                        else
                        {
                            ErrorMessage += $"Die Klemme {i + _countService.GetIndividualCount()} wurde bereits angelegt\n";
                        }


                    }
                    if (ErrorMessage != "")
                    {
                        MessageBox.Show(ErrorMessage, "Fehler",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
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
        public SetTerminalBlocksWithIndexCommand(IEngineeringBaseService engineeringBaseService, ICountService countService, ISubmitService submitService)
        {
            _engineeringBaseService = engineeringBaseService;
            _countService = countService;
            _submitService = submitService;
        }
    }
}
