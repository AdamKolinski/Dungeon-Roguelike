using System.Collections.Generic;

namespace Dungeon_Roguelike.Source.TilesetSystem
{
    public static class TilesetsManager
    {
        public static Dictionary<string, Tileset> Tilesets = new Dictionary<string, Tileset>();

        public static Tileset GetTileset(string name)
        {
            return Tilesets.TryGetValue(name, out var toReturn) ? toReturn : null;
        }
    }
}