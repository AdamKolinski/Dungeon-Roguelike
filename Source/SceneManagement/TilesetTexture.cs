using Microsoft.Xna.Framework.Graphics;

namespace Dungeon_Roguelike.Source.SceneManagement
{
    public class TilesetTexture
    {
        public int Rows, Columns;
        public Texture2D Tex;

        public TilesetTexture(Texture2D tex, int rows, int columns)
        {
            Tex = tex;
            Rows = rows;
            Columns = columns;
        }
    }
}