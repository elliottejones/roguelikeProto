using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace csproject2024.src
{
    public class Weapon : Item
    {
        
        List<Projectile> projectileList;
        Texture projectileTexture;

        float baseProjectileSpeed;
        int baseDamage;
        int basePenetration;
        int baseLife;

        public override void Use()
        {
            base.Use();

            Attack();
        }

        private void Attack()
        {
            Vector2 mouseScreenPosition = new Vector2(InputManager.MousePosition.X, InputManager.MousePosition.Y);
            Vector2 mouseWorldPosition = Vector2.Transform(mouseScreenPosition, Matrix.Invert(Globals.camera.Transform));

            Vector2 mouseVector = mouseWorldPosition - Globals.Player.position;

            projectileList.Add(new(mouseVector, 10, 10, 1, 2));

        }
    }
}
