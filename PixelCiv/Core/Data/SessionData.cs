using PixelCiv.Modules.Logistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelCiv.Core.Data
{
    public class SessionData
    {
        private ResourceManager _resourceManager;
        private LogisticsManager _logisticsManager;

        public SessionData() 
        {
            _resourceManager = new ResourceManager();
            _logisticsManager = new LogisticsManager();
        }

        public void AddResourceStorage(ResourceStorage storage)
        {

        }
    }
}
