using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace MyCode
{
    public class Perlin2D : MonoBehaviour
    {
        [SerializeField] private int xSize;
        [SerializeField] private int ySize;
        [SerializeField] private float softness;
        
        [SerializeField] private GameObject tile;
        private List<GameObject> tiles;

        private float t = 0;

        private void Start()
        {
            for (int i = 0; i < xSize; i++)
            {
                t += softness;
                float x;

                x = noise.cnoise((float2) t);
                // x = SmoothNoise_1D(t);
                // x = Mathf.PerlinNoise(i, t);
                
                Instantiate(tile, new Vector3(i, (x * 100), 0), Quaternion.identity).transform.parent = transform;
            }
        }

        private float SmoothNoise_1D(float x)
        {
            return noise.cnoise((float2) x) / 2 + noise.cnoise((float2) x - 1) / 4 + noise.cnoise((float2) x + 1) / 4;
        }

        private float SmoothNoise_2D(float x, float y)
        {
            float corners = (noise.pnoise((float2) x - 1, y - 1) + noise.pnoise((float2) x + 1, y - 1) +
                             noise.pnoise((float2) x - 1, y + 1) + noise.pnoise((float2) x + 1, y + 1)) / 16;
            
            float sides = (noise.pnoise((float2) x - 1, y) + noise.pnoise((float2) x + 1, y) +
                           noise.pnoise((float2) x, y - 1) + noise.pnoise((float2) x, y + 1)) / 8;
            
            float center = noise.pnoise((float2) x, y) / 4;
            
            return corners + sides + center;
        }
    }
}