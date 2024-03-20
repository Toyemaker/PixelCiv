using Microsoft.Xna.Framework;
using PixelCiv.Core;
using PixelCiv.Core.Components;
using PixelCiv.Core.Data;
using PixelCiv.Core.Graphics;
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
            GameObject categoryBar = new GameObject();
            {
                GameObject category = new GameObject();
                {
                    Vector2 vec = GameData.BaseFont.MeasureString("All");

                    int xPadding = 16;
                    int yPadding = 4;

                    Sprite2D buttonSprite = new Sprite2D(GameData.Pixel);
                    buttonSprite.Transform.Scale = new Vector2(vec.X + 2 * xPadding, vec.Y + 2 * yPadding);
                    buttonSprite.Color = Color.Gray;
                    Button button = new Button(buttonSprite);
                    {
                        button.OnInteractEvent += ChangeBuildingCategory;
                        button.Transform.Position = new Vector2(0, 0);
                        category.AddComponent("allButton", button);
                    }
                    Text2D text = new Text2D(GameData.BaseFont, "All");
                    {
                        text.Transform.Position = new Vector2(xPadding, yPadding);
                        category.AddComponent("text", text);
                    }

                    categoryBar.AddComponent("categoryAll", category);
                }
                category = new GameObject();
                {
                    category.Transform.Position = new Vector2(100, 0);
                    Button button = new Button(new Sprite2D(GameData.BaseButtonTexture));
                    {
                        button.OnInteractEvent += ChangeBuildingCategory;
                        category.AddComponent("oreButton", button);
                    }
                    Text2D text = new Text2D(GameData.BaseFont, "Ore");
                    {
                        category.AddComponent("text", text);
                    }

                    categoryBar.AddComponent("categoryOre", category);
                }

                AddComponent("categoryBar", categoryBar);
            }

            GameObject structureBar = new GameObject();
            {
                structureBar.Transform.Position = new Vector2(0, 100);
                GameObject structure = new GameObject();
                {
                    structure.AddAttribute("categoryAll");
                    Button button = new Button(new Sprite2D(GameData.BaseButtonTexture));
                    {
                        structure.AddComponent("houseButton", button);
                    }
                    Sprite2D sprite = new Sprite2D(GameData.BaseHouseTexture);
                    {
                        sprite.Color = Color.Red;
                        structure.AddComponent("houseSprite", sprite);
                    }
                    structureBar.AddComponent("categoryAll", structure);
                }
                structure = new GameObject();
                {
                    structure.AddAttribute("categoryAll");
                    structure.AddAttribute("categoryOre");
                    structure.Transform.Position = new Vector2(100, 0);
                    Button button = new Button(new Sprite2D(GameData.BaseButtonTexture));
                    {
                        structure.AddComponent("oreButton", button);
                    }
                    Sprite2D sprite = new Sprite2D(GameData.BaseHouseTexture);
                    {
                        sprite.Color = Color.Green;
                        structure.AddComponent("oreSprite", sprite);
                    }
                    structureBar.AddComponent("categoryOre", structure);
                }

                AddComponent("structureBar", structureBar);
            }
        }

        public void ChangeBuildingCategory(Input input, GameTime gameTime, Button button)
        {
            int x = 0;

            foreach (GameObject obj in GetChild<GameObject>("structureBar").GetChildren<GameObject>())
            {
                if (obj.HasAttribute(button.Parent.Name))
                {
                    obj.Transform.Position = new Vector2(x++ * 100, 0);
                    obj.IsEnabled = true;
                }
                else
                {
                    obj.IsEnabled = false;
                }
            }
        }
    }
}
