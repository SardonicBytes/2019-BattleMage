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
            if (boardState[startIndex.x, startIndex.y].spellEffect.MoveThisPiece(startIndex,endIndex))
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

    public void MoveSpellEffect(Vector2Int startIndex, Vector2Int endIndex)
    {
        SpellEffect spellToMove = boardState[startIndex.x, startIndex.y].spellEffect;
        boardState[endIndex.x, endIndex.y].spellEffect = spellToMove;
        boardState[startIndex.x, startIndex.y].spellEffect = null;
        spellToMove.UpdateIndex(this, endIndex);
    }

    public static Vector3 V2ToWorld ( Vector2Int inputV2 )
    {
        return new Vector3(inputV2.x ,inputV2.y,0);
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

    public void SwitchPlaces( Vector2Int pieceIndexA, Vector2Int pieceIndexB)
    {
        Square spotA = boardState[pieceIndexA.x, pieceIndexA.y];
        Square spotB = boardState[pieceIndexB.x, pieceIndexB.y];

        boardState[pieceIndexB.x, pieceIndexB.y] = spotA;
        boardState[pieceIndexA.x, pieceIndexA.y] = spotB;

        boardState[pieceIndexA.x, pieceIndexA.y].chessPiece.UpdateIndex(this, new Vector2Int(pieceIndexA.x, pieceIndexA.y));
        boardState[pieceIndexB.x, pieceIndexB.y].chessPiece.UpdateIndex(this, new Vector2Int(pieceIndexB.x, pieceIndexB.y));

        if (boardState[pieceIndexA.x, pieceIndexA.y].spellEffect != null)
            boardState[pieceIndexA.x, pieceIndexA.y].spellEffect.UpdateIndex(this, new Vector2Int(pieceIndexA.x, pieceIndexA.y));
        if (boardState[pieceIndexB.x, pieceIndexB.y].spellEffect != null)
            boardState[pieceIndexB.x, pieceIndexB.y].spellEffect.UpdateIndex(this, new Vector2Int(pieceIndexB.x, pieceIndexB.y));


        //Visuals
        boardState[pieceIndexA.x, pieceIndexA.y].chessPiece.transform.position =
            new Vector3 (pieceIndexA.x,pieceIndexA.y,0);
        boardState[pieceIndexA.x, pieceIndexA.y].spellEffect.transform.position =
            new Vector3(pieceIndexA.x, pieceIndexA.y, 0);

        boardState[pieceIndexB.x, pieceIndexB.y].chessPiece.transform.position =
            new Vector3(pieceIndexB.x, pieceIndexB.y, 0);
        boardState[pieceIndexB.x, pieceIndexB.y].spellEffect.transform.position =
            new Vector3(pieceIndexB.x, pieceIndexB.y, 0);



    }

    public void DestroyAllAt( Vector2Int index)
    {
        DestroySpell(index);
        DestroyPiece(index);
        DestroyObstacle(index);
    }

    public void DestroySpell( Vector2Int index )
    {
        if (boardState[index.x, index.y].spellEffect != null)
        {
            GameObject.Destroy(boardState[index.x, index.y].spellEffect.gameObject);
            boardState[index.x, index.y].spellEffect = null;
        }
    }
    public void DestroyPiece(Vector2Int index)
    {
        if (boardState[index.x, index.y].chessPiece != null)
        {
            GameObject.Destroy(boardState[index.x, index.y].chessPiece.gameObject);
            boardState[index.x, index.y].chessPiece = null;
        }
    }
    public void DestroyObstacle(Vector2Int index)
    {
        if (boardState[index.x, index.y].obstacle != null)
        {
            GameObject.Destroy(boardState[index.x, index.y].obstacle.gameObject);
            boardState[index.x, index.y].obstacle = null;
        }
    }

}
