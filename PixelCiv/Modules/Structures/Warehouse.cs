using Microsoft.Xna.Framework;
using PixelCiv.Core.Components;
using PixelCiv.Core.Data;
using PixelCiv.Core.Graphics;
using PixelCiv.Modules.Logistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelCiv.GameObjects.Structures
{
    public class Warehouse : Structure
    {
        public ResourceStorage Storage { get; private set; }
        private int _counter;


        public Warehouse()
        {
            Storage = new ResourceStorage()
            {
                { ResourceCategory.Categories["Ore"].GetResourceType("Copper") }
            };
            _sprite.Color = Color.Green;
        }

        public override void FixedUpdate(float tickInterval)
        {
            _counter++;
            if (_counter >= 40)
            {
                Storage.GetResource(ResourceCategory.Categories["Ore"].GetResourceType("Copper")).Quantity += 1;
                _counter -= 40;
            }            

            base.FixedUpdate(tickInterval);
        }
    }
}
