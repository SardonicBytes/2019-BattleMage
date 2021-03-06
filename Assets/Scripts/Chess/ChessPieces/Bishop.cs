﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : ChessPiece
{
    public int distance = 1;
    public override List<Vector2Int> GetLegalMoves(Board boardState)
    {
        List<Vector2Int> AllowedMoves = new List<Vector2Int>();
        AllowedMoves.AddRange(ChessLogic.GetDiagonals(boardState, index, distance));
        return AllowedMoves;
    }

}
