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
        private Texture2D texture;
        private float speed;


        public Player(Vector2 startPosition, int baseSpeed, Level level)
        {
            this.level = level;
            position = startPosition;
            this.baseSpeed = baseSpeed;
            texture = Globals.Content.Load<Texture2D>("character");
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
        }

        public void Draw()
        {
            Globals.SpriteBatch.Draw(texture, position, null, Color.White, 0f, new Vector2(texture.Width / 2, texture.Height / 2), Vector2.One, SpriteEffects.None, 0.2f);
        }
    }
}
