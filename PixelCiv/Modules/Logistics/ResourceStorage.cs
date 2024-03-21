using PixelCiv.Core.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelCiv.Modules.Logistics
{
    public class ResourceStorage : ITransactionable, IEnumerable<float>, IComponent
    {
        public string Name { get; set; }
        private Dictionary<string, Dictionary<string, float>> _resourceDict;
        public float MaxCapacity { get; set; }
        public IComponent Parent { get; set; }
        public bool IsEnabled { get; set; }

        public ResourceStorage() 
        {
            _resourceDict = new Dictionary<string, Dictionary<string, float>>();
        }

        public float this[string category, string type]
        {
            get
            {
                return (_resourceDict.ContainsKey(category) && _resourceDict[category].ContainsKey(type)) ? _resourceDict[category][type] : 0;
            }
            set
            {
                Add(category, type, value);
            }
        }

        public float GetCapacity()
        {
            float capacity = 0;

            foreach (KeyValuePair<string, Dictionary<string, float>> category in _resourceDict)
            {
                foreach (KeyValuePair<string, float> resource in category.Value)
                {
                    capacity += resource.Value;
                }
            }
            
            return capacity;
        }

        public bool IsTransactionable()
        {
            return GetCapacity() > MaxCapacity;
        }

        public void Add(string category, string type, float quantity = 0)
        {
            if (_resourceDict.ContainsKey(category))
            {
                _resourceDict[category][type] = quantity;
            }
            else
            {
                _resourceDict.Add(category, new Dictionary<string, float>() { { type, quantity } });
            }
        }

        public IEnumerable<T> GetChildren<T>() where T : IComponent
        {
            yield break;
        }

        public IEnumerator<float> GetEnumerator()
        {
            foreach (KeyValuePair<string, Dictionary<string, float>> category in _resourceDict)
            {
                foreach (KeyValuePair<string, float> resource in category.Value)
                {
                    yield return resource.Value;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
