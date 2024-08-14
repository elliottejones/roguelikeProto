using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using DotnetNoise;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using SharpDX.MediaFoundation;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace csproject2024.src
{
    internal class TileGenerator
    {
        Texture2D grassOutline;
        Texture2D sandOutline;

        private Texture grass;
        private Texture water;
        private Texture sand;
        private Texture tree1;
        private Texture tree2;

        private FastNoise noise;
        private FastNoise riverNoise;
        private Random random = new Random();

        public TileGenerator()
        {
            noise = new(random.Next(0, 2048));
            noise.UsedNoiseType = FastNoise.NoiseType.Perlin;
            noise.Frequency = 0.01f;
            noise.Octaves = 3;
            noise.Lacunarity = 2;
            noise.Gain = 0.7f;

            // Separate noise for rivers
            riverNoise = new(random.Next(2048, 4096));
            riverNoise.UsedNoiseType = FastNoise.NoiseType.SimplexFractal;
            riverNoise.Frequency = 0.01f;
            riverNoise.Octaves = 2;
            riverNoise.Lacunarity = 2;
            riverNoise.Gain = 0.5f;

            grassOutline = Globals.Content.Load<Texture2D>("grassoutline");
            sandOutline = Globals.Content.Load<Texture2D>("sandoutline");
            grass = new(Globals.Content.Load<Texture2D>("grass"), Vector2.Zero, "grass");

            water = new(Globals.Content.Load<Texture2D>("water"), Vector2.Zero, "water");

            sand = new(Globals.Content.Load<Texture2D>("beach"), Vector2.Zero, "sand");

            tree1 = new(Globals.Content.Load<Texture2D>("tree"), new Vector2(16, 77), "tree1");
            tree2 = new(Globals.Content.Load<Texture2D>("tree2"), new Vector2(16, 65), "tree2");
        }

        public Tile GenerateNewTile(Vector2 tileCoordinates)
        {
            float offsetX = tileCoordinates.X;
            float offsetY = tileCoordinates.Y;

            float datapoint = noise.GetNoise(offsetX, offsetY);
            float riverDatapoint = riverNoise.GetNoise(offsetX, offsetY);

            Vector2 screenCoordinates = tileCoordinates * 16;

            Texture texture;
            Texture2D outline;

            bool walkableTile = true;
            bool slowTile = false;
            Foliage foliage = null;
            string tileType = "";

            // Define thresholds for river generation
            float riverThreshold = 0.5f; // Adjust this value to control river density

            if (riverDatapoint > riverThreshold)
            {
                texture = water;
                slowTile = true;
                outline = null;
                tileType = "river";
            }
            else if (datapoint <= 0)
            {
                texture = water;
                slowTile = true;
                outline = null;
                tileType = "water";
            }
            else if (datapoint > 0 && datapoint <= 0.06f)
            {
                texture = sand;
                outline = sandOutline;
                tileType = "sand";
            }
            else
            {
                int randomFoliageIndex = random.Next(0, 100);
                if (randomFoliageIndex == 69)
                {
                    foliage = new Foliage(tree1, tileCoordinates, true);
                    walkableTile = false;
                }
                if (randomFoliageIndex == 16)
                {
                    foliage = new Foliage(tree2, tileCoordinates, true);
                    walkableTile = false;
                }

                texture = grass;
                outline = grassOutline;
                tileType = "grass";
            }

            Tile generatedTile = new Tile(texture, screenCoordinates, walkableTile, tileCoordinates, slowTile, foliage, outline, tileType);

            return generatedTile;
        }
    }

}
