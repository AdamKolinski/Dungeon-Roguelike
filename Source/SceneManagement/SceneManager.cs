using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace Dungeon_Roguelike.Source.SceneManagement
{
    public static class SceneManager
    {
        public static List<Scene> Scenes = new List<Scene>();
        public static Scene CurrentScene { get; set; }

        public static void LoadScene(int sceneIndex)
        {
            CurrentScene = Scenes[sceneIndex];
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            CurrentScene.Draw(spriteBatch);
        }

        public static void GenerateTiles()
        {
            foreach (var scene in Scenes)
            {
                scene.GenerateTiles();
            }
        }
    }
}