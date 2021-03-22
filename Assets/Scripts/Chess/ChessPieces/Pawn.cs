using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pawn : ChessPiece
{
    private int distance = 2;
    bool initialized = false;
    bool hasMoved = false;

    public override List<Vector2Int> GetLegalMoves(Board boardState)
    {
        List<Vector2Int> AllowedMoves = new List<Vector2Int>();

        //Basic Moves
        List<Vector2Int> moveSpots = new List<Vector2Int>();
        moveSpots.AddRange(ChessLogic.GetAdjacent(boardState, index, distance));
        //Debug.Log(moveSpots.Count + " Potential Adjacent Moves Found");
        for (int i = 0; i < moveSpots.Count; i++)
        {
            //If it would be a capture, remove it. Pawns Can't capture adjacent
            if (boardState.boardState[moveSpots[i].x, moveSpots[i].y].chessPiece == null)
            {
                AllowedMoves.Add(moveSpots[i]);
            }
        }
        //Debug.Log(moveSpots.Count + " Legal Adjacent Moves Found");

        //Attacks on Diagonals
        List<Vector2Int> attackSpots = new List<Vector2Int>();
        attackSpots.AddRange(ChessLogic.GetDiagonals(boardState, index, 1));


        for (int i = 0; i < attackSpots.Count; i++)
        {

            //If it would be a move, not a capture, remove it. Pawns can't move diagonal
            if (boardState.boardState[attackSpots[i].x, attackSpots[i].y].chessPiece != null)
            {
                AllowedMoves.Add(attackSpots[i]);
            }
        }

        //En Passant?
        // Should This be added?
        //

        return AllowedMoves;
    }

    public override void UpdateIndex(Board board, Vector2Int newIndex)
    {
        base.UpdateIndex(board,newIndex);
        if (!initialized)
        {
            initialized = true;
        }
        else if (!hasMoved)
        {
            hasMoved = true;
            distance = 1;
        }
    }



}
