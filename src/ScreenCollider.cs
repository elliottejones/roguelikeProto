using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 
   - all values in this class are with repsect to the absolute screen position in the viewport, unless otherwise stated.
   - no tile positions are used in this class 
   - this class is mainly for mouse interactions but could be used for anything
*/

namespace csproject2024.src
{
    internal class ScreenCollider
    {
        public static Vector2 mousePosition;
        public static Vector2 lastClickLocation;
        public static Vector2 mouseWorldPosition;

        public bool mouseIsTouching;
        public float timeSinceLastHit;

        public bool removed;

        private Rectangle colliderBounds;
        private Point trackerPosition;
        private int sizeX;
        private int sizeY;

        public ScreenCollider(int sizeX, int sizeY)
        {
            this.sizeX = sizeX;
            this.sizeY = sizeY;
        }

        public static void StaticUpdate()
        {
            mouseWorldPosition = Vector2.Transform(mousePosition, Matrix.Invert(Globals.camera.Transform));
        }

        public void Remove()
        {
            this.removed = true;
        }

        public void Update(Point trackerWorldPosition)
        {

            if (!Globals.ScreenGUI.screenColliders.Contains(this))
            {
                Globals.ScreenGUI.screenColliders.Add(this);
            }
            
            Vector2 trackerPositionVector2 = Vector2.Transform(new Vector2(trackerWorldPosition.X, trackerWorldPosition.Y), Globals.camera.Transform);
            trackerPosition = new Point((int)trackerPositionVector2.X,(int)trackerPositionVector2.Y);
            colliderBounds = new Rectangle(trackerPosition - new Point(sizeX / 2, sizeY / 2), new Point(sizeX, sizeY));

            timeSinceLastHit += Globals.ElapsedSeconds;

            if (colliderBounds.Contains(mousePosition))
            {
                if (InputManager.LeftMousePressed)
                {
                    timeSinceLastHit = 0;
                }
                mouseIsTouching = true;
            }
            else
            {
                mouseIsTouching = false;
            }
        }

        public void DebugDraw()
        {
            Globals.UISpriteBatch.Draw(UIElement.uibit.texture, new Vector2(trackerPosition.X,trackerPosition.Y) - new Vector2(sizeX/2,sizeY/2), null, Color.Red, 0f, Vector2.Zero, new Vector2(sizeX, 3), SpriteEffects.None, 0.1f);
            Globals.UISpriteBatch.Draw(UIElement.uibit.texture, new Vector2(trackerPosition.X + sizeX, trackerPosition.Y) - new Vector2(sizeX / 2, sizeY / 2), null, Color.Red, 0f, Vector2.Zero, new Vector2(3, sizeY), SpriteEffects.None, 0.1f);
            Globals.UISpriteBatch.Draw(UIElement.uibit.texture, new Vector2(trackerPosition.X, trackerPosition.Y) - new Vector2(sizeX / 2, sizeY / 2), null, Color.Red, 0f, Vector2.Zero, new Vector2(3, sizeY), SpriteEffects.None, 0.1f);
            Globals.UISpriteBatch.Draw(UIElement.uibit.texture, new Vector2(trackerPosition.X, trackerPosition.Y + sizeY) - new Vector2(sizeX / 2, sizeY / 2), null, Color.Red, 0f, Vector2.Zero, new Vector2(sizeX, 3), SpriteEffects.None, 0.1f);
        }

        public static void MouseClicked()
        {
            lastClickLocation = mousePosition;
        }
    }
}
