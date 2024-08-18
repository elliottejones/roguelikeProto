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
        public int GCost { get; set; }
        public int HCost { get; set; }
        public Node Parent { get; set; }

        public int GridX;
        public int GridY;

        public float FCost { get { return GCost + HCost; } }

        public Node(Vector2 position, bool walkable, int gridX, int gridY)
        {
            Position = position;
            Walkable = walkable;
            GridX = gridX;
            GridY = gridY;  
        }
    }
}
