﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.DirectWrite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace csproject2024.src
{
    class Consumable : Item
    {

        public enum ConsumableType
        {
            Heal, SpeedBoost, DamageBoost, HealQuarter
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
                case (ConsumableType.HealQuarter):
                    HealQuarter();
                    break;
                case (ConsumableType.SpeedBoost):
                    SpeedBoost();
                    break;
                case (ConsumableType.DamageBoost):
                    DamageBoost();
                    break;
            }
        }

        private void DamageBoost()
        {
            Globals.Player.DamageBoost(10);
        }

        private void SpeedBoost()
        {
            Globals.Player.Boost(10, 2);
        }

        private void Heal()
        {
            Globals.Player.Heal(fraction: 1f);
        }

        private void HealQuarter()
        {
            Globals.Player.Heal(fraction: 0.25f);
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
