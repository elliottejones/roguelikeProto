using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csproject2024.src
{
    internal class Crosshair
    {
        Texture texture = new(Globals.Content.Load<Texture2D>("crosshair"), new Vector2(11, 10), "crosshair", false);

        public void Draw(Vector2 mousePosition)
        {
            Globals.UISpriteBatch.Draw(texture.texture, mousePosition, null, Color.White, 0f, texture.origin, 1f, SpriteEffects.None, 0.01f);
        }
    }
}
