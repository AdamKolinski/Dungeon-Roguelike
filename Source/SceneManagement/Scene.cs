using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Dungeon_Roguelike.Source.SceneManagement
{
    public class Scene
    {
        public string Name;
        public Tilemap Tilemap;

        public Scene(string name, Tilemap tilemap = null)
        {
            Name = name;
            Tilemap = tilemap;
        }

        public virtual void LoadContent(ContentManager contentManager)
        {
            
        }

        public virtual void Update(GameTime gameTime)
        {
            
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            
        }
    }
}