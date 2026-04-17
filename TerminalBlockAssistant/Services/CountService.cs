using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerminalBlockAssistant.Services
{
    public class CountService : ICountService
    {
        private int _count;
        public int Count { get { return _count; } set { _count = value; } }
        private int _individualCount;
        public int IndividualCount { get { return _individualCount; } set { _individualCount = value; } }
        public int GetCount()
        {
            return _count;
        }

        public void SetCount(int value)
        {
            Count = value;
        }
        public int GetIndividualCount() 
        {
            return _individualCount;
        }
        public void SetIndividualCount(int value)
        {
            IndividualCount = value;
        }
        public CountService() 
        {
            Count = 0;
        }
    }
}
