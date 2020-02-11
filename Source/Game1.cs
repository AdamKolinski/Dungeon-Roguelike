using System;
using System.Collections.Generic;
using System.IO;
using Dungeon_Roguelike.Source.InputSystem;
using Dungeon_Roguelike.Source.SceneManagement;
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
        private Tilemap _tilemap;
        private LevelEditor _levelEditor;

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
            _tilemap = JsonConvert.DeserializeObject<Tilemap>(tmp);
            Console.WriteLine(_tilemap.TilePalette[1]);
            
            Input.Initialize();
            base.Initialize();
        }
        
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            TilesetManager.CreateTileset("tileset", Content.Load<Texture2D>("tileset"), 32, 32);
            _player = new Player(Content.Load<Texture2D>("characters"), new Vector2(100, 100), new Vector2(2, 2), 9, 8, 0);
            
            _levelEditor = new LevelEditor("tileset", new Point(10, 10), new Vector2(2, 2));
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
            _levelEditor.Update(gameTime);
            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(34, 34, 34));
            
            
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise);

            //_tilemap.Draw(_spriteBatch);
            //_player.Draw(_spriteBatch);
            _levelEditor.Draw(_spriteBatch);
            
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}