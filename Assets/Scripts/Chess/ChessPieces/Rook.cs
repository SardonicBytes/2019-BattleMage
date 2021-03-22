using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : ChessPiece
{
    int distance = 16;
    public override List<Vector2Int> GetLegalMoves(Board boardState)
    {
        List<Vector2Int> AllowedMoves = new List<Vector2Int>();
        AllowedMoves.AddRange(ChessLogic.GetAdjacent(boardState, index, distance));
        return AllowedMoves;
    }

}
