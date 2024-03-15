using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PixelCiv.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelCiv.Core.Components
{
    public class GameObject : IInteractable, IUpdatable, IRenderable, ITransformable
    {
        public IComponent Parent { get; set; }

        public Transform2D Transform { get; private set; }

        public bool IsInteractable { get; set; }
        public bool IsBlocker { get; set; }
        public bool IsVisible { get; set; }
        public bool IsActive { get; set; }

        private List<IComponent> _componentList;

        public GameObject()
        {
            Transform = new Transform2D(this);
            IsInteractable = true;
            IsBlocker = true;
            IsVisible = true;
            IsActive = true;

            _componentList = new List<IComponent>();
        }

        public void AddComponent(IComponent obj)
        {
            if (!_componentList.Contains(obj))
            {
                _componentList.Add(obj);

                obj.
                Parent = this;
            }
        }

        public void RemoveComponent(IComponent obj)
        {
            _componentList.Add(obj);
        }

        public virtual bool Interact(Input input, GameTime gameTime)
        {
            bool result = false;

            foreach (IInteractable component in _componentList.OfType<IInteractable>().Reverse())
            {
                if (component.IsInteractable && component.Interact(input, gameTime))
                {
                    if (component.IsBlocker)
                    {
                        return true;
                    }

                    result = true;
                }
            }

            return result;
        }

        public virtual void Update(GameTime gameTime)
        {
            foreach (IUpdatable component in _componentList.OfType<IUpdatable>())
            {
                if (component.IsActive)
                {
                    component.Update(gameTime);
                }
            }
        }

        public virtual void FixedUpdate(float tickInterval)
        {
            foreach (IUpdatable component in _componentList.OfType<IUpdatable>())
            {
                component.FixedUpdate(tickInterval);
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (IRenderable component in _componentList.OfType<IRenderable>())
            {
                if (component.IsVisible)
                {
                    component.Draw(spriteBatch, gameTime);
                }
            }
        }

        public IEnumerable<IComponent> GetChildren()
        {
            return GetChildren<IComponent>();
        }

        public IEnumerable<T> GetChildren<T>() where T : IComponent
        {
            return _componentList.OfType<T>();
        }

        public GameObject SetTransform(Vector2 pos, Vector2 scale, float rot)
        {
            Transform.Position = pos;
            Transform.Scale = scale;
            Transform.Rotation = rot;

            return this;
        }
    }
}
