using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace csproject2024.src
{
    internal class Level
    {
        private Dictionary<(int, int), Tile> tileMap;
        public List<Tile> tilesToDraw;
        private TileGenerator tileGenerator;
        private int renderDistance;
        public Tile lastSelectedTile;
        private Tile standingTile;

        public Level()
        {
            renderDistance = 24;
            tileMap = new Dictionary<(int, int), Tile>();
            tilesToDraw = new List<Tile>();
            tileGenerator = new TileGenerator();
        }

        public bool CheckForWalkableTile(Vector2 tilePos)
        {
            return tileMap.TryGetValue(((int)tilePos.X, (int)tilePos.Y), out Tile checkTile) && checkTile.canWalkOver;
        }

        public Tile GetTileAt(Vector2 tilePos)
        {
            tileMap.TryGetValue(((int)Math.Round(tilePos.X), (int)Math.Round(tilePos.Y)), out Tile tile);
            return tile;
        }


        public void Update(Player player, Camera camera)
        {
            lastSelectedTile?.MouseDeselect();

            Vector2 mouseScreenPosition = new Vector2(InputManager.MousePosition.X, InputManager.MousePosition.Y);
            Vector2 mouseWorldPosition = Vector2.Transform(mouseScreenPosition, Matrix.Invert(camera.Transform));

            Vector2 mapVector = mouseWorldPosition / 16;
            (int, int) map = ((int)Math.Floor(mapVector.X), (int)Math.Floor(mapVector.Y));

            if (tileMap.ContainsKey(map))
            {
                lastSelectedTile = tileMap[map];
                lastSelectedTile.MouseSelect();
            }

            tilesToDraw.Clear();

            for (int x = (int)player.tilePosition.X - renderDistance; x <= player.tilePosition.X + renderDistance; x++)
            {
                for (int y = (int)player.tilePosition.Y - renderDistance; y <= player.tilePosition.Y + renderDistance; y++)
                {
                    if (!tileMap.ContainsKey((x, y)))
                    {
                        tileMap[(x, y)] = tileGenerator.GenerateNewTile(new Vector2(x, y));
                    }

                    tilesToDraw.Add(tileMap[(x, y)]);
                }
            }

            standingTile = GetTileAt(player.spriteTilePosition);
            standingTile.stoodOn = true;
            player.standingTile = standingTile;

            Tile[,] AStarTiles = new Tile[32, 32];

            for (int x = 0; x < 32; x++)
            {
                for (int y = 0; y < 32; y++)
                {
                    if (tileMap.ContainsKey(((int)Math.Round(player.tilePosition.X - 15 + x), (int)Math.Round(player.tilePosition.Y - 15 + y))))
                    {
                        AStarTiles[x, y] = tileMap[((int)Math.Round(player.tilePosition.X - 15 + x), (int)Math.Round(player.tilePosition.Y - 15 + y))];
                    } 
                }
            }

            AStar.consideredTiles = AStarTiles;

            AStar aStar = new AStar();
            aStar.CalculatePath(GetTileAt(player.spriteTilePosition), GetTileAt(new(10,10)));

        }

        public void Draw(Player player)
        {
            foreach (Tile tile in tilesToDraw)
            {
                tile.Draw(player);
            }
        }
    }
}
