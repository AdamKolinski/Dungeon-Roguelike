using System;
using System.Collections.Generic;
using Dungeon_Roguelike.Source.InputSystem;
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
        private Point _mousePosition, _snappedMousePosition;
        
        private readonly TiledSprite _cursorTile, _tilesetTile;
        private int _tilesetIndex;
        private int[,] _tileset;
        private List<Tile> _tilePalette;

        public LevelEditor(string tilesetName, Point tilesetSize, Vector2 spriteScale)
        {
            _tilesetIndex = 1;
            _cursorTile = new TiledSprite(TilesetManager.GetTileset(tilesetName), Vector2.Zero, spriteScale, _tilesetIndex);
            _tilesetTile = new TiledSprite(TilesetManager.GetTileset(tilesetName), Vector2.Zero, spriteScale, _tilesetIndex);
            _tileset = new int[tilesetSize.X, tilesetSize.Y];
            _tilePalette = new List<Tile>();
        }
        
        public int RoundToMultiplication(int number, int multiplication, bool asCount = false)
        {
            if (multiplication == 0)
                return number;

            int tmp = number % multiplication;
            if (tmp == 0)
                return number;

            return asCount? ((number + multiplication - tmp)/multiplication) - 1 :  number + multiplication - tmp - multiplication;
        }
        
        public override void Update(GameTime gameTime)
        {
            _mouseState = Mouse.GetState();
            _mousePosition = _mouseState.Position;
            _snappedMousePosition = new Point(
                RoundToMultiplication(_mousePosition.X, (int) _cursorTile.TileWidth),
                RoundToMultiplication(_mousePosition.Y, (int) _cursorTile.TileWidth));

            if (_mouseState.ScrollWheelValue > _prevMouseState.ScrollWheelValue)
                _tilesetIndex++;
            if (_mouseState.ScrollWheelValue < _prevMouseState.ScrollWheelValue)
                _tilesetIndex--;
            
            _cursorTile.SetPosition(_snappedMousePosition.ToVector2());
            _cursorTile.TileIndex = _tilesetIndex;
            
            //Console.WriteLine($"TilesetIndex: {_tilesetIndex}");
            //Console.WriteLine($"Snapped mouse position: {_snappedMousePosition}");
            
            if(Input.IsMouseButtonDown(0))
                PlaceTile();

            _prevMouseState = _mouseState;
        }

        private void PlaceTile()
        {
            Point tilesetPosition = new Point(
                RoundToMultiplication(_mousePosition.X, (int) _cursorTile.TileWidth, true),
                RoundToMultiplication(_mousePosition.Y, (int) _cursorTile.TileWidth, true));

            if (tilesetPosition.X < _tileset.GetLength(0) && tilesetPosition.Y < _tileset.GetLength(1))
            {
                AddToPalette();
                _tileset[tilesetPosition.X, tilesetPosition.Y] = _tilesetIndex;
//                DebugTileset(_tileset);
            }
        }

        private void DebugTileset(int[,] tileset)
        {
            Console.Clear();
            for (int y = 0; y < tileset.GetLength(1); y++)
            {
                for (int x = 0; x < tileset.GetLength(0); x++)
                {
                    Console.Write(tileset[x,y] + "  ");
                }

                Console.WriteLine();
            }
        }

        private void AddToPalette()
        {
            bool shouldAddToPalette = true;
            
            foreach (var tile in _tilePalette)
            {
                if (tile.TilesetIndex == _tilesetIndex)
                    shouldAddToPalette = false;
            }

            if (shouldAddToPalette)
            {
                Tile newTile = new Tile
                {
                    ID = _tilePalette.Count,
                    HasCollision = false,
                    TilesetName = "tileset",
                    TilesetIndex = _tilesetIndex
                };
                Console.WriteLine("Adding to palette");
                _tilePalette.Add(newTile);
            }
        }

        private int GetTilesetIndexFromPalette(int tileIndex)
        {
            foreach (var tile in _tilePalette)
            {
                if (tile.TilesetIndex == tileIndex)
                    return tile.TilesetIndex;
            }

            return -1;
        }
        
        public override void Draw(SpriteBatch spriteBatch)
        {
            
            for (int y = 0; y < _tileset.GetLength(1); y++)
                for (int x = 0; x < _tileset.GetLength(0); x++)
                {
                     _tilesetTile.SetPosition(new Vector2(x, y)*_tilesetTile.TileWidth);
                     _tilesetTile.TileIndex = GetTilesetIndexFromPalette(_tileset[x, y]);
                     _tilesetTile.Draw(spriteBatch);
                }
            
            _cursorTile.Draw(spriteBatch);
        }
    }
}