using Dungeon_Roguelike.Source.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Dungeon_Roguelike.Source.SceneManagement
{
    public class Scene
    {
        public string Name;
        public Tilemap Tilemap;
        public Canvas Canvas;
        protected RenderTarget2D uiRenderTarget2D;

        public Scene(string name, Tilemap tilemap = null)
        {
            Name = name;
            Tilemap = tilemap;
            Canvas = new Canvas();
        }

        public virtual void LoadContent(ContentManager contentManager)
        {
            Canvas.LoadContent(contentManager);
        }

        public virtual void Update(GameTime gameTime)
        {
            Canvas.Update(gameTime);
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            RenderTarget2D renderTarget = new RenderTarget2D(spriteBatch.GraphicsDevice, 
                (int)Game1.ScreenWidth,
                (int)Game1.ScreenHeight, 
                false, 
                SurfaceFormat.Color, 
                DepthFormat.None,
                spriteBatch.GraphicsDevice.PresentationParameters.MultiSampleCount,
                RenderTargetUsage.PreserveContents);
            
            spriteBatch.GraphicsDevice.SetRenderTarget(renderTarget);
            spriteBatch.GraphicsDevice.Clear(ClearOptions.Target, Color.Transparent, 1f, 0);
            
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            Tilemap?.Draw(spriteBatch);
            spriteBatch.End();
            
            spriteBatch.GraphicsDevice.SetRenderTarget(null);

            uiRenderTarget2D = new RenderTarget2D(spriteBatch.GraphicsDevice, 
                (int)Game1.ScreenWidth,
                (int)Game1.ScreenHeight, 
                false, 
                SurfaceFormat.Color, 
                DepthFormat.None,
                spriteBatch.GraphicsDevice.PresentationParameters.MultiSampleCount,
                RenderTargetUsage.PreserveContents);
            
            spriteBatch.GraphicsDevice.SetRenderTarget(uiRenderTarget2D);
            spriteBatch.GraphicsDevice.Clear(ClearOptions.Target, Color.Transparent, 1f, 0);
            
            spriteBatch.Begin();
            Canvas.Draw(spriteBatch, uiRenderTarget2D);
            spriteBatch.End();
            
            spriteBatch.GraphicsDevice.SetRenderTarget(null);
            
            spriteBatch.Begin();
            spriteBatch.Draw(renderTarget, Vector2.Zero, Color.White);
            spriteBatch.Draw(uiRenderTarget2D, Vector2.Zero, Color.White);
            spriteBatch.End();
            renderTarget.Dispose();
            uiRenderTarget2D.Dispose();
        }
    }
}