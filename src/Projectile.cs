using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace csproject2024.src
{
    internal class Projectile
    {
        Vector2 position;
        float rotation;
        public bool despawned;

        Texture2D texture;
        Vector2 moveVector;
        float speed;
        int damage;
        int penetrationCount;
        float life;

        public Projectile(Vector2 moveVector, float speed, int damage, int penetrationCount, float life, Texture2D texture)
        {
            this.position = Globals.Player.position;
            this.moveVector = moveVector;
            this.speed = speed;
            this.penetrationCount = penetrationCount;
            this.life = life;
            this.damage = damage;
            this.texture = texture;      
        }

        public void Update()
        {
            life -= Globals.ElapsedSeconds;
            position += Vector2.Normalize(moveVector) * speed;
            rotation = (float)Math.Atan2(Vector2.Normalize(moveVector).Y, Vector2.Normalize(moveVector).X);
            CheckForCollisions();
            if (penetrationCount <= 0)
            {
                despawned = true;
            }
            if (life <= 0)
            {
                despawned = true;
            }
        }

        public void Draw(Vector2 origin)
        {
            if (origin!= Vector2.Zero)
            {
                origin = new(Math.Sign(rotation)*-15, 10 * (float)Math.Cos(rotation));
            }
            Globals.SpriteBatch.Draw(texture, position - origin, null, Color.White, rotation + 3.14f/2, Vector2.Zero, 0.3f, SpriteEffects.None, 0.4f);
        }

        private void CheckForCollisions()
        {
            foreach (Mob mob in Globals.MobManager.mobs)
            {
                float mobPositionModulus = Globals.Vector2Magnitude(mob.position - position);
                if (mobPositionModulus <= 10)
                {
                    int ifBoosted = damage;

                    if (Globals.damageBoost)
                    {

                        //Console.WriteLine("damage is boosted");
                        ifBoosted *= 2;
                    }

                    mob.Damage(ifBoosted);
                    this.penetrationCount--;
                }

                if (penetrationCount == 0)
                {
                    break;
                }
            }
        }
    }
}
