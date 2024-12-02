using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csproject2024.src
{
    class Consumable : Item
    {
        public int StackCount;

        public Consumable(string name, Texture icon, Sound useSound, ParticleEffect useParticleEffect) : base(name, icon, useSound, useParticleEffect)
        {

        }

        public override void Use()
        {
            if (StackCount > 0)
            {
                base.Use();

                Consume();

                StackCount--;
            }
            else
            {
                Console.WriteLine("No more items to consume.");
            }
        }

        private void Consume()
        {
            
        }
    }
}
