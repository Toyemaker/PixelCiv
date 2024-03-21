using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PixelCiv.Core.Components;
using PixelCiv.Core.Data;
using PixelCiv.Core.UI;
using PixelCiv.Modules.Logistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelCiv.Modules.Displays
{
    public class ResourceDisplay : GameObject
    {
        public ResourceManager ResourceManager { get; set; }

        private Dictionary<string, Dictionary<string, Text2D>> _resourceTextDict;

        public ResourceDisplay(ResourceManager manager)
        {
            ResourceManager = manager;

            _resourceTextDict = new Dictionary<string, Dictionary<string, Text2D>>
            {
                { "Ore", new Dictionary<string, Text2D>() }
            };

            _resourceTextDict["Ore"].Add("Copper", new Text2D(GameData.BaseFont));

            foreach (KeyValuePair<string, Dictionary<string, Text2D>> category in _resourceTextDict)
            {
                foreach (KeyValuePair<string, Text2D> text2D in category.Value)
                {
                    AddComponent("text" + category.Key + text2D.Key, _resourceTextDict[category.Key][text2D.Key]);
                }
            }
        }

        public override void FixedUpdate(float tickInterval)
        {
            foreach (KeyValuePair<string, Dictionary<string, Text2D>> category in _resourceTextDict)
            {
                foreach (KeyValuePair<string, Text2D> text2D in category.Value)
                {
                    text2D.Value.Text = text2D.Key + ": " + ResourceManager.GetCombinedResourceTotal(category.Key, text2D.Key);
                }
            }

            base.FixedUpdate(tickInterval);
        }
    }
}
