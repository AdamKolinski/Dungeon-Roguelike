using System;
using System.Collections.Generic;
using System.IO;
using Dungeon_Roguelike.Source.InputSystem;
using Dungeon_Roguelike.Source.SceneManagement;
using Dungeon_Roguelike.Source.Scenes;
using Dungeon_Roguelike.Source.Sprites;
using Dungeon_Roguelike.Source.TilesetSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;

namespace Dungeon_Roguelike.Source
{
    public class Game1 : Game
    {
        public static GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch, _canvasBatch;
        
        public static float ScreenWidth { get; set; }
        public static float ScreenHeight { get; set; }
        public static List<Sprite> CollisionObjects;

        private Sprite _map;
        private Player _player;
        private FileScene _fileScene;
        
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
            
            
            string tmp = File.ReadAllText("./Content/mapTest2.json");
            _fileScene = JsonConvert.DeserializeObject<FileScene>(tmp);

            Input.Initialize();
            base.Initialize();
        }
        
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _canvasBatch = new SpriteBatch(GraphicsDevice);
            
            Tileset tmpTileset = new Tileset("tileset", Content.Load<Texture2D>("tileset"), 32, 32);
            TilesetsManager.Tilesets.Add("tileset", tmpTileset);
            tmpTileset = new Tileset("characters", Content.Load<Texture2D>("characters"), 9, 8);
            TilesetsManager.Tilesets.Add("characters", tmpTileset);
            
            LevelEditor levelEditor = new LevelEditor(TilesetsManager.GetTileset("tileset"));
            SceneManager.Scenes.Add(_fileScene);
            SceneManager.Scenes.Add(levelEditor);
            SceneManager.GenerateTiles();
            SceneManager.LoadScene(0);
            _player = new Player(Content.Load<Texture2D>("characters"), new Vector2(100, 100), new Vector2(2, 2), 9, 8, 0);
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
            SceneManager.CurrentScene.Update(gameTime);
            //Console.WriteLine(Input.GetAxis("Horizontal"));
            
            
            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(34, 34, 34));

            //_spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: _camera.Transform);
            
            //_spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise);
            SceneManager.Draw(_spriteBatch);
            //_map.Draw(_spriteBatch);
            _player.Draw(_spriteBatch);
            _spriteBatch.End();
            
            _canvasBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise);
            //SceneManager.CurrentScene.DrawUI(_canvasBatch);
            _canvasBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}