using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace csproject2024.src
{
    internal class Player
    {
        public Vector2 position;
        public Vector2 spriteTilePosition;
        public Vector2 tilePosition;
        public Tile standingTile;
        private Level level;
        private float baseSpeed;
        private float speed;

        private Animation animation;

        private Texture texture;

        private float footstepDelay;
        private float footstepTime;

        public int health;
        public int maxHealth;

        private float naturalHealTime;
        private float healCounter;
        private int naturalHealAmount;

        private ParticleEffect damageParticle;

        public Item[] items;
        public int activeItemSlot;
        public Item activeItem;

        public Player(Vector2 startPosition, int baseSpeed, Level level, int maxHealth)
        {
            this.level = level;
            position = startPosition;
            this.baseSpeed = baseSpeed;

            this.health = 20;
            this.maxHealth = maxHealth;

            this.footstepTime = 0;
            this.footstepDelay = 0;

            naturalHealAmount = 1;
            naturalHealTime = 1f;
            healCounter = 0f;

            damageParticle = new ParticleEffect(0.8f, 0.03f, 100, false, new(0.5f,0.5f), new(Globals.Content.Load<Texture2D>("hitParticle"), Vector2.Zero, "uibit"), true, Color.White, true);
            texture = new(Globals.Content.Load<Texture2D>("character"), Vector2.Zero, "playerSpriteSheet");
            animation = new(10, "playerAnimation", texture, new Point(16, 32));

            items = new Item[4];
            activeItemSlot = 1;

            this.GiveItem(Globals.GetItemPreset.Glock());
            this.GiveItem(Globals.GetItemPreset.Glock());
            this.GiveItem(Globals.GetItemPreset.Glock());
            this.GiveItem(Globals.GetItemPreset.Glock());
        }

        public bool GiveItem(Item item)
        {
            for (int i = 0; i < 4; i++)
            {
                if (items[i] == null)
                {
                    items[i] = item;
                    items[i].hotbarSlot = i+1;
                    return true;
                }
            }

            return false;
        }
        private void UpdateHealthBar()
        {
            Globals.ScreenGUI.UpdateAttribute("text", "healthText", $"{health}");
            Globals.ScreenGUI.UpdateAttribute("width", "healthBar", $"{(int)Math.Round((float)((float)health / (float)maxHealth) * 180)}");
        }

        public void Damage(int damage)
        {
            health -= damage;

            Globals.AudioManager.PlaySound("uuh", false);
            damageParticle.Instantiate(position);

            Globals.ScreenGUI.damageBorder.Damage(damage);

            UpdateHealthBar();
        }

        public void Update()
        {
            activeItem = items[activeItemSlot - 1];

            damageParticle.Update();

            healCounter += Globals.ElapsedSeconds;
            
            if (healCounter >= naturalHealTime)
            {
                health += naturalHealAmount;
                healCounter = 0f;

                if (health >= maxHealth)
                {
                    health = maxHealth;
                    Console.WriteLine("set health to " + maxHealth);
                }

                UpdateHealthBar();
            }
            

            Vector2 projectedTilePosition = (new Vector2(position.X-8,position.Y) + InputManager.MoveVector * speed)/16;

            Tile projectedStandingTile = level.GetTileAt(projectedTilePosition);

            if (projectedStandingTile != null && projectedStandingTile.canWalkOver == true)
            {
                position += InputManager.MoveVector * speed;
            }

            spriteTilePosition = new Vector2((position.X - 8)/16, position.Y/16);
            standingTile = level.GetTileAt(spriteTilePosition);

            if (standingTile != null && standingTile.slowTile)
            {
                speed = baseSpeed / 2;
            }
            else
            {
                speed = baseSpeed;
            }

            tilePosition = position / 16;

            if (InputManager.MoveVector == Vector2.Zero)
            {
                animation.resetAnimation();
                animation.pauseAnimation();
                Globals.AudioManager.StopSound("footstepGrass");
            }
            else
            {
                animation.resumeAnimation();        
                Globals.AudioManager.PlaySound("footstepGrass", true);
            }

            animation.Update();

            switch (InputManager.InputOrientation)
            {
                case InputManager.Orientation.up:
                    {
                        animation.playAnimation(2);
                        break;
                    }
                case InputManager.Orientation.down:
                    {
                        animation.playAnimation(0);
                        break;
                    }
                case InputManager.Orientation.left:
                    {
                        animation.playAnimation(3);
                        break;
                    }
                case InputManager.Orientation.right:
                    {
                        animation.playAnimation(1);
                        break;
                    }
            }

            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] != null)
                {
                    items[i].Update();
                }
            }
        }

        public void Draw()
        {
            foreach (Item item in items)
            {
                if (item != null)
                {
                    item.Draw();
                }        
            }
            animation.DrawAnimation(position);
            damageParticle.Draw();
        }
    }
}
