using System;
using System.Collections.Generic;
using System.IO;
using Dungeon_Roguelike.Source.InputSystem;
using Dungeon_Roguelike.Source.SceneManagement;
using Dungeon_Roguelike.Source.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;

namespace Dungeon_Roguelike.Source
{
    public class LevelEditor : Scene
    {
        private MouseState _mouseState, _prevMouseState;
        private Point _mousePosition, _snappedMousePosition;
        
        private TiledSprite _cursorTile, _tilesetTile;
        private int _tilesetIndex;
        private int[,] _tileset;
        private List<Tile> _tilePalette;

        private Point _tilesetSize;
        private readonly string _tilesetName;
        private readonly Vector2 _spriteScale;

        public LevelEditor(string sceneName, string tilesetName, Point tilesetSize, Vector2 spriteScale) : base(sceneName)
        {
            _tilesetName = tilesetName;
            _spriteScale = spriteScale;
            _tilesetIndex = 1;
            _tileset = new int[tilesetSize.X, tilesetSize.Y];
            _tilePalette = new List<Tile>();
            _tilesetSize = tilesetSize;
        }

        public override void LoadContent(ContentManager contentManager)
        {
            _cursorTile = new TiledSprite(TilesetManager.GetTileset(_tilesetName), Vector2.Zero, _spriteScale, _tilesetIndex);
            _tilesetTile = new TiledSprite(TilesetManager.GetTileset(_tilesetName), Vector2.Zero, _spriteScale, _tilesetIndex);
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

            if (Input.IsKeyDown(Keys.P))
            {
                SaveTilemap();
                Console.WriteLine("Tilemap saved!");
            }
            
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
                _tileset[tilesetPosition.X, tilesetPosition.Y] = GetIDFromPalette(_tilesetIndex);
              DebugTileset(_tileset);
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
                //Console.WriteLine("Adding to palette");
                _tilePalette.Add(newTile);
            }
        }

        private int GetTilesetIndexFromPalette(int tileID)
        {
            foreach (var tile in _tilePalette)
                if (tile.ID == tileID)
                    return tile.TilesetIndex;
            
            return -1;
        }

        private int GetIDFromPalette(int tilesetIndex)
        {
            foreach (var tile in _tilePalette)
                if (tile.TilesetIndex == tilesetIndex)
                    return tile.ID;
            
            return -1;
        }

        private void SaveTilemap()
        {
            Tilemap tilemap = new Tilemap
            {
                Name = "Test Tilemap",
                Size = _tilesetSize.ToVector2(),
                TilePalette = _tilePalette.ToArray(),
                Tileset = _tileset
            };
            
            string json = JsonConvert.SerializeObject(tilemap);
            File.WriteAllText("./Content/Tilemap.json", json);
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