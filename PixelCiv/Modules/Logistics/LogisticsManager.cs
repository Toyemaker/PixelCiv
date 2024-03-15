using Microsoft.Xna.Framework;
using PixelCiv.Core.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelCiv.Modules.Logistics
{
    public class LogisticsManager : IUpdatable
    {
        public IComponent Parent { get; set; }
        public bool IsActive { get; set; }


        private Dictionary<LogisticsRelation, float> _transactionDict;

        public LogisticsManager() 
        {
            _transactionDict = new Dictionary<LogisticsRelation, float>();
        }

        public float this[LogisticsRelation relation]
        {
            get
            {
                if (_transactionDict.ContainsKey(relation))
                {
                    return _transactionDict[relation];
                }

                return 0;
            }
            set
            {
                _transactionDict[relation] = value;
            }
        }

        public void FixedUpdate(float tickInterval)
        {
            foreach (KeyValuePair<LogisticsRelation, float> transaction in _transactionDict)
            {
                transaction.Key.ApplyTransaction(transaction.Value);
            }
        }

        public IEnumerable<T> GetChildren<T>() where T : IComponent
        {
            yield return default;
        }
    }
}
