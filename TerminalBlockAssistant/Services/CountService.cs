using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TerminalBlockAssistant.Services
{
    public class CountService : ICountService
    {
        private int _count;
        public int Count 
        { 
            get 
            { 
                return _count; 
            } 
            set 
            { 
                _count = value; 
            } 
        }
        private int _individualCount;
        public int IndividualCount 
        { 
            get 
            { 
                return _individualCount; 
            }
            set 
            { 
                _individualCount = value; 
            } 
        }
        public Visibility _individualCreateVisibility { get; set; }
        public Visibility _indexCreateVisibility { get; set; }
        public int GetCount()
        {
            return _count;
        }
        public void SetCount(string count) 
        {
            int.TryParse(count, out _count);
        }
        public void SetIndividualCount(string count)
        {
            int.TryParse(count, out _individualCount);
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
        public Visibility getIndividualCreateVisibility()
        {
            return _individualCreateVisibility;
        }
        public void setIndividualCreateVisibility(Visibility value)
        {
            _individualCreateVisibility = value;
        }
        public Visibility getIndexCreateVisibility()
        {
            return _indexCreateVisibility;
        }
        public void setIndexCreateVisibility(Visibility value)
        {
            _indexCreateVisibility = value;
        }
        public CountService() 
        {
            Count = 0;
        }
    }
}
