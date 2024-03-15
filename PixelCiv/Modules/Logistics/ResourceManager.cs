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
        public IComponent Parent { get; set; }
        public bool IsActive { get; set; }

        private List<ResourceStorage> _storageList;

        public ResourceManager() 
        {
            _storageList = new List<ResourceStorage>();
        }

        public void Add(ResourceStorage storage)
        {
            _storageList.Add(storage);
        }

        public float GetCombinedResourceTotal(ResourceType type)
        {
            float amount = 0;

            foreach (ResourceStorage storage in _storageList)
            {
                amount += storage.GetResource(type).Quantity;
            }

            return amount;
        }

        public IEnumerable<T> GetChildren<T>() where T : IComponent
        {
            yield return default;
        }
    }
}
