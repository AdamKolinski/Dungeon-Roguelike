using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Dungeon_Roguelike.Source.Sprites
{
    public class TiledSprite : Sprite
    {
        protected bool _multiTiled;
        public int[,] TileIndexes { get; set; }

        public int TileIndex;

        public int Rows { get; set; }

        public int Columns { get; set; }

        public float TileWidth { get; protected set; }
        public float TileHeight { get; protected set; }

        public TiledSprite(Texture2D texture, Vector2 position, int rows, int columns, int tileIndex) : base(texture, position)
        {
            Rows = rows;
            Columns = columns;
            TileIndex = tileIndex;
            _multiTiled = false;
            Initialize();
        }

        public TiledSprite(Texture2D texture, Vector2 position, Vector2 scale, int columns, int rows, int tileIndex) : base(texture, position, scale)
        {
            Rows = rows;
            Columns = columns;
            TileIndex = tileIndex;
            _multiTiled = false;
            Initialize();
        }
        
        public TiledSprite(Texture2D texture, Vector2 position, int rows, int columns, int[,] tileIndexes) : base(texture, position)
        {
            Rows = rows;
            Columns = columns;
            TileIndexes = tileIndexes;
            _multiTiled = true;
            Initialize();
        }

        public TiledSprite(Texture2D texture, Vector2 position, Vector2 scale, int columns, int rows, int[,] tileIndexes) : base(texture, position, scale)
        {
            Rows = rows;
            Columns = columns;
            TileIndexes = tileIndexes;
            _multiTiled = true;
            Initialize();
        }

        protected override void Initialize()
        {
            TileWidth = _texture.Width * Scale.X / Columns;
            TileHeight = _texture.Height * Scale.Y / Rows;
            Rect = new Rectangle(Position.ToPoint(), new Point((int)TileWidth, (int)TileHeight));
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if(!_multiTiled)
                spriteBatch.Draw(_texture, new Rectangle((int)Position.X, (int)Position.Y, (int)TileWidth, (int)TileHeight), new Rectangle((int)(TileWidth/Scale.X * (TileIndex % Columns)), y: (int)(TileHeight/Scale.Y *(TileIndex / Columns)), width: (int)(TileWidth/Scale.X), height: (int)(TileHeight/Scale.Y)),  Color.White, 0, Vector2.Zero,  s, 0);

            if (_multiTiled)
            {
                for (int y = 0; y < TileIndexes.GetLength(0); y++)
                {
                    for (int x = 0; x < TileIndexes.GetLength(1); x++)
                    {
                        Rectangle destinationRect = new Rectangle((int) Position.X + x * (int) TileWidth, (int) Position.Y + y * (int) TileHeight, (int)TileWidth, (int) TileHeight);
                        Rectangle sourceRect = new Rectangle((int)(TileWidth/Scale.X * (TileIndexes[y, x] % Columns)), y: (int)(TileHeight/Scale.Y * (TileIndexes[y, x] / Columns)), width: (int)(TileWidth/Scale.X), height: (int)(TileHeight/Scale.Y));
                        
                        spriteBatch.Draw(_texture, destinationRect , sourceRect,  Color.White);
                    }
                }
            }
            //_spriteBatch.Draw(Helpers.pixel, Rect, new Color(0, 0.3f, 0, 0.2f));
        }
    }
}