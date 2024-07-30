using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
       
        public void init()
        {         
            Camera = new Camera();
            Level = new Level();
            Player = new Player(new Vector2(32, 32), 1, Level);
            Crosshair = new Crosshair();
            ScreenGUI = new ScreenGUI();
            DataLogger = new DataLogger();

            Globals.camera = Camera;

        }

        public void update()
        {        
            Player.Update();
            Level.Update(Player,Camera);
            Camera.Follow(Player.position, Globals.GraphicsDevice.Viewport);
            DataLogger.Update();
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
