using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TerminalBlockAssistant.Services
{
    public interface ICountService
    {
        int GetCount();
        void SetCount(int value);
        int GetIndividualCount();
        void SetIndividualCount(int value);
        void SetIndividualCount(string value);
        void SetCount(string value);
        Visibility getIndividualCreateVisibility();
        void setIndividualCreateVisibility(Visibility value);
        Visibility getIndexCreateVisibility();
        void setIndexCreateVisibility(Visibility value);
    }
}
