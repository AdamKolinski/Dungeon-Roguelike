using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace Dungeon_Roguelike.Source.SceneManagement
{
    public class TilesetManager
    {
        private static Dictionary<string, TilesetTexture> _tilesets = new Dictionary<string, TilesetTexture>();

        public static void CreateTileset(string name, Texture2D texture, int rows, int columns)
        {
            _tilesets.Add(name, new TilesetTexture(texture, rows, columns));
        }
        
        public static TilesetTexture GetTileset(string name)
        {
            TilesetTexture tilesetTexture;
            if (_tilesets.TryGetValue(name, out tilesetTexture))
            {
                return tilesetTexture;    
            }
            else
            {
                return null;
            }
            
        }
    }
}