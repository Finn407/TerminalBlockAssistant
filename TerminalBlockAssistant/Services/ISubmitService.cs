using Aucotec.EngineeringBase.Client.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerminalBlockAssistant.Services
{
    public interface ISubmitService
    {
        string MaterialName();
        ObjectCollection ObjectItems();
        void setMaterialName(string materialName);
        void setObjectItems(ObjectCollection objectItems);

    }
}
