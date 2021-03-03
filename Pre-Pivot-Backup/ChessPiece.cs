using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessPiece : MonoBehaviour
{

    private static Vector2Int StepNorth;
    private static Vector2Int StepNorthEast;
    private static Vector2Int StepEast;
    private static Vector2Int StepSouthEast;
    private static Vector2Int StepSouth;
    private static Vector2Int StepSouthWest;
    private static Vector2Int StepWest;
    private static Vector2Int StepNorthWest;



    public virtual List<Vector2Int> GetLegalMoves( Vector2Int startLocation, Square[,] square) {
        List<Vector2Int> AllowedMoves = new List<Vector2Int>();
        return AllowedMoves;
    }


    private List<Vector2Int> GetAdjacent(Vector2Int startLocation, Square[,] square) {
        List<Vector2Int> AllowedMoves = new List<Vector2Int>();
        Vector2Int currentPosition = startLocation;
        //Check North
        while (true) {
            currentPosition += StepNorth;
            if () {
                AllowedMoves.Add(currentPosition);
            }
        }
        //Check East
        while (true)
        {

        }
        //Check South
        while (true)
        {

        }
        //Check West
        while (true)
        {

        }
        //Check location if it is empty.

        //If not empty, Check if location is an enemy

        //If we can move there, add it to the list, go one more down the line
        AllowedMoves.Add(currentPosition);

        return AllowedMoves;
    }


    private List<Vector2Int> TrimToBoard(Vector2Int startLocation, Square[,] square) {
        List<Vector2Int> AllowedMoves = new List<Vector2Int>();
        return AllowedMoves;
    }

    private List<Vector2Int> GetDiagonals(Vector2Int startLocation, Square[,] square)
    {
        List<Vector2Int> AllowedMoves = new List<Vector2Int>();
        return AllowedMoves;
    }


}
