using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    public enum CharacterType
    {
        Lumberjack,
        Citizen,
        Miner,
        Gatherer,
    }

    public CharacterType myType;

    public enum CharacterState
    {
        Walking,
        Gathering,
        Depositing,
    }

    public CharacterState myState = CharacterState.Walking;

    public GameObject myObject;

    public GridCell myCell;

    public List<GridCell> pathToResource;

    public List<GridCell> pathToHome;

    Vector3 offset;

    int i = 0;

    float delay = 3.0f;

    public float timer = 0.0f;

    public bool pathFound = false;

    List<GridCell> currentPath;

    public Character(GameObject characterObject, GridCell characterCell, CharacterType characterType)
    {
        myCell = characterCell;
        myObject = characterObject;
        myType = characterType;

        offset = new Vector3(characterCell.position.x, characterCell.position.y + 0.85f, characterCell.position.z);

        Instantiate((Object)characterObject, offset, Quaternion.identity);
    }

    void Start()
    {
        currentPath = pathToResource;
    }

    void Update()
    {
        delay = 3.0f;
        if (pathFound == true)
        {            
            if (myState == CharacterState.Gathering)
            {            
                if (timer < delay)
                {
                    timer += Time.deltaTime;                    
                }
                else if (timer >= delay)
                {

                    myState = CharacterState.Walking;
                    currentPath = pathToHome;
                    i = 0;
                    
                    if (transform.position == currentPath[i + 1].position && myState != CharacterState.Gathering && myState != CharacterState.Depositing)
                    {
                        i++;
                        myCell = currentPath[i];
                        timer = 0.0f;
                    }

                    transform.position = Vector3.MoveTowards(transform.position, currentPath[i + 1].position, 5.0f * Time.deltaTime);

                }
            }

            if (myState == CharacterState.Depositing)
            {
                if (timer < delay)
                {
                    timer += Time.deltaTime;
                }
                else if (timer >= delay)
                {

                    myState = CharacterState.Walking;
                    currentPath = pathToResource;
                    i = 0;
                    
                    if (transform.position == currentPath[i + 1].position && myState != CharacterState.Gathering && myState != CharacterState.Depositing)
                    {
                        i++;
                        myCell = currentPath[i];
                        timer = 0.0f;
                        switch(myType)
                        {
                            case CharacterType.Lumberjack:
                                UserResources.woodAmount += 10;
                                break;
                            case CharacterType.Gatherer:
                                UserResources.berriesAmount += 10;
                                break;
                            case CharacterType.Miner:
                                UserResources.rocksAmount += 10;
                                break;
                        }
                        
                    }

                    transform.position = Vector3.MoveTowards(transform.position, currentPath[i + 1].position, 5.0f * Time.deltaTime);


                }
            }

            if (myState == CharacterState.Walking)
            {
                
                if (myCell == pathToResource[pathToResource.Count - 1])
                {
                    myState = CharacterState.Gathering;
                }
                else if (myCell == pathToHome[pathToHome.Count - 1])
                {
                    myState = CharacterState.Depositing;                   
                }
                else if (transform.position == currentPath[i + 1].position && myState != CharacterState.Gathering && myState != CharacterState.Depositing)
                {
                    i++;
                    myCell = currentPath[i];
                }
                else if (transform.position != currentPath[i + 1].position)
                {
                    transform.position = Vector3.MoveTowards(transform.position, currentPath[i + 1].position, 5.0f * Time.deltaTime);
                }
            }
        }
    }

}
