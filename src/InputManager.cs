using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace csproject2024.src
{
    internal class InputManager
    {

        public enum Orientation
        {
            down, right, up, left
        }

        public static Vector2 MoveVector = Vector2.Zero;

        public static Orientation InputOrientation;

        public static Point MousePosition = Point.Zero;

        public static bool LeftMouseDown;
        public static bool LeftMousePressed;
        public static bool LeftMouseUp;

        private static bool wasLeftMouseDown;

        public static void Update(KeyboardState kb, MouseState ms)
        {
            MousePosition = ms.Position;

            if (ms.LeftButton == ButtonState.Pressed)
            {
                if (!wasLeftMouseDown)
                {
                    LeftMousePressed = true;
                }
                else
                {
                    LeftMousePressed = false;
                }

                LeftMouseDown = true;
                LeftMouseUp = false;
            }
            else if (ms.LeftButton == ButtonState.Released)
            {
                if (wasLeftMouseDown)
                {
                    LeftMouseUp = true;
                }
                else
                {
                    LeftMouseUp = false;
                }

                LeftMouseDown = false;
                LeftMousePressed = false;
            }

            wasLeftMouseDown = LeftMouseDown;

            Vector2 output = Vector2.Zero;

            if (kb.IsKeyDown(Keys.W))
            {
                InputOrientation = Orientation.up;
                output += new Vector2(0, -1);
            }
            if (kb.IsKeyDown(Keys.A))
            {
                InputOrientation = Orientation.left;
                output += new Vector2(-1, 0);
            }
            if (kb.IsKeyDown(Keys.S))
            {
                InputOrientation = Orientation.down;
                output += new Vector2(0, 1);
            }
            if (kb.IsKeyDown(Keys.D))
            {
                InputOrientation = Orientation.right;
                output += new Vector2(1, 0);
            }

            if (output.LengthSquared() > 0)
            {
                output = Vector2.Normalize(output);
            }

            MoveVector = output;
        }
    }
}
