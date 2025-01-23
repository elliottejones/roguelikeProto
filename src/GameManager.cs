using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Threading;

namespace csproject2024.src
{
    internal class GameManager
    {
        private Player Player;
        private Camera Camera;
        private Level Level;
        private Crosshair Crosshair;
        private ScreenGUI ScreenGUI;
        private DataLogger DataLogger;
        private AudioManager AudioManager;
        private MobManager MobManager;
        private RoundManager RoundManager;

        public void init()
        {
            ScreenGUI = new ScreenGUI();
            Camera = new Camera();
            Level = new Level();
            Player = new Player(new Vector2(32, 32), 1, Level, 100);
            Crosshair = new Crosshair();  
            DataLogger = new DataLogger();
            AudioManager = new AudioManager();
            MobManager = new MobManager();
            RoundManager = new RoundManager(MobManager);

            Globals.ScreenGUI = ScreenGUI;
            Globals.camera = Camera;
            Globals.AudioManager = AudioManager;
            Globals.Level = Level;
            Globals.Player = Player;
            Globals.MobManager = MobManager;
        }

        public void update()
        {      
            Player.Update();
            Level.Update(Player,Camera);
            Camera.Follow(Player.position, Globals.GraphicsDevice.Viewport);
            DataLogger.Update();
            MobManager.Update(Level,Player);
            ScreenGUI.damageBorder.Update(Player.maxHealth, Player.health);    
            ScreenCollider.StaticUpdate();
            RoundManager.Update();

            try
            {
                if (Level.lastSelectedTile != null)
                {
                    ScreenGUI.UpdateAttribute("text", "position", ("X: " + ((int)Level.lastSelectedTile.tilePosition.X).ToString()) + " Y: " + ((int)Level.lastSelectedTile.tilePosition.Y).ToString());
                }
            }
            catch
            {
                Console.WriteLine("No player cursor found");
            }
            
        }

        public void draw()
        {
            Globals.SpriteBatch.Begin(
                SpriteSortMode.BackToFront,
                BlendState.AlphaBlend,
                SamplerState.PointClamp,
                null,
                RasterizerState.CullNone,
                null,
                Camera.Transform
            );

            Level.Draw(Player);
            Player.Draw();
            MobManager.Draw();
            
            Globals.SpriteBatch.End();

            Globals.UISpriteBatch.Begin(
                SpriteSortMode.BackToFront,
                BlendState.AlphaBlend,
                SamplerState.PointClamp,
                null,
                RasterizerState.CullNone,
                null,
                null
            );

            Crosshair.Draw(InputManager.MousePosition.ToVector2());
            ScreenGUI.Draw();

            Globals.UISpriteBatch.End();
        }
    }
}
