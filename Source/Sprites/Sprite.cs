using System;
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
        protected Point _position, _scale;
        public Rectangle Rect;

        public Texture2D Texture
        {
            get => _texture;
            set => _texture = value;
        }
        public Point Scale => _scale;

        public Point Position => _position;

        public void Move(Vector2 translation)
        {
            _position += new Point((int)translation.X, (int)translation.Y);
            Rect.Location += translation.ToPoint();
        }

        public void SetPosition(Point position)
        {
            _position = position;
            Rect.Location = position;
        }

        public void SetRelativePosition(Point position, UIElement relativeTo)
        {
            
        }

        public void SetSize(Vector2 size)
        {
            Rect = new Rectangle(Position, size.ToPoint());
        }
        

        public Sprite(Texture2D texture, Point position)
        {

            _texture = texture;
            _position = position;
            _scale = new Point(1, 1);
        }

        public Sprite(Texture2D texture, Point position, Point scale)
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
            Rect = new Rectangle(Position, _texture.Bounds.Size * Scale);
        }

        public virtual void Update(GameTime gameTime)
        {
            
        }

        public Point GetPosition()
        {
            return Position;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Rect, Color.White);
        }

        public void Draw(SpriteBatch spriteBatch, RenderTarget2D uiRenderTarget2D)
        {
            Console.WriteLine("Sprite nie wiem");
        }

        public void LoadContent(ContentManager contentManager)
        {
        }
    }
}