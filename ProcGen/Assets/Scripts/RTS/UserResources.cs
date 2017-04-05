using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserResources : MonoBehaviour {

    public Text woodDisplay;
    public Text berriesDisplay;
    public Text gemsDisplay;
    public Text rocksDisplay;
    public Text populationDisplay;
    public Image padlock;

    public Button addLadders;
    public Button addBoats;

    public bool addedLadders = false;
    public bool addedBoats = false;

    static public int woodAmount = 200;
    static public int berriesAmount = 200;
    static public int gemsAmount = 200;
    static public int rocksAmount = 200;
    static public int populationAmount;


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

    public void activateLadders()
    {
        addedLadders = true;
        woodAmount -= 100;
        rocksAmount -= 100;
        addLadders.interactable = false;
        addBoats.interactable = true;
        padlock.gameObject.SetActive(false);
    }
    public void activateBoats()
    {
        addedBoats = true;
        woodAmount -= 100;
        rocksAmount -= 100;
        addBoats.interactable = false;
    }
}