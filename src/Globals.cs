﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace csproject2024.src
{
    internal class Globals
    {
        static internal class GetItemPreset
        {
            public static Weapon Glock()
            {
                return new Weapon("glock", new(Content.Load<Texture2D>("glock"), Vector2.Zero, "glock"), new(Content.Load<SoundEffect>("glockshot")), new(0.1f, 0, 1, false, new(0.3f,0.3f), new(Content.Load<Texture2D>("muzzleflash"), new(-6,-3), "muzzleflash"), false, Color.White, false, 0.1f), 10, new(Content.Load<Texture2D>("bullet"),Vector2.Zero,"bullet"), 0.15f, 10, 1);
            }

            public static Weapon Lolipop()
            {
                return new Weapon("lolipop", new(Content.Load<Texture2D>("lolipop"), Vector2.Zero, "lolipop"), new(Content.Load<SoundEffect>("pop")), null, 40, new(Content.Load<Texture2D>("throwLolipop"), new(1,1), "lolipop"), 1f, 3, 1);
            }

            public static Consumable Stim()
            {
                return new Consumable("stim", new(Content.Load<Texture2D>("stim"), Vector2.Zero, "stim"), new(Content.Load<SoundEffect>("stab")), null, Consumable.ConsumableType.SpeedBoost);
            }

            public static Consumable Chilli()
            {
                return new Consumable("chilli", new(Content.Load<Texture2D>("chilli"), Vector2.Zero, "chilli"), new(Content.Load<SoundEffect>("nomnomnom")), null, Consumable.ConsumableType.SpeedBoost);
            }

            public static Consumable Cheese()
            {
                return new Consumable("cheese", new(Content.Load<Texture2D>("cheese"), Vector2.Zero, "cheese"), new(Content.Load<SoundEffect>("nomnomnom")), null, Consumable.ConsumableType.HealQuarter);
            }

            public static Consumable Medkit()
            {
                return new Consumable("medkit", new(Content.Load<Texture2D>("medkit"), Vector2.Zero, "medkit"), new(Content.Load<SoundEffect>("heal")), null, Consumable.ConsumableType.Heal);
            }

            public static Consumable Piwo()
            {
                return new Consumable("piwo", new(Content.Load<Texture2D>("piwo"), Vector2.Zero, "piwo"), new(Content.Load<SoundEffect>("drink")), null, Consumable.ConsumableType.DamageBoost);
            }

            public static Consumable Chicken()
            {
                return new Consumable("chicken", new(Content.Load<Texture2D>("chicken"), Vector2.Zero, "chicken"), new(Content.Load<SoundEffect>("nomnomnom")), null, Consumable.ConsumableType.DamageBoost);
            }

        }

        public static GameManager GameManager { get; set; }
        public static int RoundNumber {  get; set; }
        public static GraphicsDeviceManager GraphicsDeviceManager { get; set; }
        public static ScreenGUI ScreenGUI { get; set; }
        public static SpriteBatch SpriteBatch { get; set; }
        public static SpriteBatch UISpriteBatch { get; set; }
        public static AudioManager AudioManager { get; set; }
        public static GraphicsDevice GraphicsDevice { get; set; }
        public static ContentManager Content { get; set; }
        public static TileGenerator levelGenerator { get; set; }
        public static Camera camera { get; set; }   
        public static float ElapsedSeconds { get; set; }
        public static MobManager MobManager { get; set; }
        public static Level Level { get; set; }    

        public static Player Player { get; set; }

        private static double elapsedTime = 0; // Datalog elapsed time

        public static bool damageBoost = false;
        public static bool speedBoost = true;

        public static void Update(GameTime gt)
        {
            ElapsedSeconds = (float)gt.ElapsedGameTime.TotalSeconds;
        }

        public static float Vector2Magnitude(Vector2 vector)
        {
            return (float)Math.Sqrt((double)(vector.X * vector.X + vector.Y * vector.Y));
        }
        public static void OpenHtmlFileInDefaultBrowser(string htmlFilePath)
        {
            try
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    var psi = new ProcessStartInfo
                    {
                        FileName = htmlFilePath,
                        UseShellExecute = true
                    };
                    Process.Start(psi);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", htmlFilePath);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", htmlFilePath);
                }
                else
                {
                    Console.WriteLine("Unsupported operating system.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to open HTML file: {ex.Message}");
            }
        }
    }
}
