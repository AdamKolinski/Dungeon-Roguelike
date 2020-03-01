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
        public Point Spacing = new Point(22, 22);
        public float currentScrollY = 0, destinationScrollY, minScrollY = 0, maxScrollY = 800;
        
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
                SetPosition(Position+Input.MouseTranslation);
            }

            if (IsMouseOver())
            {
                currentScrollY = Mathf.Lerp(currentScrollY, destinationScrollY, (float)gameTime.ElapsedGameTime.Milliseconds/100);
                destinationScrollY += Input.MouseScrollTranslation();
                if (destinationScrollY > minScrollY) destinationScrollY = Mathf.Lerp(destinationScrollY, minScrollY, (float)gameTime.ElapsedGameTime.Milliseconds/100);
                if (destinationScrollY < -maxScrollY) destinationScrollY = Mathf.Lerp(destinationScrollY, -maxScrollY, (float)gameTime.ElapsedGameTime.Milliseconds/100);
            }
            
            Point currentPosition = Spacing;
            int currentElementIndex = 0;

            foreach (IListViewElement element in UIElements)
            {

                double column = (currentElementIndex % Math.Floor((double) Size.X / (ElementSize.X+Spacing.X)));
                double row = (Math.Floor(currentElementIndex / Math.Floor((double) Size.X / (ElementSize.X+Spacing.X))));
                
                currentPosition = new Point(
                    (int)(ElementSize.X * column) + Spacing.X + Spacing.X * (int)column,
                    (int)(currentScrollY + ElementSize.Y * row) + Spacing.Y + Spacing.Y * (int)row
                );
                
                element.SetPosition(currentPosition);
                element.SetSize(ElementSize.ToVector2());
                
                currentElementIndex++;
                
                element.Update(gameTime);
            }
        }

        public override void Draw(SpriteBatch spriteBatch, RenderTarget2D uiRenderTarget2D)
        {

            spriteBatch.End();
            
            RenderTarget2D renderTarget = new RenderTarget2D(spriteBatch.GraphicsDevice, 
                Size.X,
                Size.Y);
            
           spriteBatch.GraphicsDevice.SetRenderTarget(renderTarget);
            spriteBatch.GraphicsDevice.Clear(ClearOptions.Target, Color.Gray, 1f, 0);
            
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            //spriteBatch.Draw(Helpers.pixel, Rect, Color.Gray);

            foreach (var element in UIElements)
            {
                element.Draw(spriteBatch, uiRenderTarget2D);
            }
            spriteBatch.End();
            spriteBatch.GraphicsDevice.SetRenderTarget(uiRenderTarget2D);
            
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            spriteBatch.Draw(renderTarget, Rect, Color.White);
            spriteBatch.End();
            renderTarget.Dispose();
                
            spriteBatch.Begin();
        }
    }
}