using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;

namespace Dungeon_Roguelike.Source.SceneManagement
{
    public class SceneManager
    {
        public static ContentManager ContentManager;
        private static Dictionary<string, Scene> _scenes = new Dictionary<string, Scene>();
        private static Scene _currentScene;
        public static Scene CurrentScene => _currentScene;

        public static void AddScene(Scene scene)
        {
            _scenes.Add(scene.Name, scene);
        }
        public static void LoadScene(string name)
        {
            if (_scenes.TryGetValue(name, out _currentScene))
            {
                Console.WriteLine("Scene loaded!");
                CurrentScene.LoadContent(ContentManager);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Problem occured during loading scene!");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}