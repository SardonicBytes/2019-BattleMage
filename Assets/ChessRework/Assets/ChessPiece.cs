using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Team { White, Black };

public class ChessPiece : MonoBehaviour
{
    public Team team;
    [HideInInspector]
    public Vector2Int index = Vector2Int.zero;


    //Virtual Only. Each peice will have an override
    public virtual List<Vector2Int> GetLegalMoves(Board boardState) {
        List<Vector2Int> AllowedMoves = new List<Vector2Int>();
        return AllowedMoves;
    }

    public virtual void UpdateIndex( Board board, Vector2Int newIndex)
    {
        board.boardState[newIndex.x, newIndex.y].chessPiece = this;
        index = newIndex;
    }

}
