using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace csproject2024.src
{
    internal class Texture
    {
        public Texture2D texture;
        public Vector2 origin;
        public string name;

        public Texture(Texture2D texture, Vector2 origin, string name)
        {
            this.texture = texture;
            this.origin = origin;
            this.name = name;

            if (origin == Vector2.One )
            {
                this.origin = new Vector2(texture.Width/2, texture.Height/2); 
            }

        }
    }
}
