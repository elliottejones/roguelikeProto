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
        List<Mob> mobs;
        List<Mob> hitlist;
        public MobManager()
        {
            mobs = new List<Mob>();
            hitlist = new List<Mob>();

            Animation pigAnimation = new(10, "pig", new(Globals.Content.Load<Texture2D>("pig"), Vector2.Zero, "pig"), new Point(32, 32));
            mobs.Add(new Mob(new(0,0), pigAnimation, 15f, Mob.MobState.Wondering, 100));
            mobs.Add(new Mob(new(0, 0), pigAnimation, 15f, Mob.MobState.Wondering, 100));
           

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
