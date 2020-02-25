using System;
using System.Collections.Generic;
using Dungeon_Roguelike.Source.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Dungeon_Roguelike.Source.SceneManagement
{
    public class Tilemap
    {
        public string Name { get; set; }
        public Vector2 Size { get; set; }
        public Tile[] TilePalette { get; set; }
        public int[,] Tileset { get; set; }

        private List<TiledSprite> _spritePalette;
        private bool _generatedPalette;

        private void GenerateSpritePalette()
        {
            _spritePalette = new List<TiledSprite>();

            foreach (var tile in TilePalette)
            {
                if (tile.TilesetName == "")
                    continue;
                if (TilesetManager.GetTileset(tile.TilesetName) != null)
                {
                    TiledSprite paletteSprite = new TiledSprite(TilesetManager.GetTileset(tile.TilesetName),
                        new Point(4, 4), tile.TilesetIndex);
                    _spritePalette.Add(paletteSprite);
                }
            }

            _generatedPalette = true;
        }

        public override string ToString()
        {
            return $"Name: {Name} | Size: {Size} | TilePalette: {TilePalette.Length} | Tileset: {Tileset.Length}";
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!_generatedPalette)
            {
                GenerateSpritePalette();
            }

            for (int y = 0; y < Size.Y; y++)
            {
                for (int x = 0; x < Size.X; x++)
                {
                    foreach (Tile tile in TilePalette)
                    {
                        if (tile.ID == Tileset[x, y])
                        {
                            _spritePalette[Tileset[x, y]]
                                .SetPosition((new Vector2(x, y) * _spritePalette[Tileset[x, y]].TileWidth).ToPoint());
                            _spritePalette[Tileset[x, y]].Draw(spriteBatch);
                        }
                    }
                }
            }
        }
    }
}