using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csproject2024.src
{
    internal class AudioManager
    {
        public Dictionary<string, Sound> sounds = new Dictionary<string, Sound>();


        public AudioManager()
        {
            sounds.Add("footstepGrass", new(Globals.Content.Load<SoundEffect>("footstepGrass")));
        }

        public void PlaySound(string soundName, bool looped = false)
        {
            if (looped)
            {
                sounds[soundName].PlayLooped();
            }
            else
            {
                sounds[soundName].Play();
            }   
        }

    }
}
