using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
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
        public Dictionary<string, Song> songs = new Dictionary<string, Song>();


        public AudioManager()
        {
            sounds.Add("footstepGrass", new(Globals.Content.Load<SoundEffect>("footstepGrass")));
            sounds.Add("treeFade", new(Globals.Content.Load<SoundEffect>("treeFade")));

            songs.Add("ambience", Globals.Content.Load<Song>("ambience"));
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

        public void StopSound(string soundName)
        {
            sounds[soundName].Stop(); 
        }

        public void PlaySong(string songName)
        {
            MediaPlayer.Play(songs[songName]);
        }

        public void StopSong(string songName)
        {
            MediaPlayer.Stop();
        }
    }
}
