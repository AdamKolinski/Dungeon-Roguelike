using System;
using System.Collections.Generic;
using System.IO;
using Dungeon_Roguelike.Source.InputSystem;
using Dungeon_Roguelike.Source.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;

namespace Dungeon_Roguelike.Source
{
    public class Game1 : Game
    {
        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        
        public static float ScreenWidth { get; set; }
        public static float ScreenHeight { get; set; }
        public static List<Sprite> CollisionObjects;

        private Sprite _map;
        private Player _player;
        private Scene.Scene _scene;
        
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            IsFixedTimeStep = false;
        }
        
        
        protected override void Initialize()
        {
            ScreenWidth = _graphics.PreferredBackBufferWidth;
            ScreenHeight = _graphics.PreferredBackBufferHeight;

            string tmp = File.ReadAllText("./Content/Test.json");
            _scene = JsonConvert.DeserializeObject<Scene.Scene>(tmp);
            Console.WriteLine(_scene.Tilemap.Tileset[6]);
            
            Input.Initialize();
            base.Initialize();
        }
        
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _map = new Sprite(_spriteBatch, Content.Load<Texture2D>("tileset"), Vector2.Zero, new Vector2(2, 2));
            _player = new Player(_spriteBatch, Content.Load<Texture2D>("characters"), new Vector2(100, 100), new Vector2(2, 2), 9, 8, 0);
        }
        
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            Input.Update();
            _player.Update(gameTime);
            //Console.WriteLine(Input.GetAxis("Horizontal"));
            
            
            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(34, 34, 34));

            //_spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: _camera.Transform);
            
            //_spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise);

            _map.Draw();
            _player.Draw();
            
            _spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}