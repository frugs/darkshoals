using System.Collections.Generic;
using UnityEngine;

namespace Frugs.Darkshoals.Core.Util
{
    public static class CoordinateUtil
    {
        public static HashSet<Vector2> CoordinatesWithinRadius(Vector2 origin, float radius)
        {
            var result = new HashSet<Vector2>();
            
            for (var x = Mathf.CeilToInt(origin.x - radius); x < Mathf.CeilToInt(origin.x + radius); x++)
            {
                for (var y = Mathf.CeilToInt(origin.y - radius); y < Mathf.CeilToInt(origin.y + radius); y++)
                {
                    if ((origin.x - x) * (origin.x - x) + (origin.y - y) * (origin.y - y) <= radius)
                    {
                        result.Add(new Vector2(x, y));
                    }
                }    
            }

            return result;
        }
    }
}