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
        GemMiner,
        Villager
    }

    public CharacterType myType;

    public enum CharacterState
    {
        Walking,
        Gathering,
        Depositing,
    }

    public enum DesiredResource
    {
        Wood,
        Berries,
        Rocks,
        Gems
    }

    public GridCell.CellType myDesiredCellType;

    public DesiredResource myDesiredResource;

    public CharacterState myState = CharacterState.Walking;

    public GameObject myObject;

    public GridCell myCell;

    public GridCell startingCell;

    public List<GridCell> pathToResource;

    public List<GridCell> pathToHome;

    public GameObject myHouse;

    public Pathfinding pathfinder;
    public UserResources userResources;

    public int houseID;

    Vector3 offset;

    int i = 0;

    float delay = 3.0f;

    public float timer = 0.0f;

    public bool pathFound = false;

    bool setup;

    List<GridCell> currentPath;

    public Character(GameObject characterObject, GridCell characterCell, CharacterType characterType)
    {
        myCell = characterCell;
        myObject = characterObject;
        myType = characterType;

        offset = new Vector3(characterCell.position.x, characterCell.position.y + 0.85f, characterCell.position.z);

        //Instantiate((Object)characterObject, offset, Quaternion.identity);
    }

    void Start()
    {
        currentPath = pathToResource;
        pathfinder = GameObject.Find("RTSManager").GetComponent<Pathfinding>();
        userResources = GameObject.Find("RTSManager").GetComponent<UserResources>();
        setup = false;
    }

    void Update()
    {
        if (myType != CharacterType.Citizen)
        {
            delay = 3.0f;
            if (myType == CharacterType.GemMiner)
            {
                if (userResources.addedBoats == true || userResources.addedLadders == true)
                {
                    if (setup == false)
                    {
                        pathFound = false;
                        pathToResource = pathfinder.FindPath(myDesiredCellType, startingCell, true, this, userResources, true);
                        pathToHome = pathfinder.FindPath(myDesiredCellType, startingCell, false, this, userResources, true);
                        currentPath = pathToResource;
                        setup = true;
                    }                                                                    
                }
            }
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
                        if (myType != CharacterType.Villager)
                        {
                            pathFound = false;
                            while (pathFound == false)
                            {
                                if (myType != CharacterType.GemMiner)
                                {
                                    pathToHome = pathfinder.FindPath(myDesiredCellType, startingCell, false, this);
                                }
                                else
                                {
                                    pathToHome = pathfinder.FindPath(myDesiredCellType, startingCell, false, this, userResources, true);
                                }
                                
                            }
                            switch (myType)
                            {
                                case CharacterType.Lumberjack:
                                    myCell.resourceAmount -= 15;
                                    break;
                                case CharacterType.Gatherer:
                                    myCell.resourceAmount -= 15;
                                    break;
                                case CharacterType.Miner:
                                    myCell.resourceAmount -= 15;
                                    break;
                                case CharacterType.GemMiner:
                                    myCell.resourceAmount -= 15;
                                    break;
                            }
                            if (myCell.resourceAmount <= 0)
                            {
                                Destroy(myCell.cellObject);
                                if (myCell.myCell == GridCell.CellType.Gem)
                                {
                                    myCell.myCell = GridCell.CellType.ElevatedLand;
                                }
                                else
                                {
                                    myCell.myCell = GridCell.CellType.Grass;
                                }
                                
                            }
                        }
                        
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
                        if (myType != CharacterType.Villager)
                        {
                            pathFound = false;
                            while(pathFound == false)
                            {
                                if (myType != CharacterType.GemMiner)
                                {
                                    pathToResource = pathfinder.FindPath(myDesiredCellType, startingCell, true, this);
                                }
                                else
                                {
                                    pathToResource = pathfinder.FindPath(myDesiredCellType, startingCell, true, this, userResources, true);
                                }
                            }                         
                        }                 
                        myState = CharacterState.Walking;
                        currentPath = pathToResource;
                        i = 0;

                        if (transform.position == currentPath[i + 1].position && myState != CharacterState.Gathering && myState != CharacterState.Depositing)
                        {
                            i++;
                            myCell = currentPath[i];
                            timer = 0.0f;
                            if (myType == CharacterType.Villager)
                            {
                                switch (myDesiredResource)
                                {
                                    case DesiredResource.Berries:
                                        UserResources.berriesAmount -= 5;
                                        myHouse.GetComponent<HouseInventory>().berriesAmount += 5;
                                        break;
                                    case DesiredResource.Wood:
                                        UserResources.woodAmount -= 5;
                                        myHouse.GetComponent<HouseInventory>().woodAmount += 5;
                                        break;
                                    case DesiredResource.Rocks:
                                        UserResources.rocksAmount -= 5;
                                        myHouse.GetComponent<HouseInventory>().rocksAmount += 5;
                                        break;
                                    case DesiredResource.Gems:
                                        UserResources.gemsAmount -= 5;
                                        myHouse.GetComponent<HouseInventory>().gemsAmount += 5;
                                        break;

                                }
                            }
                            else
                            {
                                switch (myType)
                                {
                                    case CharacterType.Lumberjack:
                                        UserResources.woodAmount += 15;
                                        break;
                                    case CharacterType.Gatherer:
                                        UserResources.berriesAmount += 15;
                                        break;
                                    case CharacterType.Miner:
                                        UserResources.rocksAmount += 15;
                                        break;
                                    case CharacterType.GemMiner:
                                        UserResources.gemsAmount += 15;
                                        break;
                                }                         
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
}
