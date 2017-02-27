using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour {

    public InputField userSeed;
    public Dropdown userSeason;
    public Scrollbar userResource;
    public Dropdown userStartingWorkers;
    public Dropdown userHouses;
    public GameObject inventoryCanvas;

    public TerrainData terrainData;
    public NoiseData noiseData;
    public MapGenerator mapGenerator;
    public TextureData textureData;
    public GenerateGrid generateGrid;

    public GameObject mainCamera;
    public GameObject mainMesh;
    public UnityStandardAssets.Utility.AutoMoveAndRotate rotateScript;

    public Texture2D snow;
    public Texture2D grass;
    public Texture2D autumnGrass;
    public Mesh mesh;
    public InstantiateModels instantiateModels;

    public Material terrainMaterial;

    public int seed;

    public float resourceDistributionMultiplier;
    public bool falloff;

    public int startingWorkers;
    public int houseAmount;

    public enum seasons
    {
        Spring, Summer, Autumn, Winter
    }

    public seasons mySeason;

    void Start()
    {
        textureData.layers[2].texture = grass;
        inventoryCanvas.GetComponent<Canvas>().enabled = false;
    }

    public void GenerateMap () {
        if (userSeed.text == "")
        {
            seed = Random.Range(1, 100000);
        }
        else
        {
            int.TryParse(userSeed.text, out seed);
        }
        mySeason = (seasons)userSeason.value;
        resourceDistributionMultiplier = userResource.value;
        int.TryParse(userStartingWorkers.captionText.text, out startingWorkers);
        int.TryParse(userHouses.captionText.text, out houseAmount);

        noiseData.seed = seed;
        terrainData.useFalloff = true;

        switch (mySeason)
        {
            case seasons.Spring:
                textureData.layers[2].tintStrength = 0.0f;
                textureData.layers[2].texture = grass;
                textureData.layers[2].textureScale = 2.0f;
                break;
            case seasons.Summer:
                textureData.layers[2].tintStrength = 0.19f;
                textureData.layers[2].texture = grass;
                textureData.layers[2].textureScale = 2.0f;
                break;

            case seasons.Autumn:
                textureData.layers[2].tintStrength = 0.21f;
                textureData.layers[2].texture = autumnGrass;
                textureData.layers[2].textureScale = 2.0f;
                break;

            case seasons.Winter:
                textureData.layers[2].tintStrength = 0.0f;
                textureData.layers[2].texture = snow;
                textureData.layers[2].textureScale = 2.0f;
                break;
        }

        textureData.ApplyToMaterial(terrainMaterial);

        mainCamera.GetComponent<UnityStandardAssets.ImageEffects.Blur>().enabled = false;

        rotateScript.rotateDegreesPerSecond.value.y = 0;

        mainMesh.transform.rotation = new Quaternion(0, 0, 0 ,0);

        mapGenerator.GenerateMap();

        

        generateGrid.Generate();

        

        gameObject.SetActive(false);
        inventoryCanvas.GetComponent<Canvas>().enabled = true;

        float resourceAmount = Mathf.Round(25 * resourceDistributionMultiplier);

        if (resourceAmount == 0)
        {
            resourceAmount = 0.1f;
        }

        GenerateGrid.podID = 0;

        for (int i = 0; i < resourceAmount; i++)
        {
            GenerateGrid.podID++;
            generateGrid.GetRandomSelection(2, 1, false);
        }

        resourceAmount = Mathf.Round(20 * resourceDistributionMultiplier);

        for (int i = 0; i < resourceAmount; i++)
        {
            GenerateGrid.podID++;
            generateGrid.GetRandomSelection(1, 1, true);
        }
        generateGrid.ChooseStartingLocation();
        generateGrid.GenerateHouseLocations(houseAmount);
        instantiateModels.InstantiateResources(GenerateGrid.mapGrid);
        generateGrid.spawnStartingWorkers(startingWorkers);
        
        //generateGrid.DrawGridLines();
        
        
    }
}
