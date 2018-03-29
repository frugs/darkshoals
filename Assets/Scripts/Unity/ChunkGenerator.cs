using Frugs.Darkshoals.Core.HeightMapGeneration;
using Frugs.Darkshoals.Core.HeightMapGeneration.Util;
using UnityEngine;

namespace Frugs.Darkshoals.Unity
{
    public static class ChunkGenerator
    {
        public static GameObject GenerateChunk(int chunkX, int chunkY, int chunkSize)
        {
            var map = new float[chunkSize, chunkSize];
            
//            ContinentalShelf.GenerateShelf(ref map, chunkX, chunkY);
            Coast.GenerateCoast(ref map, chunkX, chunkY);
            Transformation2D.Translate(ref map, 1f);
            Transformation2D.Scale(ref map, 0.5f);

            var terrainData = new TerrainData();
            terrainData.SetHeights(0, 0, map);
            terrainData.size = new Vector3(chunkSize, 300f, chunkSize);
            var chunk = Terrain.CreateTerrainGameObject(terrainData);
            chunk.transform.position = new Vector3(chunkX * (chunkSize - 1), 0, chunkY * (chunkSize - 1));
            chunk.GetComponent<Terrain>().Flush();

            return chunk;
        }
    }
}    