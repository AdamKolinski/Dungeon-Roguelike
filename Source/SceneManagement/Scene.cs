using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Dungeon_Roguelike.Source.SceneManagement
{
    public class Scene
    {
        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void GenerateTiles()
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
        }

        public virtual void DrawUI(SpriteBatch canvasBatch)
        {
            
        }
    }
}