using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : ChessPiece
{

    public override List<Vector2Int> GetLegalMoves(Board boardState)
    {
        List<Vector2Int> movesToCheck = new List<Vector2Int>();
        movesToCheck.Add(index + new Vector2Int(-1, 2));
        movesToCheck.Add(index + new Vector2Int(1, 2));
        movesToCheck.Add(index + new Vector2Int(2, 1));
        movesToCheck.Add(index + new Vector2Int(2, -1));
        movesToCheck.Add(index + new Vector2Int(1, -2));
        movesToCheck.Add(index + new Vector2Int(-1, -2));
        movesToCheck.Add(index + new Vector2Int(-2, -1));
        movesToCheck.Add(index + new Vector2Int(-2, 1));

        return ChessLogic.CheckSpots(boardState,index,movesToCheck);
    }

}
