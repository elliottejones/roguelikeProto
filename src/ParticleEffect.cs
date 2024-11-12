using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

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
            float rotation;

            Vector2 scale;

            float durationCount;
            Color fadeColor;
            Color color;
            bool fade;
            public bool expired;

            public Particle(Vector2 position, Vector2 direction, Texture2D texture, float duration, float speed, Vector2 scale, bool fade, Color color, float rotation)
            {
                this.position = position;
                this.direction = direction;
                this.texture = texture;
                this.duration = duration;
                this.speed = speed;
                this.fade = fade;
                this.color = color;
                this.scale = scale;
            }  

            public void Update()
            {
                durationCount += Globals.ElapsedSeconds;
                
                if(durationCount >= duration)
                {
                    expired = true;
                    return;
                }

                if (fade)
                {
                    float fadeAmount = (durationCount * 0.7f) / duration;
                    fadeColor = new Color(color, 1 - fadeAmount);
                }
                else
                {
                    fadeColor = color;
                }
                
                position += direction * speed;
            }

            public void Draw()
            {
                Globals.SpriteBatch.Draw(texture, position, null, fadeColor, rotation, Vector2.Zero, scale, SpriteEffects.None, 0.4f);
            }
        }

        float duration;
        float spread;
        float speed;
        bool gravity;
        bool stickToPlayer;
        int count;

        Vector2 position;
        Random rng = new Random();

        Texture particleTexture;
        Vector2 particleScale;

        List<Particle> buffer;
        List<Particle> antiBuffer;

        bool fade;
        Color color;
        bool randomRotation;

        public ParticleEffect(float duration, float speed, int count, bool gravity, Vector2 particleScale, Texture particleTexture, bool fade, Color color, bool randomRotation)
        {
            this.duration = duration;
            this.speed = speed;
            this.count = count;
            this.gravity = gravity;
            this.particleScale = particleScale;
            this.particleTexture = particleTexture;

            this.buffer = new List<Particle>();
            this.antiBuffer = new List<Particle>();

            this.fade = fade;
            this.color = color;
            this.randomRotation = randomRotation;
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

                float localRotation;
                if (randomRotation)
                {
                    localRotation = SharpDX.RandomUtil.NextFloat(rng, -1, 1);
                }
                else
                {
                    localRotation = 0f;
                }

                buffer.Add(new Particle(this.position, localDirection, particleTexture.texture, duration, localSpeed, particleScale, fade, color, localRotation));
            }    
        }
    }
}
