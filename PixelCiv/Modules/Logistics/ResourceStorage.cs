using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelCiv.Modules.Logistics
{
    public class ResourceStorage : ITransactionable, IEnumerable<Resource>
    {
        private List<Resource> _resourceList;
        public float MaxCapacity { get; set; }

        public ResourceStorage() 
        {
            _resourceList = new List<Resource>();
        }

        public float GetCapacity()
        {
            float capacity = 0;

            foreach (Resource resource in _resourceList)
            {
                capacity += resource.Quantity;
            }
            
            return capacity;
        }
        public Resource GetResource(ResourceType type)
        {
            return _resourceList.Find(a => a.Type == type);
        }

        public bool IsTransactionable()
        {
            return GetCapacity() > MaxCapacity;
        }

        public void Add(ResourceType type, float quantity = 0)
        {
            if (_resourceList.Exists(a => a.Type != type))
            {
                // log warning

                return;
            }

            _resourceList.Add(new Resource(type, quantity));
        }

        public IEnumerator<Resource> GetEnumerator()
        {
            return _resourceList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
