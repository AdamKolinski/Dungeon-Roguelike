using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Dungeon_Roguelike.Source.Sprites
{
    public class BreakableSprite : TiledSprite, IDestructible
    {
        public void Destroy()
        {
            Console.WriteLine("Destroy handling");
            Game1.CollisionObjects.Remove(this);
        }

        #region Constructors
        public BreakableSprite(Texture2D texture, Point position, int rows, int columns, int tileIndex) : base(texture, position, rows, columns, tileIndex)
        {
            Rows = rows;
            Columns = columns;
            TileIndex = tileIndex;
            _multiTiled = false;
            Initialize();
        }

        public BreakableSprite(Texture2D texture, Point position, Point scale, int columns, int rows, int tileIndex) : base(texture, position, scale, columns, rows, tileIndex)
        {
            Rows = rows;
            Columns = columns;
            TileIndex = tileIndex;
            _multiTiled = false;
            Initialize();
        }
        
        public BreakableSprite(Texture2D texture, Point position, int rows, int columns, int[,] tileIndexes) : base(texture, position, rows, columns, tileIndexes)
        {
            Rows = rows;
            Columns = columns;
            TileIndexes = tileIndexes;
            _multiTiled = true;
            Initialize();
        }

        public BreakableSprite(Texture2D texture, Point position, Point scale, int columns, int rows, int[,] tileIndexes) : base(texture, position, scale, columns, rows, tileIndexes)
        {
            Rows = rows;
            Columns = columns;
            TileIndexes = tileIndexes;
            _multiTiled = true;
            Initialize();
        }

        protected override void Initialize()
        {
            _tileWidth = _texture.Width * Scale.X / Columns;
            _tileHeight = _texture.Height * Scale.Y / Rows;
            Rect = new Rectangle(Position, new Point((int)TileWidth, (int)TileHeight));
        }
        #endregion
    }
}