namespace Dungeon_Roguelike.Source.TilesetSystem
{
    public struct Tile
    {
        public int ID { get; set; }
        public bool HasCollision { get; set; }
        public string TextureName { get; set; }
        public int TileIndex { get; set; }

        public Tile(int id, bool hasCollision, string textureName, int tileIndex)
        {
            ID = id;
            HasCollision = hasCollision;
            TextureName = textureName;
            TileIndex = tileIndex;
        }
        
        public override string ToString()
        {
            return $"ID: {ID} | HasCollision: {HasCollision} | TextureName: {TextureName} | TileIndex: {TileIndex}";
        }
    }
}