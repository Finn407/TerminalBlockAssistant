using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerminalBlockAssistant.Services
{
    public interface ICountService
    {
        int GetCount();
        void SetCount(int value);
        int GetIndividualCount();
        void SetIndividualCount(int value);
    }
}
