using Aucotec.EngineeringBase.Client.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerminalBlockAssistant.Services
{
    public class SubmitService : ISubmitService
    {
        public string _materialName { get; set; }
        public ObjectCollection _objectItems { get; set; }
        public string MaterialName()
        {
            return _materialName;
        }

        public ObjectCollection ObjectItems()
        {
            return _objectItems;
        }

        public void setMaterialName(string materialName)
        {
            _materialName = materialName;
        }

        public void setObjectItems(ObjectCollection objectItems)
        {
            _objectItems = objectItems;
        }
    }
}
