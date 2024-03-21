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
        private string _category;
        private string _type;

        public LogisticsRelation(ITransactionable sender, ITransactionable receiver, string category, string type)
        {
            _sender = sender;
            _receiver = receiver;
            _category = category;
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

        public void ApplyTransaction(float amount)
        {
            float change = amount;

            if (!_receiver.IsTransactionable())
            {
                // log warning
                return;
            }
            else if (_sender[_category, _type] < amount)
            {
                change = _sender[_category, _type];

                // log warning
            }

            _sender[_category, _type] -= change;
            _receiver[_category, _type] += change;
        }
    }
}
