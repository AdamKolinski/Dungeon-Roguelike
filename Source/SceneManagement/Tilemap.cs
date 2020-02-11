using Microsoft.Xna.Framework;

namespace Dungeon_Roguelike.Source.Scene
{
    public class Tilemap
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Vector2 Size { get; set; }
        public int[] MapTiles { get; set; }
    }
}