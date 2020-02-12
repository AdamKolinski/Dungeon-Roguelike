using Dungeon_Roguelike.Source.SceneManagement;
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

        public float TileWidth => _tileWidth;

        public TiledSprite(Texture2D texture, Vector2 position, int rows, int columns, int tileIndex) : base(texture, position)
        {
            Rows = rows;
            Columns = columns;
            TileIndex = tileIndex;
            _multiTiled = false;
            Initialize();
        }

        
        public TiledSprite(TilesetTexture tilesetTexture, Vector2 position, Vector2 scale, int tileIndex) : base()
        {
            _texture = tilesetTexture.Tex;
            _scale = scale;
            Rows = tilesetTexture.Rows;
            Columns = tilesetTexture.Columns;
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
            s = SpriteEffects.None;
            _tileWidth = _texture.Width * _scale.X / Columns;
            _tileHeight = _texture.Height * _scale.Y / Rows;
            Rect = new Rectangle(_position.ToPoint(), new Point((int)_tileWidth, (int)_tileHeight));
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if(!_multiTiled)
                spriteBatch.Draw(_texture, new Rectangle((int)_position.X, (int)_position.Y, (int)_tileWidth, (int)_tileHeight), new Rectangle((int)(_tileWidth/_scale.X * (TileIndex % Columns)), y: (int)(_tileHeight/_scale.Y *(TileIndex / Columns)), width: (int)(_tileWidth/_scale.X), height: (int)(_tileHeight/_scale.Y)),  Color.White, 0, Vector2.Zero,  s, 0);

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