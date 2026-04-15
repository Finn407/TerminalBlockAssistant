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
        private readonly NavigationStore _navigationStore;
        private Aucotec.EngineeringBase.Client.Runtime.Application _application;
        private ICountService _countService;
        private ISubmitService _submitService;
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
                    for (int i = 0; i < count; i++)
                    {
                        
                        //selectedMaterial.Name = i.ToString();
                        selectedMaterial.CopyTo(selectedItem);
                        selectedMaterial.Store();
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
        public CreateTerminalBlockCommand(IEngineeringBaseService engineeringBaseService,NavigationStore navigationStore, ICountService countService,ISubmitService submitService) 
        {
            _engineeringBaseService = engineeringBaseService;
            _navigationStore = navigationStore;
            _countService = countService;
            _submitService = submitService;
        }
        
    }
}
