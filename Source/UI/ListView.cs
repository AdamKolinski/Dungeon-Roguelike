using System;
using System.Collections.Generic;
using Dungeon_Roguelike.Source.InputSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Dungeon_Roguelike.Source.UI
{
    public class ListView : UIElement
    {
        public List<IListViewElement> UIElements;
        public Point ElementSize = new Point(32, 32);
        public Point Spacing = new Point(8, 8);
        
        public ListView(Point position, Point size) : base(position, size)
        {
            UIElements = new List<IListViewElement>();
        }

        public override void LoadContent(ContentManager contentManager)
        {
            foreach (IListViewElement element in UIElements)
            {
                element.LoadContent(contentManager);
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (IsPressed())
            {
                //SetPosition(Position+Input.MouseTranslation);
            }

            foreach (IListViewElement element in UIElements)
            {
                element.Update(gameTime);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Point currentPosition = Position + Spacing;
            int currentElementIndex = 0;
            
            spriteBatch.Draw(Helpers.pixel, Rect, Color.Gray);
            foreach (var element in UIElements)
            {
                element.SetPosition(currentPosition);
                element.SetSize(ElementSize.ToVector2());
                element.Draw(spriteBatch);

                currentElementIndex++;

                double column = (currentElementIndex % Math.Floor((double) Size.X / (ElementSize.X+Spacing.X)));
                double row = (Math.Floor(currentElementIndex / Math.Floor((double) Size.X / (ElementSize.X+Spacing.X))));
                
                currentPosition = new Point(
                        (int)(Position.X + ElementSize.X * column) + Spacing.X + Spacing.X * (int)column,
                        (int)(Position.Y + ElementSize.Y * row) + Spacing.Y + Spacing.Y * (int)row
                    );


            }
        }
    }
}