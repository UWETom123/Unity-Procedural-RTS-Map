//Script used to hold information about each house and the resources that it holds

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HouseInventory : MonoBehaviour {

    public int houseID;

    public int woodAmount;
    public int rocksAmount;
    public int berriesAmount;
    public int gemsAmount;
    public int populationAmount;

    GenerateGrid generateGrid;

    float deathTimeAmount = 60.0f;
    float resourceConsumptionCooldown = 45.0f;
    float timer;
    float deathTimer;

    public Text woodDisplay;
    public Text berriesDisplay;
    public Text gemsDisplay;
    public Text rocksDisplay;
    public Text populationDisplay;

    // Use this for initialization
    void Start () {
        timer = resourceConsumptionCooldown;
        deathTimer = deathTimeAmount;
        generateGrid = GameObject.Find("MapGenerator").GetComponent<GenerateGrid>();
	}
	
	// Update is called once per frame
	void Update () {
        if (populationAmount > 0)
        {
            if (berriesAmount <= 0)
            {
                deathTimer -= Time.deltaTime;
            }
            else
            {
                deathTimer = deathTimeAmount;
            }

            if (deathTimer <= 0)
            {
                populationAmount--;
                UserResources.populationAmount--;
                foreach (GameObject villager in generateGrid.villagers)
                {
                    if (villager.GetComponent<Character>().houseID == houseID)
                    {
                        generateGrid.villagers.Remove(villager);
                        Destroy(villager);
                        deathTimer = deathTimeAmount;
                        break;
                    }
                }
            }

            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                woodAmount -= 5;
                rocksAmount -= 5;
                berriesAmount -= 5;
                gemsAmount -= 5;
                timer = resourceConsumptionCooldown;
            }
        }
        woodDisplay.text = woodAmount.ToString();
        berriesDisplay.text = berriesAmount.ToString();
        rocksDisplay.text = rocksAmount.ToString();
        gemsDisplay.text = gemsAmount.ToString();
        populationDisplay.text = populationAmount.ToString();
    }
}
