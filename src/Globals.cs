using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace csproject2024.src
{
    internal class Globals
    {
        public static SpriteBatch SpriteBatch { get; set; }
        public static SpriteBatch UISpriteBatch { get; set; }
        public static AudioManager AudioManager { get; set; }
        public static GraphicsDevice GraphicsDevice { get; set; }
        public static ContentManager Content { get; set; }
        public static TileGenerator levelGenerator { get; set; }
        public static Camera camera { get; set; }   
        public static float ElapsedSeconds { get; set; }

        private static double elapsedTime = 0; // Datalog elapsed time

        public static void Update(GameTime gt)
        {
            ElapsedSeconds = (float)gt.ElapsedGameTime.TotalSeconds;
        }

    }
}
