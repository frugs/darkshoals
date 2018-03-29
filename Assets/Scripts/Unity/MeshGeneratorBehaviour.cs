using UnityEngine;

namespace Frugs.Darkshoals.Unity
{
    public class MeshGeneratorBehaviour : MonoBehaviour
    {
        public void GenerateMesh(float [,] heightMap)
        {
            var width = heightMap.GetLength(0);
            var height = heightMap.GetLength(1);
            
            var vertices = new Vector3[width * height];
            for (var i = 0; i < width; i++)
            {
                for (var j = 0; j < height; j++)
                {
                    vertices[i * width + j] = new Vector3(i, heightMap[i, j], j);
                }
            }

            var tris = new int[(width - 1) * (height - 1) * 2 * 3];
            for (var i = 0; i < width - 1; i++)
            {
                for (var j = 0; j < height - 1; j++)
                {
                    tris[3 * 2 * (j * (width - 1) + i)] = j * width + i;
                    tris[3 * 2 * (j * (width - 1) + i) + 1] = j * width + i + 1;
                    tris[3 * 2 * (j * (width - 1) + i) + 2] = (j + 1) * width + i;
                    
                    tris[3 * 2 * (j * (width - 1) + i) + 3] = (j + 1) * width + i + 1;
                    tris[3 * 2 * (j * (width - 1) + i) + 4] = (j + 1) * width + i;
                    tris[3 * 2 * (j * (width - 1) + i) + 5] = j * width + i + 1;
                }
            }
            
            var meshFilter = GetComponent<MeshFilter>();
            meshFilter.mesh.vertices = vertices;
            meshFilter.mesh.triangles = tris;
            meshFilter.mesh.RecalculateNormals();
            meshFilter.mesh.RecalculateTangents();
        }
    }
}