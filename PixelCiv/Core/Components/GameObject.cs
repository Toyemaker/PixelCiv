using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PixelCiv.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
        public bool IsEnabled { get; set; }

        private Dictionary<string, IComponent> _componentList;
        private Dictionary<string, IComponent> _referenceList;

        public GameObject()
        {
            Transform = new Transform2D(this);
            IsInteractable = true;
            IsBlocker = true;
            IsVisible = true;
            IsActive = true;
            IsEnabled = true;

            _componentList = new Dictionary<string, IComponent>();
            _referenceList = new Dictionary<string, IComponent>();
        }

        public void AddComponent(string name, IComponent obj)
        {
            if (!_componentList.ContainsKey(name))
            {
                _componentList.Add(name, obj);

                obj.Parent = this;
            }
        }

        public void RemoveComponent(string name)
        {
            _componentList.Remove(name);
        }

        public void AddReference(string name, IComponent obj)
        {
            if (!_referenceList.ContainsKey(name))
            {
                _referenceList.Add(name, obj);

                obj.Parent = this;
            }
        }
        public void RemoveReference(string name)
        {
            _referenceList.Remove(name);
        }

        public virtual bool Interact(Input input, GameTime gameTime)
        {
            bool result = false;

            foreach (IInteractable component in _componentList.Values.Where(a => a.IsEnabled).OfType<IInteractable>().Reverse())
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
            foreach (IUpdatable component in _componentList.Values.Where(a => a.IsEnabled).OfType<IUpdatable>())
            {
                if (component.IsActive)
                {
                    component.Update(gameTime);
                }
            }
        }

        public virtual void FixedUpdate(float tickInterval)
        {
            foreach (IUpdatable component in _componentList.Values.Where(a => a.IsEnabled).OfType<IUpdatable>())
            {
                component.FixedUpdate(tickInterval);
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (IRenderable component in _componentList.Values.Where(a => a.IsEnabled).OfType<IRenderable>())
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
            return _componentList.Values.OfType<T>();
        }

        public virtual IComponent Instantiate(IComponent parent)
        {
            GameObject obj = new GameObject()
            {
                Parent = parent,
                IsActive = IsActive,
                IsBlocker = IsBlocker,
                IsInteractable = IsInteractable,
                IsVisible = IsVisible,
                IsEnabled = IsEnabled,
            };

            obj.Transform.Format(Transform.Position, Transform.Scale, Transform.Rotation);

            foreach (KeyValuePair<string, IComponent> component in _componentList)
            {
                IComponent comp = component.Value.Instantiate(obj);
                obj._componentList.Add(component.Key, comp);
            }

            foreach (KeyValuePair<string, IComponent> component in _referenceList)
            {
                obj._referenceList.Add(component.Key, component.Value);
            }

            return obj;
        }

        public T GetChild<T>(string name) where T : IComponent
        {
            return _componentList.ContainsKey(name) && _componentList[name] is T t ? t : default;
        }

        public bool ContainsChild(string name)
        {
            return _componentList.ContainsKey(name);
        }

        public T GetReference<T>(string name) where T : IComponent
        {
            return _referenceList.ContainsKey(name) && _referenceList[name] is T t ? t : default;
        }
    }
}
