using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelCiv.Modules.Logistics
{
    public class ResourceStorage : ITransactionable
    {
        private Dictionary<string, float> _resourceDict;
        public float MaxCapacity { get; set; }

        public float this[string resource]
        {
            get
            {
                return _resourceDict[resource];
            }
            set
            {
                _resourceDict[resource] = value;
            }
        }

        public ResourceStorage() 
        {
            _resourceDict = new Dictionary<string, float>();
        }

        public float GetCapacity()
        {
            float capacity = 0;

            foreach (float amount in _resourceDict.Values)
            {
                capacity += amount;
            }
            
            return capacity;
        }
        public bool IsFull()
        {
            return GetCapacity() > MaxCapacity;
        }
    }
}
