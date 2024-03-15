using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelCiv.Modules.Logistics
{
    public class ResourceManager
    {
        private List<ResourceStorage> _storageList;

        public ResourceManager() 
        {
            _storageList = new List<ResourceStorage>();
        }

        public void Add(ResourceStorage storage)
        {
            _storageList.Add(storage);
        }

        public float GetCombinedResourceTotal(string resource)
        {
            float amount = 0;

            foreach (ResourceStorage storage in _storageList)
            {
                amount += storage[resource];
            }

            return amount;
        }
    }
}
