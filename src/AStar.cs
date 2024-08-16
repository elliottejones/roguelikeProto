using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace csproject2024.src
{
    internal class AStar
    {
        public static Rectangle gridBounds;

        public Tile[,] consideredTiles;

        Node[,] nodeMap = new Node[32, 32];

        public List<Node> GetNeighbors(Node node)
        {
            List<Node> neighbors = new List<Node>();

            Vector2[] directions = new Vector2[]
            {
                new Vector2(-1, 0),  // Left
                new Vector2(1, 0),   // Right
                new Vector2(0, -1),  // Up
                new Vector2(0, 1),   // Down
                new Vector2(-1, -1), // Top Left
                new Vector2(1, -1),  // Top Right
                new Vector2(-1, 1),  // Bottom Left
                new Vector2(1, 1)    // Bottom Right
            };

            foreach (Vector2 direction in directions)
            {
                Vector2 checkVector = node.Position;

                Tile checkTile = Globals.Level.GetTileAt(checkVector);

                if (checkTile != null)
                {
                    if (checkTile.canWalkOver == true)
                    {
                        Node neighborNode = nodeMap[(int)(node.GridX + direction.X), (int)(node.GridX + direction.Y)];
                        neighbors.Add(neighborNode);
                    }
                }
            }

            return neighbors;
        }

        public void CalculatePath(Tile startTile, Tile endTile)
        {
            Node startNode = null;
            Node endNode = null;

            for (int x = 0; x < 32; x++)
            {
                for (int y = 0; y < 32; y++)
                {
                    bool walkable = consideredTiles[x, y].canWalkOver;
                    Vector2 position = consideredTiles[x, y].tilePosition;
                    Node newNode = new(position, walkable, x, y);

                    if (consideredTiles[x, y] == startTile)
                    {
                        startNode = newNode;
                    }

                    if (consideredTiles[x, y] == endTile)
                    {
                        endNode = newNode;
                    }

                    nodeMap[x, y] = newNode;
                }
            }

            List<Node> openSet = new List<Node>();
            HashSet<Node> closedSet = new HashSet<Node>();

            openSet.Add(startNode);

            while(openSet.Count > 0)
            {
                Node currentNode = openSet[0];
                for (int i = 1; i < openSet.Count; i++)
                {
                    if (openSet[i].FCost < currentNode.FCost || (openSet[i].FCost == currentNode.FCost && openSet[i].HCost < currentNode.HCost))
                    {
                        currentNode = openSet[i];
                        openSet.Remove(currentNode);
                        closedSet.Add(currentNode);

                        if (currentNode == endNode)
                        {
                            return;
                        }

                        foreach (Tile neighbors in GetNeighbors(currentNode))
                        {

                        }
                    }
                }
            }

        }

        
    }
}
