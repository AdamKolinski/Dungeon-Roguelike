using Microsoft.Xna.Framework;

namespace Dungeon_Roguelike.Source.SceneManagement
{
    public class Map
    {
        public Map(string name, Vector2 size, int[] mapTiles)
        {
            Name = name;
            Size = size;
            MapTiles = mapTiles;
        }

        public string Name { get; set; }
        public Vector2 Size { get; set; }
        public int[] MapTiles { get; set; }
        
        
    }
}