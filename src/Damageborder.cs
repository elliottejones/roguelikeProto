using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csproject2024.src
{
    internal class Damageborder
    {
        Texture border;
        float borderTimer;
        float borderDuration;
        float borderFade;
        bool showDamageIndicator;

        float permanentBorderFade;
        bool showPermanentDamageIndicator;

        public Damageborder(Texture border)
        {
            this.border = border;
            this.borderTimer = 10f;
            this.borderFade = 0f;
            this.borderDuration = 1f;
            this.showDamageIndicator = false;

            this.permanentBorderFade = 0f;
            this.showPermanentDamageIndicator = false;
        }

        public void Update(int maxHealth, float health)
        {
            borderTimer += Globals.ElapsedSeconds;
            borderFade = MathHelper.Clamp(1f - (borderTimer / borderDuration), 0f, 1f);

            float deltaHealth = maxHealth - health;
            if (deltaHealth > (maxHealth - 10))
            {
                showPermanentDamageIndicator = true;

                Console.WriteLine(health/10);

                permanentBorderFade = MathHelper.Clamp(1f - (health/10), 0f, 1f);
                Console.WriteLine(permanentBorderFade);
            }
            else
            {
                showPermanentDamageIndicator = false;
            }
            
            showDamageIndicator = borderFade > 0f;

        }

        public void Damage(int damageAmount)
        {
            borderTimer = 0f;
        }

        public void Draw()
        {
            if (showDamageIndicator)
            {
                Globals.UISpriteBatch.Draw(border.texture, Vector2.Zero, null, Color.Red * borderFade, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.7f);
            }
            if (showPermanentDamageIndicator)
            {
                Globals.UISpriteBatch.Draw(border.texture, Vector2.Zero, null, Color.Red * permanentBorderFade, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.7f);
            }

        }
    }
}
