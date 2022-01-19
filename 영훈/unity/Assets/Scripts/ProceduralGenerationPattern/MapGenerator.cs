using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eDRAW_MODE
{
    NoiseMap,
    ColorMap,
    Mesh,
}

public class MapGenerator : MonoBehaviour
{
    public eDRAW_MODE drawMode;

    [SerializeField] private MapDisplay mapDisplay;
    private readonly int mapChunkSize = 241;
    [Range(0, 6)]
    public int levelOfDetail;
    public float noiseScale;

    public int octaves;
    [Range(0f, 1f)]
    public float persistance;
    public float lacunarity;

    public int seed;
    public Vector2 offset;

    public float meshHeightMultiplier;
    public AnimationCurve meshHeightCurve;

    public bool isAutoUpdate;

    public TerrainType[] regions;

    public void GenerateMap()
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(mapChunkSize, mapChunkSize, seed, noiseScale, octaves, persistance, lacunarity, offset);

        Color[] colorMap = new Color[mapChunkSize * mapChunkSize];

        for (int y = 0; y < mapChunkSize; y++)
        {
            for (int x = 0; x < mapChunkSize; x++)
            {
                float currentHeight = noiseMap[x, y];
                for (int i = 0; i < regions.Length; i++)
                {
                    if (currentHeight <= regions[i].height)
                    {
                        colorMap[y * mapChunkSize + x] = regions[i].color;
                        break;
                    }
                }
            }
        }

        switch (drawMode)
        {
            case eDRAW_MODE.NoiseMap:
                mapDisplay.DrawTexture(TextureGenerator.TextureFromHeightMap(noiseMap));
                break;
            case eDRAW_MODE.ColorMap:
                mapDisplay.DrawTexture(TextureGenerator.TextureFromColorMap(colorMap, mapChunkSize, mapChunkSize));
                break;
            case eDRAW_MODE.Mesh:
                mapDisplay.DrawMesh(MeshGenerator.GenerateTerrainMesh(noiseMap, meshHeightMultiplier, meshHeightCurve, levelOfDetail), TextureGenerator.TextureFromColorMap(colorMap, mapChunkSize, mapChunkSize));
                break;
        }
    }

    private void OnValidate()
    {
        if (lacunarity < 1)
            lacunarity = 1;
        if (octaves < 0)
            octaves = 0;
    }
}

[System.Serializable]
public struct TerrainType
{
    public string name;
    public float height;
    public Color color;
}