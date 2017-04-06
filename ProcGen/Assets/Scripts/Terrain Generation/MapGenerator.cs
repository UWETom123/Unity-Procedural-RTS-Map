//The script responsible for initating the generation of the terrain

using UnityEngine;
using System.Collections;

public class MapGenerator : MonoBehaviour {
    
    public enum DrawMode {NoiseMap, Mesh, FalloffMap};
    public DrawMode drawMode;

    public TerrainData terrainData;
    public NoiseData noiseData;
    public TextureData textureData;

    public Material terrainMaterial;

    const int mapChunkSize = 241;
    [Range(0,6)]
    public int levelOfDetail;
    

    public bool autoUpdate;


    float[,] falloffMap;
    float[,] islandFalloffMap;

    void OnTextureValuesUpdated()
    {
        textureData.ApplyToMaterial(terrainMaterial);
    }

    void Awake()
    {
        //falloffMap = FalloffGenerator.GenerateFalloffMap(mapChunkSize);
        //islandFalloffMap = FalloffGenerator.GenerateFalloffMap(mapChunkSize);
    }
    
    public void GenerateMap()
    {
        textureData.ApplyToMaterial(terrainMaterial);
        falloffMap = FalloffGenerator.GenerateFalloffMap(mapChunkSize);
        islandFalloffMap = FalloffGenerator.GenerateIslandFalloffMap(mapChunkSize);

        float[,] noiseMap = Noise.GenerateNoiseMap(mapChunkSize, mapChunkSize, noiseData.seed, noiseData.noiseScale, noiseData.octaves, noiseData.persistance, noiseData.lacunarity, noiseData.offset);
        float[,] islandMap = Noise.GenerateNoiseMap(mapChunkSize, mapChunkSize, noiseData.seed, noiseData.noiseScale, noiseData.octaves, noiseData.persistance, noiseData.lacunarity, noiseData.offset);
        Color[] colourMap = new Color[mapChunkSize * mapChunkSize];
        Texture[] textureMap = new Texture[mapChunkSize * mapChunkSize];
        for (int y = 0; y < mapChunkSize; y++)
        {
            for (int x = 0; x < mapChunkSize; x++)
            {
                if (terrainData.useFalloff)
                {
                    noiseMap[x, y] = Mathf.Clamp01(noiseMap[x, y] - falloffMap[x, y]);
                    islandMap[x, y] = Mathf.Clamp01(islandMap[x, y] *+ falloffMap[x, y]);
                }
                
            }
        }

        MapDisplay display = FindObjectOfType<MapDisplay>();
        if (drawMode == DrawMode.NoiseMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromHeightMap(noiseMap));
        }
        else if (drawMode == DrawMode.Mesh)
        {
            display.DrawMesh(MeshGenerator.GenerateTerrainMesh(noiseMap, terrainData.meshHeightMultiplier, terrainData.meshHeightCurve, levelOfDetail));
            display.DrawIsland(MeshGenerator.GenerateTerrainMesh(islandMap, terrainData.meshHeightMultiplier, terrainData.islandHeightCurve, levelOfDetail));
        }
        else if (drawMode == DrawMode.FalloffMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromHeightMap(FalloffGenerator.GenerateFalloffMap(mapChunkSize)));
        }

        textureData.UpdateMeshHeights(terrainMaterial, terrainData.minHeight, terrainData.maxHeight);
    }

    void OnValidate()
    {
        if (textureData != null)
        {
            textureData.OnValuesUpdated -= OnTextureValuesUpdated;
            textureData.OnValuesUpdated += OnTextureValuesUpdated;
        }
        falloffMap = FalloffGenerator.GenerateFalloffMap(mapChunkSize);
    }
}