namespace Frugs.Darkshoals.Core.HeightMapGeneration.Util
{
    public static class Boundary
    {
        public static void CombineAtBounary(ref float[,] map, float[,] map2, int xOffset, int yOffset, int boundaryX)
        {
            var mask1 = new float[map.GetLength(0), map.GetLength(1)];
            var mask2 = new float[map.GetLength(0), map.GetLength(1)];

            var boundary = boundaryX - xOffset * map.GetLength(1);

            for (var i = 0; i < boundary; i++)
            {
                for (var j = 0; j < map.GetLength(1); j++)
                {
                    mask1[j, i] = 1f;
                    mask2[j, map.GetLength(0) - i] = 1f;
                }
            }
        }
    }
}