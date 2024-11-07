using System;
using System.Collections.Generic;
using System.DirectoryServices;
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

        public ScreenCollider ScreenCollider;

        private Vector2 position;
        private Tile currentTile;

        private Animation animation;
        private float walkspeed;

        private int health;
        private int maxHealth;

        private int despawnTimer = 0;
        public bool despawned = false;

        public MobState state;

        private List<Vector2> currentPath = new List<Vector2>();
        private Vector2 lastWalkVector;
        private int currentPathIndex = 0;
        private Vector2 lastPlayerPosition;
        private float pathUpdateTimer = 0f;
        private const float PATH_UPDATE_INTERVAL = 1f; // Update path every 1 second

        private float hitTimer;
        private float hitDelay = 1f; //time between mob attacks on player
        private int attackDamage;

        private int wonderCounter = 0;

        AStar pathfinder;
        private int currentCheckpointIndex = 0;
        public Mob(Vector2 startPosition, Animation animation, float walkspeed, MobState initialState, int maxHealth, int attackDamage = 0)
        {
            
            this.animation = animation; 
            this.walkspeed = walkspeed;
            this.state = initialState;
            this.position = startPosition;
            this.maxHealth = maxHealth;
            this.health = maxHealth;
            this.attackDamage = attackDamage;

            this.lastPlayerPosition = new(0, 0);

            this.pathfinder = new AStar();
            this.ScreenCollider = new ScreenCollider(128, 128);
        }

        public void Update(Level level, Player player)
        {
            ScreenCollider.Update(new Point((int)position.X,(int)position.Y));

            if (despawnTimer >= 100)
            {
                this.despawned = true;
            }

            //Console.WriteLine($"Mob world position: {position}, Mob tile position: {currentTile?.tilePosition}");
            animation.Update();
            animation.playAnimation(GetFacingDirection(lastWalkVector));
            currentTile = level.GetTileAt(position/16);

            pathUpdateTimer += (float)Globals.ElapsedSeconds;
            hitTimer += (float)Globals.ElapsedSeconds;

            // Check if we need to recalculate the path
            if (pathUpdateTimer >= PATH_UPDATE_INTERVAL || Vector2.Distance(player.standingTile.tilePosition, lastPlayerPosition) > 2f)
            {
                pathUpdateTimer = 0f;
                if (state == MobState.Following)
                {
                    RecalculatePathPlayer(level, player);
                }
                else if(state == MobState.Wondering)
                {
                    if (wonderCounter >= 500)
                    {
                        wonderCounter = 0;
                        Random rngX = new Random();
                        Random rngY = new Random();
                        int offsetX = rngX.Next(-5, 5);
                        int offsetY = rngY.Next(-5, 5);
                        Vector2 wonderPos = new Vector2(this.currentTile.tilePosition.X + offsetX, this.currentTile.tilePosition.Y + offsetY);
                        RecalculatePathTilePosition(level, wonderPos);
                    }
                    else
                    {
                        wonderCounter++;
                    }
                }
                if (state == MobState.Attacking)
                {
                    RecalculatePathPlayer(level, player);
                    Vector2 positionDelta = this.position - player.position;
                    if (Math.Sqrt(positionDelta.X* positionDelta.X + positionDelta.Y  * positionDelta.Y) <= 10 && hitTimer >= hitDelay)
                    {
                        hitTimer = 0;
                        player.Damage(attackDamage);
                    }
                }
                
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
                    lastWalkVector = direction;
                }
                else
                {
                    currentPathIndex++;
                }

                //Console.WriteLine($"Mob position: {position}, Moving towards: {targetPosition}");
            }
        }

        private void RecalculatePathTilePosition(Level level, Vector2 tilePos)
        {
            Tile tile = level.GetTileAt(tilePos);
            
            if (IsValidTile(currentTile) && IsValidTile(tile) && tile.tileType != "water")
            {
                pathfinder.CalculatePath(currentTile, tile);
                currentPath = pathfinder.checkPoints;
                currentPathIndex = 0;
                despawnTimer = 0;
                //Console.WriteLine($"Recalculated path from {currentTile.tilePosition} to {tile.tilePosition}");
            }
        }
        private void RecalculatePathPlayer(Level level, Player player)
        {
            if (IsValidTilePlayer(currentTile) && IsValidTilePlayer(player.standingTile))
            {
                pathfinder.CalculatePath(currentTile, player.standingTile);
                currentPath = pathfinder.checkPoints;
                currentPathIndex = 0;
                lastPlayerPosition = player.standingTile.tilePosition;
                despawnTimer = 0;
                //Console.WriteLine($"Recalculated path from {currentTile.tilePosition} to {player.standingTile.tilePosition}");
            }
            else
            {
                despawnTimer++;
                Console.WriteLine($"Invalid tile position. Mob: {currentTile?.tilePosition}, Player: {player.standingTile?.tilePosition}");
            }
        }

        private bool IsValidTilePlayer(Tile tile)
        {
            return tile != null && Vector2.Distance(tile.tilePosition, Globals.Player.tilePosition) < 16;

        }

        private bool IsValidTile(Tile tile)
        {
            return tile != null;
        }

        public void Draw()
        {
            animation.DrawAnimation(position);
        }

        private int GetFacingDirection(Vector2 direction)
        {
            if (Math.Abs(direction.X) > Math.Abs(direction.Y))
            {
                // Horizontal movement
                if (direction.X > 0)
                    return 1; // Right
                else
                    return 3; // Left
            }
            else
            {
                // Vertical movement
                if (direction.Y > 0)
                    return 0; // Down
                else
                    return 2; // Up
            }
        }
    }
}
