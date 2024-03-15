using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelCiv.Modules.Logistics
{
    public interface ITransactionable
    {
        Resource GetResource(ResourceType type);
        bool IsTransactionable();
    }
}
