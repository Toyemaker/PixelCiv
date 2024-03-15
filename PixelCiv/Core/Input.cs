using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelCiv.Core
{
    public class Input
    {
        private KeyboardState _previousKeyboardState;
        private KeyboardState _currentKeyboardState;

        private MouseState _previousMouseState;
        private MouseState _currentMouseState;

        private Vector2 _mousePosition;

        public void SetKeyboardState(KeyboardState state)
        {
            _previousKeyboardState = _currentKeyboardState;
            _currentKeyboardState = state;
        }

        public void SetMouseState(MouseState state)
        {
            _previousMouseState = _currentMouseState;
            _currentMouseState = state;
        }

        public void SetMousePosition(Vector2 position)
        {
            _mousePosition = position;
        }

        public bool IsKeyDown(Keys key)
        {
            return _currentKeyboardState.IsKeyDown(key);
        }

        public bool IsKeyUp(Keys key)
        {
            return _currentKeyboardState.IsKeyUp(key);
        }

        public bool IsKeyPressed(Keys key)
        {
            return IsKeyDown(key) && _previousKeyboardState.IsKeyUp(key);
        }

        public bool IsKeyReleased(Keys key)
        {
            return IsKeyUp(key) && _previousKeyboardState.IsKeyDown(key);
        }

        public bool IsMouseButtonDown(MouseButton button)
        {
            switch (button)
            {
                case MouseButton.Left:
                    return _currentMouseState.LeftButton == ButtonState.Pressed;
                case MouseButton.Right:
                    return _currentMouseState.RightButton == ButtonState.Pressed;
                case MouseButton.Middle:
                    return _currentMouseState.MiddleButton == ButtonState.Pressed;
                default: return false;
            }
        }

        public bool IsMouseButtonUp(MouseButton button)
        {
            return !IsMouseButtonDown(button);
        }

        public bool IsMouseButtonPressed(MouseButton button)
        {
            switch (button)
            {
                case MouseButton.Left:
                    return IsMouseButtonDown(button) && _previousMouseState.LeftButton == ButtonState.Released;
                case MouseButton.Right:
                    return IsMouseButtonDown(button) && _previousMouseState.RightButton == ButtonState.Released;
                case MouseButton.Middle:
                    return IsMouseButtonDown(button) && _previousMouseState.MiddleButton == ButtonState.Released;
                default: return false;
            }
        }

        public bool IsMouseButtonReleased(MouseButton button)
        {
            switch (button)
            {
                case MouseButton.Left:
                    return IsMouseButtonUp(button) && _previousMouseState.LeftButton == ButtonState.Pressed;
                case MouseButton.Right:
                    return IsMouseButtonUp(button) && _previousMouseState.RightButton == ButtonState.Pressed;
                case MouseButton.Middle:
                    return IsMouseButtonUp(button) && _previousMouseState.MiddleButton == ButtonState.Pressed;
                default: return false;
            }
        }

        public Vector2 GetMousePosition()
        {
            return _mousePosition;
        }
    }

    public enum MouseButton
    {
        Left,
        Middle,
        Right,
    }
}
