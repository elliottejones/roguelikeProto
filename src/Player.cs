using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace csproject2024.src
{
    internal class Player
    {
        public Vector2 position;
        public Vector2 spriteTilePosition;
        public Vector2 tilePosition;
        public Tile standingTile;
        private Level level;
        private float baseSpeed;
        private float speed;

        private Animation animation;

        private Texture texture;

        private float footstepDelay;
        private float footstepTime;

        public Player(Vector2 startPosition, int baseSpeed, Level level)
        {
            this.level = level;
            position = startPosition;
            this.baseSpeed = baseSpeed;

            this.footstepTime = 0;
            this.footstepDelay = 0;

            texture = new(Globals.Content.Load<Texture2D>("character"), Vector2.Zero, "playerSpriteSheet");
            animation = new(10, "playerAnimation", texture, new Point(16, 32));
        }

        public void Update()
        {
            Vector2 projectedTilePosition = (new Vector2(position.X-8,position.Y) + InputManager.MoveVector * speed)/16;

            Tile projectedStandingTile = level.GetTileAt(projectedTilePosition);

            if (projectedStandingTile != null && projectedStandingTile.canWalkOver == true)
            {
                position += InputManager.MoveVector * speed;
            }

            spriteTilePosition = new Vector2((position.X - 8)/16, position.Y/16);
            standingTile = level.GetTileAt(spriteTilePosition);

            if (standingTile != null && standingTile.slowTile)
            {
                speed = baseSpeed / 2;
            }
            else
            {
                speed = baseSpeed;
            }

            tilePosition = position / 16;

            if (InputManager.MoveVector == Vector2.Zero)
            {
                animation.resetAnimation();
                animation.pauseAnimation();
                Globals.AudioManager.StopSound("footstepGrass");
            }
            else
            {
                animation.resumeAnimation();        
                Globals.AudioManager.PlaySound("footstepGrass", true);
            }

            animation.Update();

            switch (InputManager.InputOrientation)
            {
                case InputManager.Orientation.up:
                    {
                        animation.playAnimation(2);
                        break;
                    }
                case InputManager.Orientation.down:
                    {
                        animation.playAnimation(0);
                        break;
                    }
                case InputManager.Orientation.left:
                    {
                        animation.playAnimation(3);
                        break;
                    }
                case InputManager.Orientation.right:
                    {
                        animation.playAnimation(1);
                        break;
                    }
            }
        }

        public void Draw()
        {
            animation.DrawAnimation(position);
        }
    }
}
