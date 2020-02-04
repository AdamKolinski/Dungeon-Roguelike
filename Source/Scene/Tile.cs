namespace Dungeon_Roguelike.Source.Scene
{
    public struct Tile
    {
        public int ID { get; set; }
        public bool HasCollision { get; set; }
        public int TextureId { get; set; }

        public override string ToString()
        {
            return $"ID: {ID} | HasCollision: {HasCollision} | TextureID: {TextureId}";
        }
    }
}