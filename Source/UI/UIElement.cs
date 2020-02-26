using System;
using Dungeon_Roguelike.Source.InputSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Dungeon_Roguelike.Source.UI
{
    public abstract class UIElement
    {
        public Action OnMouseClick = delegate { };
        public UIElement(Point position, Point size)
        {
            Position = position;
            Size = size;
            Rect = new Rectangle(position, size);
        }

        protected bool _isPressed;
        public Point Position { get; protected set; }
        public Point Size { get; protected set; }
        public Rectangle Rect { get; protected set; }
        public UIElement Parent;

        public virtual void SetPosition(Point position)
        {
            Position = position;
            Rect = new Rectangle(position, Size);
        }
        
        public bool IsMouseOver()
        {
            if (Parent != null)
                return IsMouseOver(Parent);
            Point mousePos = Input.MousePosition;
            if (mousePos.X >= Rect.Location.X && mousePos.X <= Rect.Location.X + Rect.Size.X &&
                mousePos.Y >= Rect.Location.Y && mousePos.Y <= Rect.Location.Y + Rect.Size.Y)
                return true;

            return false;
        }

        public bool IsMouseOver(UIElement relativeTo)
        {
            Point mousePos = Input.MousePosition;
            if (mousePos.X >= relativeTo.Position.X + Rect.Location.X && mousePos.X <= relativeTo.Position.X + Rect.Location.X + Rect.Size.X &&
                mousePos.Y >= relativeTo.Position.Y + Rect.Location.Y && mousePos.Y <= relativeTo.Position.Y + Rect.Location.Y + Rect.Size.Y)
                return true;

            return false;
        }

        public bool IsClicked()
        {
            if (IsMouseOver())
            {
                if (Input.IsMouseButtonUp(0, true))
                {
                    return true;
                }
            }
            return false;
        }
        
        protected bool IsPressed()
        {
            if (IsMouseOver() || _isPressed)
            {
                if (Input.IsMouseButtonPressed(0, true))
                {
                    _isPressed = true;
                    Input.UIClicked = true;
                    return true;
                }

                _isPressed = false;
                return false;
            }

            return false;
        }

        public abstract void LoadContent(ContentManager contentManager);
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch, RenderTarget2D uiRenderTarget2D);
    }
}