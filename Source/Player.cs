using Dungeon_Roguelike.Source.InputSystem;
using Dungeon_Roguelike.Source.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Dungeon_Roguelike.Source
{
    public class Player : TiledSprite
    {
        private float _movementSpeed = 10; 
        public override void Update(GameTime gameTime)
        {
            _position += (new Vector2(Input.GetAxis("Horizontal"), -Input.GetAxis("Vertical")) * _movementSpeed * gameTime.ElapsedGameTime.Milliseconds/100f).ToPoint();
        }

        #region Constructors
        public Player(Texture2D texture, Point position, int rows, int columns, int tileIndex) : base(texture, position, rows, columns, tileIndex)
        {
        }

        public Player(Texture2D texture, Point position, Point scale, int columns, int rows, int tileIndex) : base(texture, position, scale, columns, rows, tileIndex)
        {
        }

        public Player(Texture2D texture, Point position, int rows, int columns, int[,] tileIndexes) : base(texture, position, rows, columns, tileIndexes)
        {
        }

        public Player(Texture2D texture, Point position, Point scale, int columns, int rows, int[,] tileIndexes) : base(texture, position, scale, columns, rows, tileIndexes)
        {
        }
        #endregion
    }
}