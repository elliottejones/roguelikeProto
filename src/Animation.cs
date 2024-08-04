using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace csproject2024.src
{
    internal class Animation
    {
        // private attributes

        private Texture spriteSheet;
        private Point frameRectangleSize;

        // public attributes

        public string name;
        public int FPS;

        // private values

        private float elapsedTime;

        private Rectangle sourceRectangle;

        private int frameNumber;
        private int stateNumber;

        private int frameCount;
        private bool active;
        private int cachedFPS;

        public Animation(int FPS, string name, Texture spriteSheet, Point frameRectangleSize)
        {
            this.FPS = FPS;
            this.cachedFPS = FPS;

            this.name = name;
            this.spriteSheet = spriteSheet;
            this.frameRectangleSize = frameRectangleSize;

            this.active = false;

            this.sourceRectangle = new Rectangle();

            frameCount = spriteSheet.texture.Width / frameRectangleSize.X;

            frameNumber = 0;
        }

        public void Update()
        {
            if (!active)
            {
                FPS = 0;
                return;
            }

            elapsedTime += Globals.ElapsedSeconds;

            if (elapsedTime > 1 / (float)FPS)
            {
                frameNumber++;

                if (frameNumber > frameCount - 1)
                {
                    frameNumber = 0;
                }

                elapsedTime = 0;
            }

            sourceRectangle = new Rectangle(new Point(frameNumber * frameRectangleSize.X, stateNumber * frameRectangleSize.Y), new Point(frameRectangleSize.X, frameRectangleSize.Y));
        }

        public void playAnimation(int stateNumber = 0)
        {
            this.stateNumber = stateNumber;
            active = true;
        }

        public void stopAnimation() // mainly for peformance reasons. not really any need to use this
        {
            if (active)
            {
                active = false;
            }
        }

        public void pauseAnimation() // freezes the animation in place
        {
            cachedFPS = FPS;
            FPS = 0;
        }

        public void resumeAnimation() // unfreezes the animation (if called before any value is cached, FPS will resort to it's initial value)
        {
            FPS = cachedFPS;
        }

        public void DrawAnimation(Vector2 position)
        {
            Globals.SpriteBatch.Draw(spriteSheet.texture, position, sourceRectangle, Color.White, 0f, new Vector2(frameRectangleSize.X/2,frameRectangleSize.Y/2), Vector2.One, SpriteEffects.None, 0.2f);
        }

        public void resetAnimation()
        {
            frameNumber = 0;
        }
    }
}
