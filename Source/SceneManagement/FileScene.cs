using System;
using System.Collections.Generic;
using Dungeon_Roguelike.Source.Sprites;
using Dungeon_Roguelike.Source.TilesetSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Dungeon_Roguelike.Source.SceneManagement
{
    public class FileScene : Scene
    {
        public int SceneIndex { get; set; }
        public Tile[] Tiles;
        public Map Map;

        protected List<TiledSprite> _tiles;

        public override void GenerateTiles()
        {
            _tiles = new List<TiledSprite>();
            
            foreach (var tile in Tiles)
            {
                if(tile.TextureName == "") continue;
                
                Tileset tileset = TilesetsManager.GetTileset(tile.TextureName);
                _tiles.Add(new TiledSprite(tileset.Texture, Vector2.Zero, new Vector2(2, 2), tileset.Columns, tileset.Rows, tile.TileIndex));                
            }

            Console.WriteLine(_tiles);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < Map.MapTiles.Length; i++)
            {
                if(Tiles[Map.MapTiles[i]].TextureName == "") continue;
                
                TiledSprite tmpSprite = _tiles[Map.MapTiles[i]-1];
                tmpSprite.Position = new Vector2((i%Map.Size.X)*tmpSprite.TileWidth, (float)Math.Floor(i / Map.Size.X)*tmpSprite.TileHeight);
                tmpSprite.Rect = new Rectangle(tmpSprite.Position.ToPoint(), new Point((int)tmpSprite.TileWidth, (int)tmpSprite.TileHeight));
                
                Rectangle sourceRect = new Rectangle((int) (tmpSprite.TileWidth / tmpSprite.Scale.X * (tmpSprite.TileIndex % tmpSprite.Columns)),
                    y: (int) (tmpSprite.TileHeight / tmpSprite.Scale.Y * (tmpSprite.TileIndex / tmpSprite.Columns)), width: (int) (tmpSprite.TileWidth / tmpSprite.Scale.X),
                    height: (int) (tmpSprite.TileHeight / tmpSprite.Scale.Y));

                spriteBatch.Draw(tmpSprite.Texture, tmpSprite.Rect, sourceRect, Color.White);
            }
            //spriteBatch.Draw();
        }
    }
}