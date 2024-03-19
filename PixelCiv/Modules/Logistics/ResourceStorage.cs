using PixelCiv.Core.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelCiv.Modules.Logistics
{
    public class ResourceStorage : ITransactionable, IEnumerable<Resource>, IComponent
    {
        private List<Resource> _resourceList;
        public float MaxCapacity { get; set; }
        public IComponent Parent { get; set; }
        public bool IsEnabled { get; set; }

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

        public void TransferAllResources(ResourceStorage destination)
        {
            foreach (Resource resource in _resourceList)
            {
                destination.Add(resource);
            }
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
        public void Add(Resource resource)
        {
            Add(resource.Type, resource.Quantity);
        }

        public IEnumerator<Resource> GetEnumerator()
        {
            return _resourceList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerable<T> GetChildren<T>() where T : IComponent
        {
            yield break;
        }

        public IComponent Instantiate(IComponent parent)
        {
            ResourceStorage storage = new ResourceStorage()
            {
                Parent = parent,
                MaxCapacity = MaxCapacity,
                IsEnabled = IsEnabled,
            };

            foreach (Resource resource in _resourceList)
            {
                storage.Add(new Resource(resource.Type, resource.Quantity));
            }

            return storage;
        }
    }
}
