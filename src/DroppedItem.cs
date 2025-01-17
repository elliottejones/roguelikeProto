using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace csproject2024.src
{
    internal class DroppedItem
    {
        static Sound pickupSound = new(Globals.Content.Load<SoundEffect>("pickup"));

        Texture2D icon;
        Vector2 position; // The center of the icon.
        Item item;
        public bool removed;

        public DroppedItem(Item item, Vector2 position)
        {
            this.icon = item.icon.texture;
            this.position = position;
            this.item = item;
        }

        public void Update()
        {
            Vector2 distance = Globals.Player.position - this.position;
            if(Globals.Vector2Magnitude(distance) <= 10)
            {
                PickedUp();
            }
        }

        public void PickedUp()
        {
            if(Globals.Player.GiveItem(item))
            {
                //pickupSound.Play();
                removed = true;
            }
        }

        public void Remove()
        {
            this.removed = true;
        }

        public void Draw()
        {
            Globals.SpriteBatch.Draw(icon, position, null, Color.White, 0f, new Vector2(icon.Width / 2, icon.Height / 2), 0.1f, SpriteEffects.None, 0.4f);
        }
    }
}
