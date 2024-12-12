using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace csproject2024.src
{
    class Weapon : Item
    {    
        List<Projectile> projectileList = new List<Projectile>();
        List<Projectile> despawnedProjectileList = new List<Projectile>();

        Texture projectileTexture;

        float baseProjectileSpeed;
        int baseDamage;
        int basePenetration;
        int baseLife;

        public Weapon(string name, Texture icon, Sound useSound, ParticleEffect useParticleEffect, int damage, Texture projectileTexture): base(name, icon, useSound, useParticleEffect)
        {
            this.baseDamage = damage;
            this.projectileTexture = projectileTexture;
        }

        public override void Use()
        {
            base.Use();

            Attack();
        }

        private void Attack()
        {
            Console.WriteLine("aTACAK");
            Vector2 mouseScreenPosition = new Vector2(InputManager.MousePosition.X, InputManager.MousePosition.Y);
            Vector2 mouseWorldPosition = Vector2.Transform(mouseScreenPosition, Matrix.Invert(Globals.camera.Transform));

            Vector2 mouseVector = mouseWorldPosition - Globals.Player.position;

            projectileList.Add(new(mouseVector, 10, 10, 1, 2, projectileTexture.texture));
        }

        public override void Update()
        {
            foreach (Projectile p in projectileList)
            {
                p.Update();
                if (p.despawned)
                {
                    despawnedProjectileList.Add(p);
                }
            }

            foreach (Projectile p in despawnedProjectileList)
            {
                projectileList.Remove(p);
            }
            despawnedProjectileList.Clear();
        }

        public override void Draw()
        {
            base.Draw();

            foreach (Projectile p in projectileList)
            {
                p.Draw();
            }
        }
    }
}
