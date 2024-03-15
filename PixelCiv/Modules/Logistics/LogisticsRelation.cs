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
        private ResourceType _type;

        public LogisticsRelation(ITransactionable sender, ITransactionable receiver, ResourceType type)
        {
            _sender = sender;
            _receiver = receiver;
            _type = type;
        }

        public ITransactionable GetSender()
        {
            return _sender;
        }

        public ITransactionable GetReceiver()
        {
            return _receiver;
        }

        public ResourceType GetResource()
        {
            return _type;
        }

        public void ApplyTransaction(float amount)
        {
            float change = amount;

            if (!_receiver.IsTransactionable())
            {
                // log warning
                return;
            }
            else if (_sender.GetResource(_type).Quantity < amount)
            {
                change = _sender.GetResource(_type).Quantity;

                // log warning
            }

            _sender.GetResource(_type).Quantity -= change;
            _receiver.GetResource(_type).Quantity += change;
        }
    }
}
