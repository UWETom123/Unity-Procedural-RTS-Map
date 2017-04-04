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

    public Text woodDisplay;
    public Text berriesDisplay;
    public Text gemsDisplay;
    public Text rocksDisplay;
    public Text populationDisplay;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        woodDisplay.text = woodAmount.ToString();
        berriesDisplay.text = berriesAmount.ToString();
        rocksDisplay.text = rocksAmount.ToString();
        gemsDisplay.text = gemsAmount.ToString();
        populationDisplay.text = populationAmount.ToString();
    }
}
