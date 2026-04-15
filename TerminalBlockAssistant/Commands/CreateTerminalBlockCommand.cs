using Aucotec.EngineeringBase.Client.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
        public override void Execute(object parameter)
        {
            _application = _engineeringBaseService.GetApplication();
            ObjectItem selectedItem = _application.Selection.FirstOrDefault();
            int count = _countService.GetCount();

            if (selectedItem.TypeId == ObjectType.DevTerminalBlock)
            {
                for (int i = 0; i < count; i++) 
                {
                    ObjectItem temp = selectedItem.NewChild(ObjectKind.Device,ObjectType.DevTerminal);
                    //temp.Attributes.FindById(AttributeId.).Value=
                    temp.Store();
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
        public CreateTerminalBlockCommand(IEngineeringBaseService engineeringBaseService,NavigationStore navigationStore, ICountService countService) 
        {
            _engineeringBaseService = engineeringBaseService;
            _navigationStore = navigationStore;
            _countService = countService;
        }
        
    }
}
