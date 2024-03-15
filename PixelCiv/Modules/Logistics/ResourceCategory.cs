using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelCiv.Modules.Logistics
{
    public class ResourceCategory : IEnumerable<ResourceType>
    {
        public static Dictionary<string, ResourceCategory> Categories = new Dictionary<string, ResourceCategory>()
        {
            { 
                "Ore", new ResourceCategory("Ore")
                {
                    "Copper"
                } 
            },
        };

        public string Name { get; private set; }
        private List<ResourceType> _resourceTypes  = new List<ResourceType>();

        public ResourceCategory(string name)
        {
            Name = name;
            _resourceTypes = new List<ResourceType>();
        }

        public void Add(string name)
        {
            if (_resourceTypes.Exists(a => a.Name == name))
            {
                return;
            }

            _resourceTypes.Add(new ResourceType(this, name));
        }

        public ResourceType GetResourceType(string name)
        {
            return _resourceTypes.Find(a => a.Name == name);
        }

        public IEnumerator<ResourceType> GetEnumerator()
        {
            return _resourceTypes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
