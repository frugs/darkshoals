using Frugs.Darkshoals.Core.HeightMapGeneration.Util;

namespace Frugs.Darkshoals.Core.HeightMapGeneration
{
    public static class Coast
    {
        public static void GenerateCoast(ref float[,] map, int chunkX, int chunkY)
        {
            TerrainComponents.GenerateSmoothComponent(ref map, chunkX, chunkY);
            Transformation2D.Scale(ref map, 1 / 10f);
        }
    }
}