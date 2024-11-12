using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csproject2024.src
{
    public class Item
    {
        string Name;
        Texture Icon;
        Sound UseSound;
        ParticleEffect UseParticleEffect;

        public virtual void Use()
        {
            UseSound.Play();
            UseParticleEffect.Instantiate(Globals.Player.position);
        }
    }
}
