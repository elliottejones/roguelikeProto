﻿using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using System.Net;
using System.IO;
using System.Reflection.Metadata;

namespace csproject2024.src
{
    internal class InputManager
    {

        public enum Orientation
        {
            down, right, up, left
        }

        public static bool mainMenu = true;

        public static Vector2 MoveVector = Vector2.Zero;

        public static Orientation InputOrientation;

        public static Point MousePosition = Point.Zero;

        public static bool LeftMouseDown;
        public static bool LeftMousePressed;
        public static bool LeftMouseUp;

        private static bool wasLeftMouseDown;

        private static KeyboardState previousKeyboardState;

        private static bool fullscreen;

        private static int lastScrollWheelPosition;
        private static int changeInScrollWheelPosition;

        public static void Update(KeyboardState kb, MouseState ms)
        {
            MousePosition = ms.Position;

            changeInScrollWheelPosition = lastScrollWheelPosition - ms.ScrollWheelValue;
            lastScrollWheelPosition = ms.ScrollWheelValue;
            //Console.WriteLine(changeInScrollWheelPosition);
            Globals.camera.cameraZoom -= changeInScrollWheelPosition*0.01f;

            bool isVJustPressed =
                kb.IsKeyDown(Keys.V) &&
                !(previousKeyboardState.IsKeyDown(Keys.V));

            if (isVJustPressed)
            {
                Globals.Player.DropCurrentItem();
            }

            bool isEnterJustPressed =
                kb.IsKeyDown(Keys.Enter) &&
                !(previousKeyboardState.IsKeyDown(Keys.Enter));

            if (isEnterJustPressed)
            {
                if (mainMenu)
                {
                    Globals.GameManager.Init();
                    mainMenu = false;
                    RoundManager.doSpawn = true;
                }
            }

            bool isAltCJustPressed =
                kb.IsKeyDown(Keys.LeftAlt) &&
                kb.IsKeyDown(Keys.C) &&
                !(previousKeyboardState.IsKeyDown(Keys.LeftAlt) && previousKeyboardState.IsKeyDown(Keys.C));

            if (isAltCJustPressed)
            {
                Globals.ScreenGUI.debugColliders = !Globals.ScreenGUI.debugColliders;
            }

            bool isAltKJustPressed =
                kb.IsKeyDown(Keys.LeftAlt) &&
                kb.IsKeyDown(Keys.K) &&
                !(previousKeyboardState.IsKeyDown(Keys.LeftAlt) && previousKeyboardState.IsKeyDown(Keys.K));

            if (isAltKJustPressed)
            {
                Globals.OpenHtmlFileInDefaultBrowser(@"..\..\..\Content\info.html");
            }

            bool isF11JustPressed =
                kb.IsKeyDown(Keys.F11) &&
                !(previousKeyboardState.IsKeyDown(Keys.F11));

            if (isF11JustPressed)
            {
                fullscreen = !fullscreen;
                Console.WriteLine("changed");
                Globals.GraphicsDeviceManager.HardwareModeSwitch = fullscreen;
                Globals.GraphicsDeviceManager.ApplyChanges();
            }

            bool isEJustPressed =
                kb.IsKeyDown(Keys.E) &&
                !(previousKeyboardState.IsKeyDown(Keys.E));

            if (isEJustPressed)
            {
                Globals.Player.activeItemSlot++;
                if (Globals.Player.activeItemSlot >= 6)
                {
                    Globals.Player.activeItemSlot = 1;
                }
            }

            bool isQJustPressed =
                kb.IsKeyDown(Keys.Q) &&
                !(previousKeyboardState.IsKeyDown(Keys.Q));

            if (isQJustPressed)
            {
                Globals.Player.activeItemSlot--;
                if (Globals.Player.activeItemSlot <= 0)
                {
                    Globals.Player.activeItemSlot = 5;
                }
            }

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
            
            if (LeftMousePressed)
            {
                if (Globals.Player.activeItem != null)
                {
                    Globals.Player.activeItem.Use();
                } 
            }
            
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

            if (output == new Vector2(1, 1) || output == new Vector2(1, -1))
            {
                InputOrientation = Orientation.right;
            }
            else if (output == new Vector2 (-1,1) || output == new Vector2(-1,-1))
            {
                InputOrientation = Orientation.left;
            }

            if (output.LengthSquared() > 0)
            {
                output = Vector2.Normalize(output);
            }

            MoveVector = output;

            previousKeyboardState = kb;
        }
    }
}
