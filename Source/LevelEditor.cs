using System;
using System.Collections.Generic;
using Dungeon_Roguelike.Source.SceneManagement;
using Dungeon_Roguelike.Source.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Dungeon_Roguelike.Source
{
    public class LevelEditor : Scene
    {
        private MouseState _mouseState, _prevMouseState;
        private Point _mousePosition;
        
        private readonly TiledSprite _currentTile;
        private int _tilesetIndex;
        private int[,] _tileset;
        private List<Tile> _tilePalette;

        public LevelEditor(string tilesetName, Point tilesetSize, Vector2 spriteScale)
        {
            _tilesetIndex = 1;
            _currentTile = new TiledSprite(TilesetManager.GetTileset(tilesetName), Vector2.Zero, spriteScale, 1);
            _tileset = new int[tilesetSize.X, tilesetSize.Y];
            _tilePalette = new List<Tile>();
        }
        
        public override void Update(GameTime gameTime)
        {
            _mouseState = Mouse.GetState();
            _mousePosition = _mouseState.Position;

            if (_mouseState.ScrollWheelValue > _prevMouseState.ScrollWheelValue)
                _tilesetIndex++;
            if (_mouseState.ScrollWheelValue < _prevMouseState.ScrollWheelValue)
                _tilesetIndex--;
            
            _currentTile.SetPosition(_mousePosition.ToVector2());
            _currentTile.TileIndex = _tilesetIndex;
            Console.WriteLine($"TilesetIndex: {_tilesetIndex}");
            
            _prevMouseState = _mouseState;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            _currentTile.Draw(spriteBatch);
        }
    }
}