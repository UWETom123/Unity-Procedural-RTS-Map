using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Pathfinding : MonoBehaviour {

    public GenerateGrid generateGrid;

    public bool pathFound;

    GridCell FindClosestCell(GridCell.CellType cellType, GridCell workerCell)
    {
        GridCell closestCell = GenerateGrid.mapGrid[0,0];

        List<float> distances = new List<float>();

        foreach (GridCell cell in GenerateGrid.mapGrid)
        {
            if (cell.myCell == cellType)
            {
                cell.distanceToCharacter = Vector3.Distance(workerCell.position, cell.position);
                distances.Add(cell.distanceToCharacter);                         
            }
        }

        foreach (GridCell cell in GenerateGrid.mapGrid)
        {
            if (cell.myCell == cellType)
            {
                if (cell.distanceToCharacter == distances.Min())
                {
                    closestCell = cell;
                    break;
                }
            }
        }
       
        return closestCell;
    }

    public List<GridCell> FindPath (GridCell.CellType cellType, GridCell workerCell, bool reverseList, Character character, UserResources userResources = null, bool gemMiner = false)
    {
        GridCell startingCell = workerCell;
        GridCell targetCell = FindClosestCell(cellType, workerCell);

        List<GridCell> openSet = new List<GridCell>();
        HashSet<GridCell> closedSet = new HashSet<GridCell>();
        openSet.Add(startingCell);

        while (openSet.Count > 0)
        {
            GridCell currentCell = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < currentCell.fCost || openSet[i].fCost == currentCell.fCost && openSet[i].hCost < currentCell.hCost)
                {
                    currentCell = openSet[i];
                }
            }
            openSet.Remove(currentCell);
            closedSet.Add(currentCell);

            if (currentCell == targetCell)
            {
                pathFound = true;
                character.pathFound = true;
                return RetracePath(startingCell, targetCell, reverseList);              
            }

            foreach (GridCell neighbour in generateGrid.GetNeighbours(currentCell))
            {
                if (gemMiner == true)
                {
                    if (userResources.addedLadders == true && userResources.addedBoats == false)
                    {
                        if (neighbour.myCell == GridCell.CellType.Water || closedSet.Contains(neighbour))
                        {
                            continue;
                        }
                    }
                    else if (userResources.addedBoats == true && userResources.addedLadders == false)
                    {
                        if (closedSet.Contains(neighbour))
                        {
                            continue;
                        }
                    }
                    else if (userResources.addedBoats == true && userResources.addedLadders == true)
                    {
                        if (closedSet.Contains(neighbour))
                        {
                            continue;
                        }
                    }
                    else if (userResources.addedBoats == false && userResources.addedLadders == false)
                    {
                        continue;
                    }
                }
                else
                {
                    if (neighbour.myCell == GridCell.CellType.RareZone || neighbour.myCell == GridCell.CellType.ElevatedLand || neighbour.myCell == GridCell.CellType.Water || closedSet.Contains(neighbour))
                    {
                        continue;
                    }
                }
                
                int newMovementCostToNeighbour = currentCell.gCost + GetDistance(currentCell, neighbour);
                if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newMovementCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, targetCell);
                    neighbour.parent = currentCell;

                    if (!openSet.Contains(neighbour))
                    {
                        openSet.Add(neighbour);
                    }
                }
            }
        }
        return null;
    }

    List<GridCell> RetracePath(GridCell startCell, GridCell endCell, bool reverseList)
    {
        List<GridCell> path = new List<GridCell>();
        GridCell currentCell = endCell;

        while (currentCell != startCell)
        {
            path.Add(currentCell);
            currentCell = currentCell.parent;
        }
        if (reverseList == true)
        {
            path.Reverse();
        }       
        
        return path;
    }

    int GetDistance(GridCell cellA, GridCell cellB)
    {
        int dstX = Mathf.Abs(cellA.gridX - cellB.gridX);
        int dstY = Mathf.Abs(cellA.gridY - cellB.gridY);

        if (dstX > dstY)
        {
            return 14 * dstY + 10 * (dstX - dstY);
        }
        return 14 * dstX + 10 * (dstY - dstX);
    }

    public void DrawPathLines(List<GridCell> path)
    {
        Vector3 drawTarget;
        foreach (GridCell cell in path)
        {
            drawTarget = new Vector3(cell.position.x, cell.position.y + 1, cell.position.z);
            Debug.DrawLine(cell.position, drawTarget, Color.red, 100000);
        }
    }

}
