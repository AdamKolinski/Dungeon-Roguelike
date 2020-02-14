using System;
using Dungeon_Roguelike.Source.InputSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

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

        public Point Position { get; protected set; }
        public Point Size { get; protected set; }
        public Rectangle Rect { get; protected set; }

        public virtual void SetPosition(Point position)
        {
            Position = position;
            Rect = new Rectangle(position, Size);
        }
        
        public bool IsMouseOver()
        {
            Point mousePos = Input.MousePosition;
            if (mousePos.X >= Position.X && mousePos.X <= Position.X + Size.X &&
                mousePos.Y >= Position.Y && mousePos.Y <= Position.Y + Size.Y)
                return true;

            return false;
        }

        public bool IsClicked()
        {
            if (IsMouseOver())
            {
                if (Input.IsMouseButtonDown(0, true))
                {
                    Input.UiClicked = true;
                    return true;
                }
            }

            return false;
        }

        public abstract void LoadContent(ContentManager contentManager);
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}