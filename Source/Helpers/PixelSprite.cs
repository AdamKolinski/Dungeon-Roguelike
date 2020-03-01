using Dungeon_Roguelike.Source.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Dungeon_Roguelike.Source
{
    public class PixelSprite : Sprite
    {
        public PixelSprite(Texture2D texture, Point position) : base(texture, position)
        {
        }

        public Sprite Clone()
        {
            return new Sprite(_texture, _position);
        }
    }
}