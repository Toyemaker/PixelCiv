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
        private int _counter;


        public Warehouse()
        {
            AddComponent("storage", new ResourceStorage()
            {
                
            });

            GetChild<Sprite2D>("sprite").Color = Color.Green;
        }

        public override void FixedUpdate(float tickInterval)
        {
            _counter++;
            if (_counter >= 40)
            {
                GetChild<ResourceStorage>("storage")["Ore", "Copper"] += 1;
                _counter -= 40;
            }            

            base.FixedUpdate(tickInterval);
        }
    }
}
