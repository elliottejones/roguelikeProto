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
        Random rng;

        public List<Mob> mobs;
        List<Mob> hitlist;
        public MobManager()
        {
            rng = new Random();
            mobs = new List<Mob>();
            hitlist = new List<Mob>();
        }

        public void newBear()
        {
            //Console.WriteLine("beer");

            rng = new Random();

            float rx = Math.Sign(rng.Next(-1, 2)) * 100;
            float ry = Math.Sign(rng.Next(-1, 2)) * 100;

            if (rx == 0)
            {
                rx += 100;
            }

            if (ry == 0)
            {
                ry -= 100;
            }

            //Console.WriteLine(rx);
            //Console.WriteLine(ry);

            Animation bearAnimation = new(10, "bear", new(Globals.Content.Load<Texture2D>("bear"), Vector2.Zero, "bear"), new Point(32, 32));
            mobs.Add(new Mob(new(Globals.Player.position.X + rx, Globals.Player.position.Y + ry), bearAnimation, 15f, Mob.MobState.Attacking, 100, attackDamage: 10));
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
