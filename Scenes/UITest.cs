using Dungeon_Roguelike.Source;
using Dungeon_Roguelike.Source.SceneManagement;
using Dungeon_Roguelike.Source.Sprites;
using Dungeon_Roguelike.Source.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Dungeon_Roguelike.Scenes
{
    public class UITest : Scene
    {
        private ListView listView;
        
        public UITest(string name, Tilemap tilemap = null) : base(name, tilemap)
        {
            listView = new ListView(new Point(0, 0), new Point(128, 256));
        }

        public override void LoadContent(ContentManager contentManager)
        {
            listView.UIElements.Add(new Button(Point.Zero, Point.Zero, Helpers.pixelSprite, "Test"));
            
            Sprite tmpSprite = new Sprite(contentManager.Load<Texture2D>("pixel"), Point.Zero);
            for (int i = 0; i < 11; i++)
                listView.UIElements.Add(tmpSprite);


            Canvas.UIElements.Add(listView);
            
            base.LoadContent(contentManager);
            

        }
    }
}