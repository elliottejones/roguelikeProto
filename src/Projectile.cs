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
        int life;

        public Projectile(Vector2 moveVector, float speed, int damage, int penetrationCount, int life, Texture2D texture)
        {
            this.moveVector = moveVector;
            this.speed = speed;
            this.penetrationCount = penetrationCount;
            this.life = life;
            this.texture = texture;
        }

        public void Update()
        {
            position += moveVector * speed;
            rotation = (float)Math.Atan2(Vector2.Normalize(moveVector).Y, Vector2.Normalize(moveVector).X);
            CheckForCollisions();
            if (penetrationCount == 0)
            {
                despawned = true;
            }
        }

        public void Draw()
        {
            Globals.SpriteBatch.Draw(texture, position, null, Color.White, rotation, Vector2.Zero, 1f, SpriteEffects.None, 0.4f);
        }

        private void CheckForCollisions()
        {
            foreach (Mob mob in Globals.MobManager.mobs)
            {
                float mobPositionModulus = Globals.Vector2Magnitude(mob.position - position);
                if (mobPositionModulus <= 10)
                {
                    mob.Damage(damage);
                    this.penetrationCount--;
                }
            }
        }
    }
}
