using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessPiece : MonoBehaviour
{

    public virtual List<Vector2> GetLegalMoves( Vector2 startLocation, Square[,] square) {
        List<Vector2> AllowedMoves = new List<Vector2>();
        return AllowedMoves;
    }

    private List<Vector2> GetAdjacent(Vector2 startLocation, Square[,] square) {
        List<Vector2> AllowedMoves = new List<Vector2>();
        return AllowedMoves;
    }

    private List<Vector2> TrimToBoard(Vector2 startLocation, Square[,] square) {
        List<Vector2> AllowedMoves = new List<Vector2>();
        return AllowedMoves;
    }

    private List<Vector2> GetDiagonals(Vector2 startLocation, Square[,] square)
    {
        List<Vector2> AllowedMoves = new List<Vector2>();
        return AllowedMoves;
    }


}
