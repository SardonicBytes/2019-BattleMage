using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board
{

    public Square[,] boardState;

    public bool IsInBounds( Vector2Int index )
    {
        if (index.x < 0 || index.y < 0)
            return false;
        else if (index.x >= boardState.GetLength(0) || index.y >= boardState.GetLength(1) )
            return false;
        else
            return true;
    }

    public void MovePiece( Vector2Int startIndex, Vector2Int endIndex)
    {

        if (boardState[startIndex.x, startIndex.y].spellEffect != null)
        {   
            if (boardState[startIndex.x, startIndex.y].spellEffect.MoveThisPiece(startIndex))
            {
                return;
            }
        }

        ChessPiece pieceToMove = boardState[startIndex.x, startIndex.y].chessPiece;
        boardState[endIndex.x, endIndex.y].chessPiece = pieceToMove;
        boardState[startIndex.x, startIndex.y].chessPiece = null;
        pieceToMove.UpdateIndex(this,endIndex);

        //QuickTest
        pieceToMove.transform.position = new Vector3(endIndex.x,endIndex.y,0);

    }

    public void Capture(Vector2Int startIndex, Vector2Int endIndex)
    {
        //Do Some Additional Stuff
        GameObject pieceToCapture = boardState[endIndex.x, endIndex.y].chessPiece.gameObject;
        GameObject.Destroy(pieceToCapture);
        MovePiece(startIndex, endIndex);

    }

    public Board(int size)
    {
        boardState = new Square[size, size];
        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                boardState[x, y] = new Square(true);
            }
        }
    }
}
