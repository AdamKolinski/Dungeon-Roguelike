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
        public Point ElementSize = new Point(16, 16);
        
        public ListView(Point position, Point size) : base(position, size)
        {
            UIElements = new List<IListViewElement>();
        }

        public override void LoadContent(ContentManager contentManager)
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Point currentPosition = Position;
            int currentElementIndex = 0;
            
            foreach (var element in UIElements)
            {
                element.SetPosition(currentPosition.ToVector2());
                element.SetSize(ElementSize.ToVector2());
                element.Draw(spriteBatch);

                currentElementIndex++;
                Console.WriteLine(
                    Position.X + ElementSize.X * (currentElementIndex % Math.Floor((double)Size.X/ElementSize.X))
                    );
                
                currentPosition = new Point(
                        (int)(Position.X + ElementSize.X * (currentElementIndex % Math.Floor((double)Size.X/ElementSize.X))),
                        (int)(Position.Y + ElementSize.Y * (currentElementIndex % Math.Floor((double)Size.Y/ElementSize.Y)))
                    );
            }
        }
    }
}