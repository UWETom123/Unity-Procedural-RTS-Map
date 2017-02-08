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

    static public int woodAmount;
    static public int berriesAmount;
    static public int gemsAmount;
    static public int rocksAmount;
    static public int populationAmount;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        woodDisplay.text = woodAmount.ToString();
        berriesDisplay.text = berriesAmount.ToString();
        rocksDisplay.text = rocksAmount.ToString();
        populationDisplay.text = populationAmount.ToString();
    }
}
