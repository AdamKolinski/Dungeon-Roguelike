using Dungeon_Roguelike.Source.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Dungeon_Roguelike.Source.UI
{
    public interface IListViewElement
    {
        void SetPosition(Vector2 position);
        void SetSize(Vector2 size);
        void Draw(SpriteBatch spriteBatch);
    }
}