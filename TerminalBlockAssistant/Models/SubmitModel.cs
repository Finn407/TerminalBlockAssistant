using Aucotec.EngineeringBase.Client.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerminalBlockAssistant.Models
{
    public class SubmitModel
    {
        public string MaterialName { get; set; }
        public ObjectCollection ObjectItems { get; set; }
        public SubmitModel(string materialName,ObjectCollection objects) 
        {
            MaterialName = materialName;
            ObjectItems = objects;
        }
    }
}
