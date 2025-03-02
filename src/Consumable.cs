using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.DirectWrite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csproject2024.src
{
    class Consumable : Item
    {

        public enum ConsumableType
        {
            Heal, SpeedBoost, DamageBoost
        }

        public int StackCount;
        private ConsumableType type;
        Microsoft.Xna.Framework.Graphics.SpriteFont arielFont = Globals.Content.Load<Microsoft.Xna.Framework.Graphics.SpriteFont>("arialFont");

        public Consumable(string name, Texture icon, Sound useSound, ParticleEffect useParticleEffect, ConsumableType type) : base(name, icon, useSound, useParticleEffect)
        {
            this.type = type;
            StackCount = 1;
        }

        public override void AddToStack()
        {
            base.AddToStack();
            StackCount++;
        }

        private void Consume()
        {
            switch (type)
            {
                case (ConsumableType.Heal):
                    Heal();
                    break;
                case (ConsumableType.SpeedBoost):
                    SpeedBoost();
                    break;
                case (ConsumableType.DamageBoost):
                    break;
            }
        }

        private void SpeedBoost()
        {
            Globals.Player.Boost(10, 2);
        }

        private void RegenHealth()
        {

        }

        private void Heal()
        {
            Globals.Player.health = Globals.Player.maxHealth;
        }

        public override void Use()
        {
            if (StackCount > 0)
            {
                base.Use();

                Consume();

                StackCount--;

                if (StackCount == 0)
                {
                    this.Remove();
                }

                
            }
            else
            {
                this.Remove();
                Console.WriteLine("No more items to consume.");
            }
        }

        public override void DrawUI(Color color)
        {
            base.DrawUI(color);

            Globals.UISpriteBatch.DrawString(arielFont, StackCount.ToString(), base.CalculateHotbarPosition(), Color.Black);

        }
    }
}
