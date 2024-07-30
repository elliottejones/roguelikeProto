using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csproject2024.src
{
    internal class Texture
    {
        public Texture2D texture;
        public Vector2 origin;
        public string name;

        private bool animated;

        private Texture2D privateTexture1;
        private Texture2D privateTexture2;

        private float elapsedTime;
        private float changeInterval;

        public Texture(Texture2D texture, Vector2 origin, string name, bool animated, Texture2D otherTexture = null)
        {
            this.texture = texture;
            this.origin = origin;
            this.name = name;

            this.animated = animated;

            if (origin == Vector2.One )
            {
                this.origin = new Vector2(texture.Width/2, texture.Height/2); 
            }

        }

        public void UpdateAnimation()
        {
            if (animated)
            {
                elapsedTime += Globals.ElapsedSeconds;

                if (elapsedTime >= changeInterval)
                {
                    if (texture == privateTexture1)
                    {
                        texture = privateTexture2;
                    }
                    else
                    {
                        texture = privateTexture1;
                    }

                    elapsedTime = 0;
                }
            }
            
        }
    }
}
