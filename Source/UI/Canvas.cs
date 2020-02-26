using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Dungeon_Roguelike.Source.UI
{
    public class Canvas
    {
        public List<UIElement> UIElements;

        public Canvas()
        {
            UIElements = new List<UIElement>();
        }

        public void LoadContent(ContentManager contentManager)
        {
            foreach (var element in UIElements)
            {
                element.LoadContent(contentManager);
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (var element in UIElements)
            {
                element.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch, RenderTarget2D uiRenderTarget2D)
        {
            foreach (var element in UIElements)
            {
                element.Draw(spriteBatch, uiRenderTarget2D);
            }
        }
    }
}