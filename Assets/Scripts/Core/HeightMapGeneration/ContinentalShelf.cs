using Frugs.Darkshoals.Core.HeightMapGeneration.Util;

namespace Frugs.Darkshoals.Core.HeightMapGeneration
{
    public static class ContinentalShelf
    {
        public static void GenerateShelf(ref float[,] map, int chunkX, int chunkY)
        {
            var height = map.GetLength(0);
            var width = map.GetLength(1);

            TerrainComponents.GenerateSmoothComponent(ref map, chunkX, chunkY);

            var coarseShelf = new float[height, width];
            TerrainComponents.GenerateCoarseComponent(ref coarseShelf, chunkX, chunkY);
                    
            var coarseMap = new float[height, width];
            Noise2D.Uniform(
                ref coarseMap,
                chunkX * (height - 1),
                chunkY * (width - 1),
                0.005f,
                1f,
                Noise2D.Perlin);
            Transformation2D.Exponent(ref coarseMap, 1.1f);
            Transformation2D.ClampLower(ref coarseMap, 0f);
            Transformation2D.Mutlipy(ref coarseShelf, coarseMap);
            
            Transformation2D.Combine(ref map, 1f, coarseShelf, 5f);
        }
    }
}