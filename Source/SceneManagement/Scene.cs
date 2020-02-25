using Dungeon_Roguelike.Source.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Dungeon_Roguelike.Source.SceneManagement
{
    public class Scene
    {
        public string Name;
        public Tilemap Tilemap;
        public Canvas Canvas;

        public Scene(string name, Tilemap tilemap = null)
        {
            Name = name;
            Tilemap = tilemap;
            Canvas = new Canvas();
        }

        public virtual void LoadContent(ContentManager contentManager)
        {
            Canvas.LoadContent(contentManager);
        }

        public virtual void Update(GameTime gameTime)
        {
            Canvas.Update(gameTime);
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            Tilemap?.Draw(spriteBatch);
            Canvas.Draw(spriteBatch);
        }
    }
}