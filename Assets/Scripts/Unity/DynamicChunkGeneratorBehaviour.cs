using System.Collections.Generic;
using System.Linq;
using Frugs.Darkshoals.Core.Util;
using UnityEngine;

namespace Frugs.Darkshoals.Unity
{
    public class DynamicChunkGeneratorBehaviour : MonoBehaviour
    {
        private const int ChunkSize = 32;
        
        public Transform Source;

        private readonly HashSet<Vector2> _generatedChunkCoordinates = new HashSet<Vector2>();
        private readonly Dictionary<Vector2, GameObject> _generatedChunks = new Dictionary<Vector2, GameObject>();

        public void Update()
        {
            var chunksWithinRadius = 
                CoordinateUtil.CoordinatesWithinRadius(
                    new Vector2(Source.position.x / ChunkSize, Source.position.z / ChunkSize), 
                    128);
            
            var chunksToGenerate = new HashSet<Vector2>(chunksWithinRadius);
            chunksToGenerate.ExceptWith(_generatedChunkCoordinates);
            _generatedChunkCoordinates.UnionWith(chunksToGenerate);
            
            foreach (var chunkCoord in chunksToGenerate)
            {
                _generatedChunks[chunkCoord] = ChunkGenerator.GenerateChunk(
                    Mathf.FloorToInt(chunkCoord.x),
                    Mathf.FloorToInt(chunkCoord.y),
                    ChunkSize);
            }
            
            foreach (var chunkCoordinate in _generatedChunkCoordinates.Except(chunksWithinRadius))
            {
                Destroy(_generatedChunks[chunkCoordinate]);
                _generatedChunks.Remove(chunkCoordinate);
            }
            
            _generatedChunkCoordinates.IntersectWith(chunksWithinRadius);
        }
    }
}