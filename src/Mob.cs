using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace csproject2024.src
{
    internal class Mob
    {
        public enum MobState
        {
            Stationary, Wondering, Following, Attacking
        }

        private Vector2 position;

        private Animation animation;
        private int walkspeed;

        private int health;
        private int maxHealth;

        public MobState state;

        private Vector2 walkVector;

        public Mob(Vector2 startPosition, Animation animation, int walkspeed, MobState initialState, int maxHealth)
        {
            this.animation = animation; 
            this.walkspeed = walkspeed;
            this.state = initialState;
            this.position = startPosition;
            this.maxHealth = maxHealth;
            this.health = maxHealth;
        }

        public void Update(Level level, Player player)
        {
            animation.Update();
            animation.playAnimation(0);
        }

        public void Draw()
        {
            Console.WriteLine("pig position: " + position.X + ", "+position.Y);
            animation.DrawAnimation(position);
        }
    }
}
