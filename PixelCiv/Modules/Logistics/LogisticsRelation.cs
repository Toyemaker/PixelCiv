using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelCiv.Modules.Logistics
{
    public struct LogisticsRelation
    {
        private ITransactionable _sender;
        private ITransactionable _receiver;
        private string _resource;

        public LogisticsRelation(ITransactionable sender, ITransactionable receiver, string resource)
        {
            _sender = sender;
            _receiver = receiver;
            _resource = resource;
        }

        public ITransactionable GetSender()
        {
            return _sender;
        }

        public ITransactionable GetReceiver()
        {
            return _receiver;
        }

        public string GetResource()
        {
            return _resource;
        }

        public void ApplyTransaction(float amount)
        {
            float change = amount;

            if (GetSender()[GetResource()] < amount)
            {
                change = GetSender()[GetResource()];

                // log warning
            }

            GetSender()[GetResource()] -= change;
            GetReceiver()[GetResource()] += change;
        }
    }
}
