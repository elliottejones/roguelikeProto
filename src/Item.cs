using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csproject2024.src
{
    abstract class Item
    {
        string name { get; set; }
        Texture icon { get; set; }
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
            useSound.Play();
            useParticleEffect.Instantiate(Globals.Player.position);
        }

        public virtual void Draw()
        {

        }
    }
}
