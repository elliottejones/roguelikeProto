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
        private Texture2D texture;

        public Crosshair()
        {
           texture = Globals.Content.Load<Texture2D>("crosshair");
        }

        public void Draw(Vector2 mousePosition)
        {
            Globals.UISpriteBatch.Draw(texture, mousePosition, null, Color.White, 0f, new Vector2(11, 10), 1f, SpriteEffects.None, 0.01f);
        }
    }
}
