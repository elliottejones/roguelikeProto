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
        public MobManager()
        {
            mobs = new List<Mob>(); 

            Animation pigAnimation = new(10, "pig", new(Globals.Content.Load<Texture2D>("pig"), Vector2.Zero, "pig"), new Point(32, 32));
            mobs.Add(new Mob(new(0,0), pigAnimation, 30f, Mob.MobState.Wondering, 100));
        }

        public void Update(Level level, Player player)
        {
            foreach (Mob mob in mobs)
            {
                mob.Update(level,player);
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
