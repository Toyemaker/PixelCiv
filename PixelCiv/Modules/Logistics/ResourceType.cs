using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelCiv.Modules.Logistics
{
    public class ResourceType
    {
        public string Name { get; private set; }
        public ResourceCategory Category { get; private set; }

        public ResourceType(ResourceCategory category, string name)
        {
            Category = category;
            Name = name;
        }
    }
}
