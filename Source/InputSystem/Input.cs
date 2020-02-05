using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace Dungeon_Roguelike.Source.InputSystem
{
    public class Input
    {
        private static readonly List<Axis> Axes = new List<Axis>();
        private static KeyboardState _currentKeyboardState, _previousKeyboardState;
        private static MouseState _currentMouseState, _previousMouseState;

        public static void Initialize()
        {
            Axis horizontal = new Axis("Horizontal", Keys.D, Keys.A);
            Axis vertical = new Axis("Vertical", Keys.W, Keys.S);
            Axes.Add(horizontal);
            Axes.Add(vertical);
        }

        public static float GetAxis(string name)
        {
            foreach (var axis in Axes)
            {
                if (axis.name == name)
                    return axis.value;
            }
            return 0;
        }
        public static bool IsKeyDown(Keys key)
        {
            return _currentKeyboardState.IsKeyDown(key) && !_previousKeyboardState.IsKeyDown(key);
        }

        public static bool IsMouseKeyDown(int button)
        {
            bool toReturn = false;
            switch (button)
            {
                case 0:
                    toReturn = _currentMouseState.LeftButton == ButtonState.Pressed &&
                        _previousMouseState.LeftButton != ButtonState.Pressed;
                    break;
                case 1:
                    toReturn = _currentMouseState.RightButton == ButtonState.Pressed &&
                               _previousMouseState.RightButton != ButtonState.Pressed;
                    break;
            }

            return toReturn;
        }
        public static void Update()
        {
            GetState();
            foreach (var axis in Axes)
            {
                if (Keyboard.GetState().IsKeyDown(axis.positiveKey)) axis.value = 1;
                else if (Keyboard.GetState().IsKeyDown(axis.negativeKey)) axis.value = -1;
                else axis.value = 0;
            }

        }

        private static void GetState()
        {
            _previousKeyboardState = _currentKeyboardState;
            _currentKeyboardState = Keyboard.GetState();

            _previousMouseState = _currentMouseState;
            _currentMouseState = Mouse.GetState();
        }
        
    }
}