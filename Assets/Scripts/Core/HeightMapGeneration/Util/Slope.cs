namespace Frugs.Darkshoals.Core.HeightMapGeneration.Util
{
    public class Slope
    {
        public static void HorizontalSlope(ref float[,] map, float gradient, int chunkX)
        {
            for (var x = 0; x < map.GetLength(0); x++)
            {
                for (var y = 0; y < map.GetLength(1); y++)
                {
                    map[x, y] += (chunkX * map.GetLength(0) + x) * gradient;
                }
            }
        }
    }
}