using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csproject2024.src
{
    class Item // This is what i call good code
    {
        public static Texture iconBorder = new(Globals.Content.Load<Texture2D>("itemborder"), Vector2.Zero, "itemborder");
        public string name { get; set; }
        public Texture icon { get; set; }
        Sound useSound { get; set; }
        ParticleEffect useParticleEffect { get; set; }

        public int hotbarSlot;

        public Item(string name, Texture Icon, Sound useSound, ParticleEffect useParticleEffect)
        {
            this.name = name;
            this.icon = Icon;
            this.useSound = useSound;
            this.useParticleEffect = useParticleEffect;
        }

        public virtual void Use()
        {
            useSound.PlayQuick();
            if (useParticleEffect != null)
            {
                useParticleEffect.Instantiate(Globals.Player.position);
            }
                    
        }

        public virtual void Update()
        {
            if (useParticleEffect != null)
            {
                useParticleEffect.Update();
            }
        }

        public virtual void AddToStack() // Is overrided by consumable
        {
            
        }

        public void Remove()
        {
            Globals.Player.RemoveItem(this);
        }

        public Vector2 CalculateHotbarPosition()
        {
            int yPos = 970;
            int xPos = 690 + ((hotbarSlot - 1) * 110);
            return new Vector2(xPos, yPos);
        }

        public virtual void Draw()
        {
            if (useParticleEffect != null)
            {
                useParticleEffect.Draw();
            }     
        }

        public virtual void DrawUI(Color color)
        {
            Globals.UISpriteBatch.Draw(icon.texture, CalculateHotbarPosition(), null, color, 0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 0.2f);
        }
    }
}
