using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.Xna.Framework;

namespace csproject2024.src
{
    internal class Node
    {
        public Vector2 Position { get; set; }
        public bool Walkable { get; set; }
        public float GCost { get; set; }
        public float HCost { get; set; }
        public Node Parent { get; set; }

        public float FCost { get { return GCost + HCost; } }

        public Node(Vector2 position, bool walkable)
        {
            Position = position;
            Walkable = walkable;
        }
    }
}
