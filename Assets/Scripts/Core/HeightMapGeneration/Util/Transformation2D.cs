using UnityEngine;

namespace Frugs.Darkshoals.Core.HeightMapGeneration.Util
{
    public static class Transformation2D
    {
        public static void Sum(ref float[,] map, float[,] addition)
        {
            for (var x = 0; x < map.GetLength(0); x++)
            {
                for (var y = 0; y < map.GetLength(1); y++)
                {
                    map[x, y] += addition[x, y];
                }
            }
        }
        
        public static void Translate(ref float[,] map, float amount)
        {
            for (var x = 0; x < map.GetLength(0); x++)
            {
                for (var y = 0; y < map.GetLength(1); y++)
                {
                    map[x, y] += amount;
                }
            }
        }
        
        public static void Mutlipy(ref float[,] map, float[,] multiple)
        {
            for (var x = 0; x < map.GetLength(0); x++)
            {
                for (var y = 0; y < map.GetLength(1); y++)
                {
                    map[x, y] *= multiple[x, y];
                }
            }
        }
        
        public static void ClampLower(ref float[,] map, float threshold)
        {
            for (var x = 0; x < map.GetLength(0); x++)
            {
                for (var y = 0; y < map.GetLength(1); y++)
                {
                    map[x, y] = Mathf.Max(map[x, y], threshold);
                }
            }
        }
        
        public static void Scale(ref float[,] map, float magnitude)
        {
            for (var x = 0; x < map.GetLength(0); x++)
            {
                for (var y = 0; y < map.GetLength(1); y++)
                {
                    map[x, y] *= magnitude;
                }
            }
        }

        public static void Exponent(ref float[,] map, float p)
        {
            for (var x = 0; x < map.GetLength(0); x++)
            {
                for (var y = 0; y < map.GetLength(1); y++)
                {
                    map[x, y] = Mathf.Pow(map[x, y], p);
                }
            }
        }

        public static void Abs(ref float[,] map)
        {
            for (var x = 0; x < map.GetLength(0); x++)
            {
                for (var y = 0; y < map.GetLength(1); y++)
                {
                    map[x, y] = Mathf.Abs(map[x, y]);
                }
            }
        }

        public static void Combine(
            ref float[,] map, float mapRelativeScale, float[,] combinator, float combinatorRelativeScale)
        {
            Scale(ref map, mapRelativeScale);
            Scale(ref combinator, combinatorRelativeScale);
            Sum(ref map, combinator);
            Scale(ref map, 1f / (mapRelativeScale + combinatorRelativeScale));
        }
    }
}