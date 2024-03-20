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
        public string Name { get; set; }
        public IComponent Parent { get; set; }
        public bool IsActive { get; set; }
        public bool IsEnabled { get; set; }

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
            yield break;
        }

        public IComponent Instantiate(IComponent parent)
        {
            LogisticsManager logisticsManager = new LogisticsManager()
            {
                Parent = parent,
                IsActive = IsActive,
                IsEnabled = IsEnabled,
            };

            foreach (KeyValuePair<LogisticsRelation, float> pair in _transactionDict)
            {
                logisticsManager._transactionDict.Add(pair.Key, pair.Value);
            }

            return logisticsManager;
        }
    }
}
