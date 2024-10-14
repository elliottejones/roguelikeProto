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
        private Tile currentTile;

        private Animation animation;
        private float walkspeed;

        private int health;
        private int maxHealth;

        public MobState state;

        private List<Vector2> currentPath = new List<Vector2>();
        private Vector2 walkVector;
        private int currentPathIndex = 0;
        private Vector2 lastPlayerPosition;
        private float pathUpdateTimer = 0f;
        private const float PATH_UPDATE_INTERVAL = 1f; // Update path every 1 second

        AStar pathfinder;
        private int currentCheckpointIndex = 0;
        public Mob(Vector2 startPosition, Animation animation, float walkspeed, MobState initialState, int maxHealth)
        {
            this.animation = animation; 
            this.walkspeed = walkspeed;
            this.state = initialState;
            this.position = startPosition;
            this.maxHealth = maxHealth;
            this.health = maxHealth;

            this.lastPlayerPosition = new(0, 0);

            this.pathfinder = new AStar();
        }

        public void Update(Level level, Player player)
        {
            //Console.WriteLine($"Mob world position: {position}, Mob tile position: {currentTile?.tilePosition}");
            animation.Update();
            animation.playAnimation(0);
            currentTile = level.GetTileAt(position/16);

            pathUpdateTimer += (float)Globals.ElapsedSeconds;

            // Check if we need to recalculate the path
            if (pathUpdateTimer >= PATH_UPDATE_INTERVAL || Vector2.Distance(player.standingTile.tilePosition, lastPlayerPosition) > 2f)
            {
                RecalculatePath(level, player);
                pathUpdateTimer = 0f;
            }


            // Follow the current path
            if (currentPath.Count > 0 && currentPathIndex < currentPath.Count)
            {
                Vector2 targetPosition = currentPath[currentPathIndex]*16;
                Vector2 direction = targetPosition - position;

                if (direction.Length() > 1f)
                {
                    direction.Normalize();
                    position += direction * walkspeed * (float)Globals.ElapsedSeconds;
                }
                else
                {
                    currentPathIndex++;
                }

                //Console.WriteLine($"Mob position: {position}, Moving towards: {targetPosition}");
            }
        }


        private void RecalculatePath(Level level, Player player)
        {
            if (IsValidTile(currentTile) && IsValidTile(player.standingTile))
            {
                pathfinder.CalculatePath(currentTile, player.standingTile);
                currentPath = pathfinder.checkPoints;
                currentPathIndex = 0;
                lastPlayerPosition = player.standingTile.tilePosition;

                //Console.WriteLine($"Recalculated path from {currentTile.tilePosition} to {player.standingTile.tilePosition}");
            }
            else
            {
                Console.WriteLine($"Invalid tile position. Mob: {currentTile?.tilePosition}, Player: {player.standingTile?.tilePosition}");
            }
        }

        private bool IsValidTile(Tile tile)
        {
            return tile != null && tile.tilePosition.X >= Globals.Player.tilePosition.X - 32 && tile.tilePosition.X < Globals.Player.tilePosition.X + 32 && tile.tilePosition.Y >= Globals.Player.tilePosition.X - 32 && tile.tilePosition.Y < Globals.Player.tilePosition.Y + 32;

        }

        public void Draw()
        {
            animation.DrawAnimation(position);
        }
    }
}
