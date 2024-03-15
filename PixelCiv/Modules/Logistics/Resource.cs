using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelCiv.Modules.Logistics
{
    public class Resource
    {
        public ResourceType Type { get; private set; }
        public float Quantity { get; set; }

        public Resource(ResourceType type, float quantity = 0)
        {
            Type = type;
            Quantity = quantity;
        }
    }
}
