using PixelCiv.Core.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelCiv.Modules.Logistics
{
    public class ResourceManager : IUpdatable
    {
        public string Name { get; set; }
        public IComponent Parent { get; set; }
        public bool IsActive { get; set; }
        public bool IsEnabled { get; set; }

        private List<ResourceStorage> _storageList;

        public ResourceManager() 
        {
            _storageList = new List<ResourceStorage>();
        }

        public void Add(ResourceStorage storage)
        {
            _storageList.Add(storage);
        }
        public void Remove(ResourceStorage storage)
        {
            _storageList.Remove(storage);
        }

        public float GetCombinedResourceTotal(string category, string type)
        {
            float amount = 0;

            foreach (ResourceStorage storage in _storageList)
            {
                amount += storage[category, type];
            }

            return amount;
        }

        public IEnumerable<T> GetChildren<T>() where T : IComponent
        {
            yield break;
        }

        public IComponent Instantiate(IComponent parent)
        {
            ResourceManager manager = new ResourceManager()
            {
                Parent = parent,
                IsActive = IsActive,
                IsEnabled = IsEnabled,
            };

            foreach(ResourceStorage storage in _storageList)
            {
                manager._storageList.Add(storage);
            }

            return manager;
        }
    }
}
