using System;
using System.Linq;

namespace Frugs.Darkshoals.Core.HeightMapGeneration.Util
{
    public static class DiamondSquare
    {
        public static float[,] CreateHeightMap(int size, float value)
        {
            return CreateHeightMap(size, value, value, value, value);
        }
        
        public static float[,] CreateHeightMap(int size, float tl, float tr, float bl, float br)
        {
            if (size % 2 != 1 || size < 3)
            {
                throw new InvalidOperationException();
            }

            var map = new float[size, size];

            for (var i = 0; i < size; i++)
            {
                for (var j = 0; j < size; j++)
                {
                    map[i, j] = float.NaN;
                }
            }
            
            map[0, 0] = tl;
            map[0, map.GetUpperBound(1)] = tr;
            map[map.GetUpperBound(0), 0] = bl;
            map[map.GetUpperBound(0), map.GetUpperBound(1)] = br;
            return map;
        }
        
        public static void Apply(ref float[,] map, float[,] prevMap, Func<float> randValueFunc)
        {
            var stepSize = map.GetLength(0) - 1;
            var scale = 1.0f;

            while (stepSize > 1)
            {
                Inner(ref map, prevMap, stepSize, scale, randValueFunc);

                stepSize /= 2;
                scale /= 2f;
            }
        }

        private static void Diamond(ref float[,] map, float[,] prevMap, int x, int y, int size, float value)
        {
            var hs = size / 2;

            // a     b 
            //
            //    x
            //
            // c     d
            var a = MapLookup(map, prevMap, x - hs, y - hs);
            var b = MapLookup(map, prevMap, x + hs, y - hs);
            var c = MapLookup(map, prevMap, x - hs, y + hs);
            var d = MapLookup(map, prevMap, x + hs, y + hs);
            var average = new[] {a, b, c, d}.Average() ?? 0f;

            map[x, y] = average + value;
        }

        private static void Square(ref float[,] map, float[,] prevMap, int x, int y, int size, float value)
        {
            var hs = size / 2;

            //   c
            //
            //a  x  b
            //
            //   d

            var a = MapLookup(map, prevMap, x - hs, y);
            var b = MapLookup(map, prevMap, x + hs, y);
            var c = MapLookup(map, prevMap, x, y - hs);
            var d = MapLookup(map, prevMap, x, y + hs);
            var average = new[] {a, b, c, d}.Average() ?? 0f;
            
            map[x, y] = average + value;
        }

        private static void Inner(
            ref float[,] map, float[,] prevMap, int stepSize, float scale, Func<float> randValueFunc)
        {
            var halfStep = stepSize / 2;

            var width = map.GetLength(0);
            var height = map.GetLength(1);
            
            for (var y = halfStep; y < height; y += stepSize)
            {
                for (var x = halfStep; x < width; x += stepSize)
                {
                    if (float.IsNaN(map[x, y]))
                    {
                        Diamond(ref map, prevMap, x, y, stepSize, randValueFunc() * scale);
                    }
                }
            }
            
            for (var y = 0; y < height + halfStep; y += stepSize)
            {
                for (var x = 0; x < width + halfStep + 1; x += stepSize)
                {
                    if (x + halfStep < width && y < height && float.IsNaN(map[x + halfStep, y]))
                    {
                        Square(ref map, prevMap, x + halfStep, y, stepSize, randValueFunc() * scale);                        
                    }

                    if (x < width && y + halfStep < height && float.IsNaN(map[x, y + halfStep]))
                    {
                        Square(ref map, prevMap, x, y + halfStep, stepSize, randValueFunc() * scale);
                    }
                }
            }
        }

        private static float? MapLookup(float[,] map, float[,] prevMap, int x, int y)
        {
            if (map.GetLowerBound(0) <= x && x <= map.GetUpperBound(0) && 
                map.GetLowerBound(1) <= y && y <= map.GetUpperBound(1))
            {
                return map[x, y];
            }

            if (prevMap != null &&
                map.GetLowerBound(0) - prevMap.GetUpperBound(0) < x && x < prevMap.GetUpperBound(0) && 
                prevMap.GetLowerBound(1) <= y && y <= prevMap.GetUpperBound(1))
            {
                return prevMap[prevMap.GetLength(0) + x, y];
            }

            return null;
        }
    }
}