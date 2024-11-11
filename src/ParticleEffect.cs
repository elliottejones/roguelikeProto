using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

/*NOTES:
 *  Use remaining parameters 
 *  Add option to stick to player position
 *  Add scaling for custom particle textures
*/
namespace csproject2024.src
{
    internal class ParticleEffect
    {
        private class Particle
        {
            Vector2 position;
            Vector2 direction;
            Texture2D texture;
            float duration;
            float speed;

            float durationCount;
            Color fadeColor;
            public bool expired;

            public Particle(Vector2 position, Vector2 direction, Texture2D texture, float duration, float speed)
            {
                this.position = position;
                this.direction = direction;
                this.texture = texture;
                this.duration = duration;
                this.speed = speed;
            }  

            public void Update()
            {
                durationCount += Globals.ElapsedSeconds;
                
                if(durationCount >= duration)
                {
                    expired = true;
                    return;
                }

                float fadeAmount = (durationCount*0.7f)/duration;
                fadeColor = new Color(Color.Red, 1-fadeAmount);
                position += direction * speed;
            }

            public void Draw()
            {
                Globals.SpriteBatch.Draw(texture, position, null, fadeColor, 0f, Vector2.Zero, new Vector2(1,1), SpriteEffects.None, 0.4f);
            }
        }

        float duration;
        float spread;
        float speed;
        float speedVariation;
        bool gravity;
        int count;

        Vector2 position;
        Random rng = new Random();
        Texture texture;
        List<Particle> buffer;
        List<Particle> antiBuffer;

        public ParticleEffect(float duration, float spread, float speed, float speedVariation, int count, bool gravity, Texture texture)
        {
            this.duration = duration;
            this.spread = spread;
            this.speed = speed;
            this.count = count;
            this.texture = texture;

            this.buffer = new List<Particle>();
            this.antiBuffer = new List<Particle>();
        }

        public void Update()
        {
            foreach (Particle p in buffer)
            {
                if (p.expired)
                {
                    antiBuffer.Add(p);
                    continue;
                }

                p.Update();   
            }

            foreach (Particle p in antiBuffer)
            {
                buffer.Remove(p);
            }
            antiBuffer.Clear();

            position = Globals.Player.position;
        }

        public void Draw()
        {
            foreach (Particle p in buffer)
            {
                p.Draw();
            }
        }

        public void Instantiate(Vector2 position)
        {
            for(int i = 0; i < count; i++)
            {
                float localSpeed = SharpDX.RandomUtil.NextFloat(rng, 0.1f, 0.6f);
                localSpeed += speed;

                Vector2 localDirection = new Vector2(SharpDX.RandomUtil.NextFloat(rng,-1,1), SharpDX.RandomUtil.NextFloat(rng,-1, 1));

                buffer.Add(new Particle(this.position, localDirection, texture.texture, duration, localSpeed));
            }    
        }
    }
}
