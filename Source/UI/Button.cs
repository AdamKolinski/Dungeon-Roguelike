using Dungeon_Roguelike.Source.InputSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Dungeon_Roguelike.Source.UI
{
    public class Button : UIElement
    {
        private Texture2D _backgroundTexture;
        private string _backgroundTextureName;
        public Text Text;
        public Color BackgroundColor;
        
        public Button(Point position, Point size, string backgroundTexture, string text) : base(position, size)
        {
            _backgroundTextureName = backgroundTexture;
            BackgroundColor = Color.White;
            Text = new Text(position, "Arial", text);
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
                Text.text = "Mouse Over!";
                BackgroundColor = Color.CornflowerBlue;
                if(Input.IsMouseButtonDown(0, true)) Input.UiClicked = true;
            }
            else
            {
                Text.text = "Text";
                BackgroundColor = Color.White;
            }

            if (IsClicked())
            {
                Text.text = "Im clicked!";
                BackgroundColor = Color.Black;
                OnMouseClick();
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_backgroundTexture, Rect, BackgroundColor);
            Text.Draw(spriteBatch);
        }
    }
}