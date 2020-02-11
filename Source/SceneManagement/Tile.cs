namespace Dungeon_Roguelike.Source.SceneManagement
{
    public class Tile
    {
        public int ID { get; set; }
        public bool HasCollision { get; set; }
        public string TilesetName { get; set; }
        public int TilesetIndex { get; set; }

        public override string ToString()
        {
            return $"ID: {ID} | HasCollision: {HasCollision} | TilesetName: {TilesetName} | TilesetIndex: {TilesetIndex}";
        }
    }
}