﻿using Dungeon_Roguelike.Source.InputSystem;
using Dungeon_Roguelike.Source.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Dungeon_Roguelike.Source
{
    public static class Cursor
    {
        public static Point Position, Size;
        public static Texture2D Tex;
        public static MouseState MouseState;
        
        public static void Update(GameTime gameTime)
        {
            MouseState = Mouse.GetState();
            Position = MouseState.Position;
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Tex, new Rectangle(Position, Size), Color.White);
        }
    }
}