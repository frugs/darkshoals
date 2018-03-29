using System;
using UnityEngine;

namespace Frugs.Darkshoals.Core.HeightMapGeneration.Util
{
    public static class Noise2D
    {
        private const float PerlinOffset = 1635.2983f;
        
        public static float Perlin(float x, float y)
        {
            return 2f * Mathf.PerlinNoise(PerlinOffset + x, PerlinOffset + y) - 1f;
        }
        
        public static float RidgedBillowedPerlin(float x, float y)
        {
            return 1f - Mathf.Abs(Perlin(x, y));
        }

        public static void Uniform(
            ref float[,] map, 
            float xOffset, 
            float yOffset, 
            float frequency, 
            float amplitude,
            Func<float, float, float> noiseFunc)
        {
            for (var x = 0; x < map.GetLength(0); x++)
            {
                for (var y = 0; y < map.GetLength(1); y++)
                {
                    var xInput = (x + xOffset) * frequency;
                    var yInput = (y + yOffset) * frequency;
                    var noiseOutput = noiseFunc(xInput, yInput);
                    
                    map[y, x] += noiseOutput * amplitude;
                }
            }
        }
        
        public static void Turbulence(
            ref float[,] map, 
            float xOffset, 
            float yOffset, 
            int octaves, 
            float persistence, 
            float baseFrequency,
            Func<float, float, float> noiseFunc)
        {
            var turbulence = new float[map.GetLength(0), map.GetLength(1)];
            
            var frequency = baseFrequency;
            var amplitude = 1f;
            var maxValue = 0f;
            
            for (var i = 0; i < octaves; i++)
            {                        
                Uniform(ref turbulence, xOffset, yOffset, frequency, amplitude, noiseFunc);

                frequency *= 2;
                maxValue += amplitude;
                amplitude *= persistence;
            }
            
            Transformation2D.Scale(ref turbulence, 1f / maxValue);
            Transformation2D.Sum(ref map, turbulence);
        }
    }
}