using Dungeon_Roguelike.Source.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Dungeon_Roguelike.Source.Sprites
{
    public class Sprite : IListViewElement
    {
        protected SpriteEffects s = SpriteEffects.FlipHorizontally;
        protected Texture2D _texture;
        protected Vector2 _position, _scale;
        public Rectangle Rect;

        public Texture2D Texture => _texture;
        public Vector2 Scale => _scale;

        public Vector2 Position => _position;

        public void Move(Vector2 translation)
        {
            _position += new Vector2((int)translation.X, (int)translation.Y);
            Rect.Location += translation.ToPoint();
        }

        public void SetPosition(Vector2 position)
        {
            _position = position;
            Rect.Location = position.ToPoint();
        }

        public void SetSize(Vector2 size)
        {
            Rect = new Rectangle(Position.ToPoint(), size.ToPoint());
        }
        

        public Sprite(Texture2D texture, Vector2 position)
        {

            _texture = texture;
            _position = position;
            _scale = Vector2.One;
        }

        public Sprite(Texture2D texture, Vector2 position, Vector2 scale)
        {

            _texture = texture;
            _position = position;
            _scale = scale;
            //Texture2D e = new Texture2D();
        }

        protected Sprite()
        { 
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
            spriteBatch.Draw(_texture, Rect, Color.White);
        }

        public void LoadContent(ContentManager contentManager)
        {
        }
    }
}