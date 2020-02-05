using System;
using Microsoft.Xna.Framework.Graphics;

namespace Dungeon_Roguelike.Source.TilesetSystem
{
    public class Tileset
    {
        public string TextureName;
        public Texture2D Texture;
        public int Rows, Columns, TileWidth, TileHeight;

        public Tileset(string textureName, Texture2D texture, int rows, int columns)
        {
            TextureName = textureName;
            Texture = texture;
            Rows = rows;
            Columns = columns;
            TileWidth = Texture.Width / Columns;
            TileHeight = Texture.Height / Rows;
        }
    }
}