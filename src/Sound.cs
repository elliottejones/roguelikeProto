using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace csproject2024.src
{
    internal class Sound
    {
        private SoundEffect sound;
        public SoundEffectInstance soundInstance;

        public Sound(SoundEffect sound)
        {
            this.sound = sound;
            soundInstance = sound.CreateInstance();
        }

        public void Play()
        {
            soundInstance.Play();
        }

        public void PlayLooped()
        {
            soundInstance.Play();
        }

        public void Stop()
        {
            soundInstance.Stop();
        }
    }
}
