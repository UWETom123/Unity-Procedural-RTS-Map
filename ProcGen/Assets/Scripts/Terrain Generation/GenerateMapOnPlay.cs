using UnityEngine;
using System.Collections;

public class GenerateMapOnPlay : MonoBehaviour {

    public MapGenerator mapGen;

	// Use this for initialization
	void Start () {
        mapGen.GenerateMap();	
	}
	
}
