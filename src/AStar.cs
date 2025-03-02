using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace csproject2024.src
{
    internal class AStar
    {
        public static Tile[,] consideredTiles;
        private Node[,] nodeMap = new Node[32, 32];
        public List<Vector2> checkPoints = new List<Vector2>();

        private List<Node> GetNeighbors(Node node)
        {
            List<Node> neighbors = new List<Node>();
            Vector2[] directions = new Vector2[]
            {
                new Vector2(-1, 0), new Vector2(1, 0),   // Left, Right
                new Vector2(0, -1), new Vector2(0, 1),   // Up, Down
                new Vector2(-1, -1), new Vector2(1, -1), // Top Left, Top Right
                new Vector2(-1, 1), new Vector2(1, 1)    // Bottom Left, Bottom Right
            };

            foreach (Vector2 direction in directions)
            {
                int newX = (int)(node.GridX + direction.X);
                int newY = (int)(node.GridY + direction.Y);

                if (newX >= 0 && newX < nodeMap.GetLength(0) && newY >= 0 && newY < nodeMap.GetLength(1))
                {
                    Node neighborNode = nodeMap[newX, newY];
                    if (neighborNode != null && neighborNode.Walkable)
                    {
                        neighbors.Add(neighborNode);
                    }
                }
            }

            return neighbors;
        }

        private int GetDistance(Node node1, Node node2)
        {
            int xDistance = Math.Abs(node1.GridX - node2.GridX);
            int yDistance = Math.Abs(node1.GridY - node2.GridY);

            if (xDistance > yDistance)
            {
                return 14 * yDistance + 10 * (xDistance - yDistance);
            }
            return 14 * xDistance + 10 * (yDistance - xDistance);
        }

        private void RetracePath(Node startNode, Node endNode)
        {
            List<Node> path = new List<Node>();
            Node currentNode = endNode;

            while (currentNode != startNode)
            {
                path.Add(currentNode);
                currentNode = currentNode.Parent;
            }

            path.Reverse();

            checkPoints.Clear();

            foreach (Node node in path)
            {
                checkPoints.Add(new Vector2(node.Position.X, node.Position.Y));
            }
        }

        public void CalculatePath(Tile startTile, Tile endTile)
        {
            if (consideredTiles == null)
            {
                Console.WriteLine("Error: consideredTiles is null");
                return;
            }

            ResetNodes();

            Node startNode = GetNodeFromTile(startTile);
            Node endNode = GetNodeFromTile(endTile);

            if (startNode == null || endNode == null)
            {
                Console.WriteLine("Error: Start or end node is null. Cannot calculate path.");
                return;
            }

            List<Node> openSet = new List<Node>();
            HashSet<Node> closedSet = new HashSet<Node>();

            openSet.Add(startNode);

            while (openSet.Count > 0)
            {
                Node currentNode = openSet.OrderBy(n => n.FCost).ThenBy(n => n.HCost).First();
                openSet.Remove(currentNode);

                if (currentNode == endNode)
                {
                    RetracePath(startNode, endNode);
                    return;
                }

                closedSet.Add(currentNode);

                foreach (Node neighbor in GetNeighbors(currentNode))
                {
                    if (closedSet.Contains(neighbor))
                        continue;

                    int newMovementCostToNeighbor = currentNode.GCost + GetDistance(currentNode, neighbor);
                    if (newMovementCostToNeighbor < neighbor.GCost || !openSet.Contains(neighbor))
                    {
                        neighbor.GCost = newMovementCostToNeighbor;
                        neighbor.HCost = GetDistance(neighbor, endNode);
                        neighbor.Parent = currentNode;

                        if (!openSet.Contains(neighbor))
                            openSet.Add(neighbor);
                    }
                }
            }

            Console.WriteLine("No path found");
        }

        private void ResetNodes()
        {
            for (int x = 0; x < 32; x++)
            {
                for (int y = 0; y < 32; y++)
                {
                    bool walkable = consideredTiles[x, y].canWalkOver;
                    Vector2 position = consideredTiles[x, y].tilePosition;
                    nodeMap[x, y] = new Node(position, walkable, x, y);
                }
            }
        }

        private Node GetNodeFromTile(Tile tile)
        {
            int tileX = -1;
            int tileY = -1;

            for (int x = 0; x < 32; x++)
            {
                for (int y = 0; y < 32; y++)
                {
                    if (consideredTiles[x, y] == tile)
                    { 
                        tileX = x; tileY = y;
                    }
                }
            }

            if (tileX == -1 && tileY == -1)
            {
                return null;
            }

            Node node = nodeMap[tileX, tileY];

            return node;
        }
    }
}