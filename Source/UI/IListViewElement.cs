using Dungeon_Roguelike.Source.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Dungeon_Roguelike.Source.UI
{
    public interface IListViewElement
    {
        void SetPosition(Point position);
        void SetSize(Vector2 size);
        void Draw(SpriteBatch spriteBatch);
        void LoadContent(ContentManager contentManager);
        void Update(GameTime gameTime);
    }
}