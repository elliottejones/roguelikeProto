using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace csproject2024.src
{
    internal class MobManager
    {
        public List<Mob> mobs;
        List<Mob> hitlist;
        public MobManager()
        {
            mobs = new List<Mob>();
            hitlist = new List<Mob>();

            newBear();
        }

        private void newBear()
        {
            Animation bearAnimation = new(10, "bear", new(Globals.Content.Load<Texture2D>("bear"), Vector2.Zero, "bear"), new Point(32, 32));
            mobs.Add(new Mob(new(0, 0), bearAnimation, 15f, Mob.MobState.Attacking, 100, attackDamage: 10));
        }

        public void Update(Level level, Player player)
        {
            foreach (Mob mob in mobs)
            {
                if (mob.despawned)
                {
                    hitlist.Add(mob);
                }
                else
                {
                    mob.Update(level, player);
                }
            }

            foreach (Mob mob in hitlist)
            {
                if (mobs.Contains(mob))
                {
                    mobs.Remove(mob);
                }     
            }
        }

        public void Draw()
        {
            foreach (Mob mob in mobs)
            {
                mob.Draw();
            }
        }
    }
}
