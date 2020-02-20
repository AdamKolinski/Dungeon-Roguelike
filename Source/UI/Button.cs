using Dungeon_Roguelike.Source.InputSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Dungeon_Roguelike.Source.UI
{
    public class Button : UIElement, IListViewElement
    {
        private Texture2D _backgroundTexture;
        private string _backgroundTextureName;
        public Text Text;
        public string MouseOverText, NormalText;
        public Color BackgroundColor;
        
        public Button(Point position, Point size, string backgroundTexture, string text) : base(position, size)
        {
            _backgroundTextureName = backgroundTexture;
            BackgroundColor = Color.White;
            Text = new Text(position, "Arial", text);
            NormalText = text;
            MouseOverText = text;
        }

        public override void SetPosition(Point position)
        {
            base.SetPosition(position);
            Text.SetPosition(position);
        }

        public override void LoadContent(ContentManager contentManager)
        {
            _backgroundTexture = contentManager.Load<Texture2D>(_backgroundTextureName);
            Text.LoadContent(contentManager);
        }

        public override void Update(GameTime gameTime)
        {
            if (IsMouseOver())
            {
                Text.text = MouseOverText;
                Text.Color = Color.White;
                BackgroundColor = Color.CornflowerBlue;
                if(Input.IsMouseButtonDown(0, true)) Input.UiClicked = true;
            }
            else
            {
                Text.text = NormalText;
                Text.Color = Color.Black;
                BackgroundColor = Color.White;
            }

            if (IsClicked())
            {
                BackgroundColor = Color.Black;
                OnMouseClick();
            }
        }

        public void SetPosition(Vector2 position)
        {
            Position = position.ToPoint();
            Rect = new Rectangle(Position, Size);
            Text.Position = Position;
        }

        public void SetSize(Vector2 size)
        {
            Size = size.ToPoint();
            Rect = new Rectangle(Position, Size);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_backgroundTexture, Rect, BackgroundColor);
            Text.Draw(spriteBatch);
        }
    }
}