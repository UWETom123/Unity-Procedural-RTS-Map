using UnityEngine;
using System.Collections;

public class TreeSpawner : MonoBehaviour {

    public GameObject[] Trees;
    int treeIndex;

    public void spawnTree(float positionX, float positionY)
    {
        Trees = new GameObject[2];
        treeIndex = Random.Range(0, 2);



        //Instantiate(Trees[treeIndex], )
    }
}
