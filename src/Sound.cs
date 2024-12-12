using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace csproject2024.src
{
    public class Sound
    {
        private SoundEffect sound;
        private SoundEffectInstance soundInstance;

        public bool IsActive => soundInstance.State == SoundState.Playing;

        public Sound(SoundEffect sound)
        {
            this.sound = sound;
            soundInstance = sound.CreateInstance();
        }

        public void PlayQuick() // inefficient but good for fast repeating sounds (garbage collection overhead)
        {
            SoundEffectInstance quickSound = sound.CreateInstance();
            quickSound.Play();
        }

        public void Play()
        {
            if (soundInstance.State != SoundState.Playing)
            {
                soundInstance.IsLooped = false;
                soundInstance.Play();
            }
        }

        public void PlayLooped()
        {
            if (soundInstance.State != SoundState.Playing)
            {
                soundInstance.IsLooped = true;
                soundInstance.Play();
            }
        }

        public void Stop()
        {
            if (soundInstance.State == SoundState.Playing)
            {
                soundInstance.Stop();
            }
        }
    }
}

