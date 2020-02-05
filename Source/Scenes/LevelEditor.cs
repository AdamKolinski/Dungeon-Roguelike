using System;
using System.Collections.Generic;
using System.IO;
using Dungeon_Roguelike.Source.InputSystem;
using Dungeon_Roguelike.Source.SceneManagement;
using Dungeon_Roguelike.Source.Sprites;
using Dungeon_Roguelike.Source.TilesetSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;

namespace Dungeon_Roguelike.Source.Scenes
{
    public class LevelEditor : Scene
    {
        private int _tileIndex;
        private Point _mousePosition;
        private Tileset _tileset;
        private MouseState _state, _prevMouseState;
        private List<Tile> _placedTiles;
        private int[] _mapData;
        private Vector2 _mapSize;

        public LevelEditor(Tileset tileset)
        {
            _placedTiles = new List<Tile>();
            _placedTiles.Add(new Tile(0, true, "", 0));
            _mapData = new int[100];
            _tileIndex = 1;
            _tileset = tileset;
            _mapSize = new Vector2(10, 10);
        }
        
        public override void Update(GameTime gameTime)
        {
            if(Input.IsKeyDown(Keys.P)) SaveTilemap();
            _state = Mouse.GetState();
            _mousePosition = _state.Position;
            if (_state.ScrollWheelValue > _prevMouseState.ScrollWheelValue)
            {
                _tileIndex++;
            } else if (_state.ScrollWheelValue < _prevMouseState.ScrollWheelValue)
            {
                _tileIndex--;
            }

            if (Input.IsMouseKeyDown(0))
            {
                int index = 0;
                Tile tmpTile = new Tile(_placedTiles.Count, true, _tileset.TextureName, _tileIndex);

//                if (_placedTiles.Count == 0)
//                {
//                    _placedTiles.Add(tmpTile);
//                }

                bool shouldAdd = true;
                foreach (var tile in _placedTiles)
                {
                    Console.WriteLine($"Checking {tile.ID} | {tile.TileIndex} | {_tileIndex}");
                    if (tile.TileIndex == tmpTile.TileIndex)
                        shouldAdd = false;
                    
                }

                if (shouldAdd)
                {
                    Console.WriteLine("Adding");
                    _placedTiles.Add(tmpTile);
                }
                
                foreach (var tile in _placedTiles)
                {
                    if (tile.TileIndex == _tileIndex)
                        index = tile.ID;

                }
                _mapData[(int)(_mousePosition.X / _tileset.Rows % _mapSize.X + (_mousePosition.Y / _tileset.Columns)%_mapSize.Y*_mapSize.X)] = index;
            }

            //Console.WriteLine(_mousePosition.X / _tileset.Rows % _mapSize.X + (_mousePosition.Y / _tileset.Columns)%_mapSize.Y*_mapSize.X);
            //Console.WriteLine();
            _prevMouseState = _state;
        }

        private void SaveTilemap()
        {
            FileScene tilemapToSave = new FileScene
            {
                SceneIndex = 2, Tiles = _placedTiles.ToArray(), Map = new Map("Dupa", _mapSize, _mapData)
            };
            string jsonMap = JsonConvert.SerializeObject(tilemapToSave);
            File.WriteAllText(@"./Content/mapTest2.json", jsonMap);
            Console.WriteLine("Trying to save tilemap!");
        }
        
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (_mapData.Length == 0) return;
            
            for (int i = 0; i < _mapData.Length; i++)
            {
                if (_mapData[i] != 0)
                {
                    int tmp = 0;
                    
                    foreach (var tile in _placedTiles)
                    {
                        if (tile.ID == _mapData[i])
                            tmp = tile.TileIndex;

                    }
                    
                    Point srcLoc = new Point((tmp % _tileset.Rows) * _tileset.TileWidth, tmp / _tileset.Columns * _tileset.TileHeight);
                    Point srcSize = new Vector2(_tileset.TileWidth, _tileset.TileHeight).ToPoint();
                    spriteBatch.Draw(_tileset.Texture, new Rectangle(new Vector2(i%_mapSize.X, i/_mapSize.X).ToPoint()*new Point(32, 32), new Point(32, 32)), new Rectangle(srcLoc, srcSize) , Color.White);
                }
            }
            
            Point size = new Point(_tileset.TileWidth, _tileset.TileHeight) * new Point(2, 2); //TODO: Scale dependency
            Point location = new Point(_mousePosition.X / size.X * size.X, _mousePosition.Y / size.Y * size.Y);
            
            Point sourceLocation = new Point((_tileIndex % _tileset.Rows) * _tileset.TileWidth, _tileIndex / _tileset.Columns * _tileset.TileHeight);
            Point sourceSize = new Vector2(_tileset.TileWidth, _tileset.TileHeight).ToPoint();
            
            Rectangle source = new Rectangle(sourceLocation, sourceSize);
            
            spriteBatch.Draw(_tileset.Texture, new Rectangle(location, size), source, Color.White);
            
        }

        public override void DrawUI(SpriteBatch canvasBatch)
        {
            canvasBatch.Draw(_tileset.Texture, Vector2.Zero, Color.White);
        }
    }
}