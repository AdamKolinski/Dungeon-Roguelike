using System;
using System.Collections.Generic;
using System.IO;
using Dungeon_Roguelike.Source.InputSystem;
using Dungeon_Roguelike.Source.SceneManagement;
using Dungeon_Roguelike.Source.Sprites;
using Dungeon_Roguelike.Source.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Dungeon_Roguelike.Source
{
    public class UITest : Scene
    {

        public UITest(string sceneName, Tilemap tilemap) : base(sceneName, tilemap)
        {
        }

        public override void LoadContent(ContentManager contentManager)
        {

            Console.WriteLine(Helpers.pixelSprite.Clone());
            Sprite tmp = Helpers.pixelSprite.Clone();
            Button button = new Button(new Point(792, 670), new Point(288, 50), tmp, "Save")
            {
                Text = {Color = Color.White}
            };
 
            Canvas.UIElements.Add(button);
            Button button2 = new Button(new Point(792, 620), new Point(288, 50), Helpers.pixelSprite.Clone(), "test")
            {
                Text = {Color = Color.White}
            };
            Canvas.UIElements.Add(button2);
            
            base.LoadContent(contentManager);
        }
    }
}