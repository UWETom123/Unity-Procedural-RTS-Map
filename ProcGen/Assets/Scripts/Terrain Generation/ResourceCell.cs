//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class ResourceCell {

//    public Vector3 cellPosition;
//    public float newY;
//    public Vector3 lineTarget;
//    bool cellGenerated = false;
//    int possibleCellIndex;

//    public ResourceCell(Vector3 position)
//    {

//        if (GenerateGrid.currentlySelectedCells < GenerateGrid.spreadAmount)
//        {
//            possibleCellIndex = Random.Range(0, 7);

//            cellPosition = GenerateGrid.FindPossibleCells(position, position.y)[possibleCellIndex];

//            if (GenerateGrid.resourceCells.Contains(cellPosition))
//            {
//                possibleCellIndex = Random.Range(0, 7);
//                cellPosition = GenerateGrid.FindPossibleCells(position, position.y)[possibleCellIndex];
//            }

//            Debug.Log(cellPosition);

//            GenerateGrid.currentlySelectedCells++;
//            GenerateGrid.resourceCells.Add(cellPosition);
//            ResourceCell rc = new ResourceCell(cellPosition);

//            Debug.Log(rc.cellPosition);
//            lineTarget = new Vector3(cellPosition.x, cellPosition.y + 3, cellPosition.z);
//            Debug.DrawLine(cellPosition, lineTarget, Color.yellow, 1000000);
//            if (GenerateGrid.currentlySelectedCells >= GenerateGrid.spreadAmount)
//            {
//                GenerateGrid.currentlySelectedCells = 0;
//                cellGenerated = true;
//            }

//        }

//    }

//}
