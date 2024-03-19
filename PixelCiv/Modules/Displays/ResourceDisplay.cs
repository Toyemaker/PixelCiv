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

        private Dictionary<ResourceType, Text2D> _resourceTextDict;

        public ResourceDisplay(ResourceManager manager)
        {
            ResourceManager = manager;
            _resourceTextDict = new Dictionary<ResourceType, Text2D>();

            int height = 0;

            foreach (ResourceCategory category in ResourceCategory.Categories.Values)
            {
                foreach (ResourceType type in category)
                {
                    Text2D text = new Text2D(GameData.BaseFont, "");
                    text.Transform.Position = new Vector2(0, 20 * height++);

                    _resourceTextDict.Add(type, text);
                    AddComponent(type.Name, text);
                }
            }
        }

        public override void FixedUpdate(float tickInterval)
        {
            foreach (ResourceCategory category in ResourceCategory.Categories.Values)
            {
                foreach (ResourceType type in category)
                {
                    _resourceTextDict[type].Text = type.Name + ": " + ResourceManager.GetCombinedResourceTotal(type);
                }
            }

            base.FixedUpdate(tickInterval);
        }
    }
}
