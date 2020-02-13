using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Dungeon_Roguelike.Source.UI
{
    public class Text : UIElement
    {
        private SpriteFont _font;
        private readonly string _fontName;
        public string text { get; set; }
        public Color Color { get; set; }

        public Text(Point position, string font, string text, Point size = default(Point)) : base(position, size)
        {
            _fontName = font;
            this.text = text;
            Color = Color.White;
        }

        public override void LoadContent(ContentManager contentManager)
        {
            _font = contentManager.Load<SpriteFont>(_fontName);
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(_font, text, Position.ToVector2(), Color);
        }
    }
}