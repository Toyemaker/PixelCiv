using PixelCiv.Core.Components;
using PixelCiv.Core.UI;
using PixelCiv.GameObjects;
using PixelCiv.GameObjects.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelCiv.Modules.Displays
{
    public class BuildingMenu : GameObject
    {
        public BuildingMenu()
        {
            AddComponent("categoryBar", new GameObject());
            GetChild<GameObject>("categoryBar").AddComponent("categoryAll", new Button());

            AddComponent("structureBar", new GameObject());
        }
    }
}
