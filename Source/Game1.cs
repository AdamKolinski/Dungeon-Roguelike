using System;
using System.Collections.Generic;
using System.IO;
using Dungeon_Roguelike.Scenes;
using Dungeon_Roguelike.Source.InputSystem;
using Dungeon_Roguelike.Source.SceneManagement;
using Dungeon_Roguelike.Source.Sprites;
using Dungeon_Roguelike.Source.TilesetSystem;
using Dungeon_Roguelike.Source.UI;
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
        
        private Player _player;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            _graphics.PreparingDeviceSettings += GraphicsDevicePreparingDeviceSettings;
            IsMouseVisible = false;
            IsFixedTimeStep = false;
            _graphics.PreferredBackBufferWidth = 1080;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();
        }
        
        private void GraphicsDevicePreparingDeviceSettings(object sender, PreparingDeviceSettingsEventArgs e) {
            e.GraphicsDeviceInformation.PresentationParameters.RenderTargetUsage = RenderTargetUsage.PreserveContents;
        }
        
        
        protected override void Initialize()
        {
            ScreenWidth = _graphics.PreferredBackBufferWidth;
            ScreenHeight = _graphics.PreferredBackBufferHeight;
            
            Cursor.Size = new Point(24, 24);
            
            string tmp = File.ReadAllText("./Content/Tilemap2.json");
            Tilemap tilemap = JsonConvert.DeserializeObject<Tilemap>(tmp);
            
            
            Text text = new Text(new Point(0,0), "Arial", "Hello World!");

            Scene levelEditor = new LevelEditor("Level Editor", "tileset", new Point(20, 12), new Point(4, 4));
            Scene testScene = new Scene("Level01", tilemap);
            
            Canvas testCanvas = new Canvas();
            testCanvas.UIElements.Add(text);

            SceneManager.ContentManager = Content;
            SceneManager.AddScene(levelEditor);
            SceneManager.AddScene(testScene);
            SceneManager.AddScene(new UITest("Test"));

            testScene.Canvas = testCanvas;
            //levelEditor.Canvas = testCanvas;
            Input.Initialize();
            base.Initialize();
        }
        
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Helpers.pixel = Content.Load<Texture2D>("pixel");
            Helpers.pixelSprite = new TiledSprite(Helpers.pixel, Point.Zero, 1, 1, 0);
            
            Cursor.Tex = Content.Load<Texture2D>("test");

            TilesetManager.CreateTileset("tileset", Content.Load<Texture2D>("jawbreaker"), 5, 8);
            SceneManager.LoadScene("Level Editor");
            //_player = new Player(Content.Load<Texture2D>("characters"), new Vector2(100, 100), new Vector2(2, 2), 9, 8, 0);
        }
        
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            Cursor.Update(gameTime);
            Input.Update();
            //_player.Update(gameTime);
            SceneManager.CurrentScene.Update(gameTime);
            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(34, 34, 34));

            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise);
            
            
            //_player.Draw(_spriteBatch);
            SceneManager.CurrentScene.Draw(_spriteBatch);
            _spriteBatch.End();
            
            
            
            SpriteBatch cursorBatch = new SpriteBatch(GraphicsDevice);
            RasterizerState r = new RasterizerState { MultiSampleAntiAlias = true };
            
            cursorBatch.Begin(samplerState: SamplerState.PointClamp , rasterizerState: r);
            Cursor.Draw(cursorBatch);
            cursorBatch.End();
            
            base.Draw(gameTime);
        }
    }
}