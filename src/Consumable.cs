using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csproject2024.src
{
    public class Consumable : Item
    {
        public int StackCount;

        public override void Use()
        {
            if (StackCount > 0)
            {
                base.Use();

                Consume();

                StackCount--;
            }
            else
            {
                Console.WriteLine("No more items to consume.");
            }
        }

        private void Consume()
        {
            
        }
    }
}
