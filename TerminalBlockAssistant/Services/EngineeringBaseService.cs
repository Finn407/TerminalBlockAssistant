using Aucotec.EngineeringBase.Client.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerminalBlockAssistant.Services
{
    public class EngineeringBaseService : IEngineeringBaseService
    {
        private readonly Application _application;

        public EngineeringBaseService(Application application)
        {
            _application = application;
        }

        public string GetProjectName()
        {
            return _application.RootObject?.Name;
        }
        public Application GetApplication() 
        {
            return _application;
        }
    }
}
