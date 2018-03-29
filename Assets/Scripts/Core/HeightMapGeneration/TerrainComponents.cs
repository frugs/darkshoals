using Frugs.Darkshoals.Core.HeightMapGeneration.Util;

namespace Frugs.Darkshoals.Core.HeightMapGeneration
{
    public static class TerrainComponents
    {
        public static void GenerateCoarseComponent(ref float[,] map, int chunkX, int chunkY)
        {
            var height = map.GetLength(0);
            var width = map.GetLength(1);
            
            Noise2D.Turbulence(
                ref map,
                chunkX * (height - 1),
                chunkY * (width - 1), 
                3, 
                0.5f,
                0.01f,
                Noise2D.Perlin);
            
            var ridges = new float[height, width];
            Noise2D.Turbulence(
                ref ridges,
                chunkX * (height - 1),
                chunkY * (width - 1), 
                8, 
                0.5f,
                0.12f,
                Noise2D.RidgedBillowedPerlin);
            Transformation2D.Combine(ref map, 50f, ridges, 5f);
        }

        public static void GenerateSmoothComponent(ref float[,] map, int chunkX, int chunkY)
        {
            var height = map.GetLength(0);
            var width = map.GetLength(1);
            
            Noise2D.Turbulence(
                ref map,
                chunkX * (height - 1),
                chunkY * (width - 1), 
                3, 
                0.5f,
                0.005f,
                Noise2D.Perlin);
            
            var ridges = new float[height, width];
            Noise2D.Turbulence(
                ref ridges,
                chunkX * (height - 1),
                chunkY * (width - 1), 
                8, 
                0.5f,
                0.05f,
                Noise2D.RidgedBillowedPerlin);
            Transformation2D.Combine(ref map, 30f, ridges, 1.2f);
        }
    }
}