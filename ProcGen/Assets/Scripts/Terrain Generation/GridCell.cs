using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell {

    GenerateGrid generateGrid;

    public int gridX;
    public int gridY;

    public int gCost;
    public int hCost;

    public GridCell parent;
    
    public Vector3 position;
    public float newY;
    public Vector3 lineTarget;
    public bool occupiedCell = false;
    public int resourcePodID;
    public int houseID;
    public float distanceToCharacter;

    public enum CellType
    { Water,
      Grass,
      RareZone,
      ElevatedLand,
      Resource,
      ResourceStartingPoint,
      RareResource,
      RareResourceStartingPoint,
      StartingCell,
	  IslandCell,
      Wood,
      Berry,
      Gem,
      Rock,
      HouseArea,
      VillagerSpawnLocation
    };
    public CellType myCell;

    public GridCell (Vector3 gridPosition, CellType gridType, bool occupied, int resourceID, int _gridX, int _gridY)
    {
        position = gridPosition;
        myCell = gridType;
        occupiedCell = occupied;
        resourcePodID = resourceID;
        gridX = _gridX;
        gridY = _gridY;
    }

    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }

}
