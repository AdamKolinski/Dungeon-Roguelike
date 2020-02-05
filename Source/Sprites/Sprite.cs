using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Dungeon_Roguelike.Source.Sprites
{
    public class Sprite
    {
        protected SpriteEffects s = SpriteEffects.FlipHorizontally;
        protected Texture2D _texture;
        public Vector2 Position { get; set; }
        public Vector2 Scale { get; }
        public Rectangle Rect;

        public Texture2D Texture => _texture;

        public void Move(Vector2 translation)
        {
            Position += new Vector2((int)translation.X, (int)translation.Y);
            Rect.Location += translation.ToPoint();
        }

        public Sprite(Texture2D texture, Vector2 position)
        {
            _texture = texture;
            Position = position;
            Scale = Vector2.One;
        }

        public Sprite(Texture2D texture, Vector2 position, Vector2 scale)
        {
            _texture = texture;
            Position = position;
            Scale = scale;
            //Texture2D e = new Texture2D();
        }

        protected virtual void Initialize()
        {
            Rect = new Rectangle(Position.ToPoint(), _texture.Bounds.Size * Scale.ToPoint());
        }

        public virtual void Update(GameTime gameTime)
        {
            
        }
        
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, new Rectangle((int)Position.X, (int)Position.Y, (int)(_texture.Width*Scale.X), (int)(_texture.Height*Scale.Y)), Color.White);
            
        }
    }
}