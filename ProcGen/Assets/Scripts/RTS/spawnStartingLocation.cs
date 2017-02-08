using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnStartingLocation : MonoBehaviour {

    public GameObject startingBuilding;
    public Camera mainCamera;

    public const int buildingY = 12;
    public int buildingX;
    public int buildingZ;

    Vector3 buildingLocation;

	// Use this for initialization
	void Start () {

	}

	
	// Update is called once per frame
	void Update () {
		
	}

    public void generateStartingLocation()
    {
        buildingX = Random.Range(-40, 40);
        buildingZ = Random.Range(-40, 40);

        buildingLocation = new Vector3(buildingX, buildingY, buildingZ);

        Instantiate((Object)startingBuilding, buildingLocation, Quaternion.identity);

        mainCamera.transform.position = new Vector3(buildingX, buildingY + 25, buildingZ + 15);
    }
}
