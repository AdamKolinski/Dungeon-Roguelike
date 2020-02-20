using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

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
            
        }

        public override void Update(GameTime gameTime)
        {
            if (IsPressed())
            {
                Console.WriteLine("List View is pressed");
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Point currentPosition = Position + Spacing;
            int currentElementIndex = 0;
            
            spriteBatch.Draw(Helpers.pixel, new Rectangle(Position, Size), Color.Gray);
            
            foreach (var element in UIElements)
            {
                element.SetPosition(currentPosition.ToVector2());
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