using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Dungeon_Roguelike.Source
{
    public class Game2 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private int _circleOffsetLimit = 100;
        private int _circleSpeed = 3;
        private int _circleOffset;
        private int _circleOffsetDir = 1;

        private Texture2D _circle;
        private RenderTarget2D _renderTarget;

        public Game2()
        {
            _graphics = new GraphicsDeviceManager(this);
            IsFixedTimeStep = false;
            Content.RootDirectory = "Content";
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // This is the texture we'll be drawing on our render target.
            _circle = Content.Load<Texture2D>("tileset");
            
            // this is the rendertarget we'll be clipping the texture out of
            _renderTarget = new RenderTarget2D(GraphicsDevice, 
                300,
                200, 
                false, 
                SurfaceFormat.Color, 
                DepthFormat.None,
                GraphicsDevice.PresentationParameters.MultiSampleCount,
                RenderTargetUsage.PreserveContents);
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // just a nice animation so we can see the effect is updated every frame.
            //UpdateCircleOffset();

            base.Update(gameTime);
        }

        private void UpdateCircleOffset()
        {
            _circleOffset += _circleOffsetDir * _circleSpeed;
            if (_circleOffsetDir > 0)
            {
                if (_circleOffsetLimit < _circleOffset)
                    _circleOffsetDir = -1;
            }
            else
            {
                if (_circleOffset < -_circleOffsetLimit)
                    _circleOffsetDir = 1;
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            RenderTarget2D renderTarget2 = new RenderTarget2D(GraphicsDevice, 
                300,
                600, 
                false, 
                SurfaceFormat.Color, 
                DepthFormat.None,
                GraphicsDevice.PresentationParameters.MultiSampleCount,
                RenderTargetUsage.PreserveContents);
            
            GraphicsDevice.SetRenderTarget(renderTarget2);
            GraphicsDevice.Clear(ClearOptions.Target, Color.Transparent, 1, 0);
            
            _spriteBatch.Begin();
            _spriteBatch.Draw(_circle, new Rectangle(Point.Zero, new Point(600, 600)), Color.White);
            _spriteBatch.End();
            
            // first make the render target all black
            GraphicsDevice.SetRenderTarget(_renderTarget);
            GraphicsDevice.Clear(ClearOptions.Target, Color.Transparent, 1, 0);
            // render the circle to the render target

            _spriteBatch.Begin();
            _spriteBatch.Draw(_circle, new Vector2(50f + _circleOffset, 50f), Color.White);
            _spriteBatch.End();
            
            
            GraphicsDevice.SetRenderTarget(null);

            _spriteBatch.Begin();
            _spriteBatch.Draw(_renderTarget, new Vector2(50f), Color.White);
            _spriteBatch.Draw(renderTarget2, Vector2.Zero, Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}