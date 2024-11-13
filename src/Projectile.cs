using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using System.Text;
using System.Threading.Tasks;

namespace csproject2024.src
{
    internal class Projectile
    {
        Vector2 position;

        Vector2 moveVector;
        float speed;
        int damage;
        int penetrationCount;
        int life;

        public Projectile(Vector2 moveVector, float speed, int damage, int penetrationCount, int life)
        {
            this.moveVector = moveVector;
            this.speed = speed;
            this.penetrationCount = penetrationCount;
            this.life = life;
        }

        public void Update()
        {
            position += moveVector * speed;

            CheckForCollisions();
        }

        private void CheckForCollisions()
        {
            foreach (Mob mob in Globals.MobManager.mobs)
            {

            }
        }
    }
}
