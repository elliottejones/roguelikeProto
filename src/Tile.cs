using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace csproject2024.src
{
    internal class Tile
    {
        // Constants

        private Vector2 OUTLINE_OFFSET = new Vector2(-8,-8);

        // Independent propeties

        private Texture texture;
        private Texture2D outline;

        private Vector2 outlineDrawPosition;
        private Vector2 drawPosition;
        public Vector2 tilePosition;
        private Color color;

        private float outlineLayerHeight;
        private float layerHeight;

        private bool mouseSelected;

        public string tileType;
        
        // Player related properties

        public bool canWalkOver;
        public bool slowTile;
        private Foliage foliage;
        public bool stoodOn;

        public Tile(Texture texture, Vector2 drawPosition, bool canWalkOver, Vector2 tilePosition, bool slowTile, Foliage? foliage, Texture2D outline, String tileType)
        {
            this.texture = texture;
            this.drawPosition = drawPosition;
            this.canWalkOver = canWalkOver;
            this.tilePosition = tilePosition;
            this.slowTile = slowTile;
            this.foliage = foliage;
            this.outline = outline;
            this.tileType = tileType;

            Random random = new();
            float outlineLayerOffset = SharpDX.RandomUtil.NextFloat(random, -0.01f, 0.01f);

            if (texture.name == "grass")
            {
                layerHeight = 0.5f;
                outlineLayerHeight = 0.55f;
            }
            else if (texture.name == "sand")
            {
                layerHeight = 0.6f;
                outlineLayerHeight = 0.65f;
            }
            else
            {
                layerHeight = 0.7f;
                outlineLayerHeight = -1f;
            }

            outlineLayerHeight += outlineLayerOffset;
            outlineDrawPosition = drawPosition + OUTLINE_OFFSET;

        }

        public void MouseSelect()
        {
            mouseSelected = true;
        }

        public void MouseDeselect()
        {
            mouseSelected = false;  
        }


        public void Draw(Player player)
        {
            if (mouseSelected == true && this.tileType != "water")
            {
                color = Color.LightGray;
            }
            else
            {
                color = Color.White;
            }

            if (stoodOn) // Just for debugging
            {
                //color = Color.Red;
            }

            Globals.SpriteBatch.Draw(texture.texture, drawPosition, null, color, 0f, Vector2.Zero, Vector2.One, SpriteEffects.None, layerHeight);

            if (foliage != null)
            {
                foliage.Draw(player);
            }

            if (outline != null)
            {
                Globals.SpriteBatch.Draw(outline, outlineDrawPosition, null, color, 0f, Vector2.Zero, Vector2.One, SpriteEffects.None, outlineLayerHeight);
            }
        }
    }
}
