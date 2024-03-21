using Microsoft.Xna.Framework;
using PixelCiv.Core;
using PixelCiv.Core.Components;
using PixelCiv.Core.Data;
using PixelCiv.Core.Graphics;
using PixelCiv.Core.UI;
using PixelCiv.GameObjects.Structures;
using PixelCiv.Modules.Logistics;
using PixelCiv.Modules.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelCiv.Modules.Displays
{
    public class BuildingMenu : GameObject
    {
        public HexGrid HexGrid { get; set; }

        private float _categoryBarWidth;
        private float _categoryBarHeight;
        private float _structureBarWidth;

        public BuildingMenu(HexGrid hexGrid)
        {
            HexGrid = hexGrid;

            AddComponent("categoryBar", new GameObject());
            AddComponent("structureBar", new GameObject());

            AddCategory("All");
            AddCategory("Ore");
            AddCategory("Housing");

            AddStructure<Warehouse>("Warehouse", "categoryAll", "categoryOre", "categoryHousing");
            AddStructure<Warehouse>("Gathering Hut", "categoryAll", "categoryHousing");
            AddStructure<Warehouse>("Copper Mine", "categoryAll", "categoryOre");
            GetChild<GameObject>("structureBar").Transform.Position = new Vector2(0, _categoryBarHeight);            
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public void ChangeBuildingCategory(Input input, GameTime gameTime, Button button)
        {
            int x = 0;

            foreach (GameObject obj in GetChild<GameObject>("structureBar").GetChildren<GameObject>())
            {
                if (obj.HasAttribute(button.Parent.Name))
                {
                    obj.Transform.Position = new Vector2(x++ * 64, 0);
                    obj.IsEnabled = true;
                }
                else
                {
                    obj.IsEnabled = false;
                }
            }
        }

        public void AddStructure<T>(string name, params string[] categories) where T : Structure
        {
            GameObject obj = new GameObject();
            {
                obj.Transform.Position = new Vector2(_structureBarWidth, 0);
                _structureBarWidth += 64;
                foreach (string str in categories)
                {
                    obj.AddAttribute(str);
                }
                Button button = new Button(new Sprite2D(GameData.Pixel));
                {
                    button.Transform.Scale = new Vector2(64);
                    button.OnInteractEvent += (a, b, c) =>
                    {
                        HexGrid.AddComponent("placeStructure", Activator.CreateInstance<T>());
                    };
                    obj.AddComponent("button", button);
                }
                Sprite2D sprite = new Sprite2D(GameData.BaseHouseTexture);
                {
                    sprite.Transform.Scale = new Vector2(4);
                    sprite.Color = Color.Red;
                    obj.AddComponent("sprite", sprite);
                }
                GetChild<GameObject>("structureBar").AddComponent(name, obj);
            }
        }

        public void AddCategory(string name)
        {
            GameObject category = new GameObject();
            {
                Vector2 vec = GameData.BaseFont.MeasureString(name);

                int xPadding = 16;
                int yPadding = 4;

                Sprite2D buttonSprite = new Sprite2D(GameData.Pixel);
                buttonSprite.Transform.Scale = new Vector2(vec.X + 2 * xPadding, vec.Y + 2 * yPadding);
                buttonSprite.Color = Color.Gray;
                Button button = new Button(buttonSprite);
                {
                    button.OnInteractEvent += ChangeBuildingCategory;
                    category.AddComponent("button", button);
                }
                Text2D text = new Text2D(GameData.BaseFont, name);
                {
                    text.Transform.Position = new Vector2(xPadding, yPadding);
                    category.AddComponent("text", text);
                }

                category.Transform.Position = new Vector2(_categoryBarWidth, 0);
                _categoryBarWidth += vec.X + 2 * xPadding;
                _categoryBarHeight = vec.Y + 2 * yPadding;

                GetChild<GameObject>("categoryBar").AddComponent("category" + name, category);
            }
        }
    }
}
