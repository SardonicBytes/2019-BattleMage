using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardVisuals : MonoBehaviour
{
    ChessPiece SelectedPiece;
    public GameObject baseVisualSquare;
    SpriteRenderer[,] visualSquares;

    public Color ColourAvailableMove;
    public Color ColourCapture;

    public Board board;

    public int boardSize;

    private void Awake()
    {
        board = new Board(boardSize);

        visualSquares = new SpriteRenderer[8,8];
        for (int x = 0; x < boardSize; x++)
        {
            for (int y = 0; y < boardSize; y++)
            {
                GameObject newSquare = Instantiate(baseVisualSquare, new Vector3(x, y, -1), Quaternion.identity);
                visualSquares[x, y] = newSquare.GetComponent<SpriteRenderer>();
                newSquare.GetComponent<ChessSquare>().boardVisuals = this;
            }
        }
    }

    public void SelectPiece(ChessPiece chessPiece) {
        ResetHighlight();
        if (SelectedPiece == chessPiece)
        {
            chessPiece = null;

            return;
        }
        SelectedPiece = chessPiece;
        Debug.Log("Picked up a piece");

        //Visualize Available Moves

        List<Vector2Int> legalMoves = chessPiece.GetLegalMoves(board);
        for (int i = 0; i < legalMoves.Count; i++)
        {
            visualSquares[legalMoves[i].x, legalMoves[i].y].color = ColourAvailableMove;
            if (board.boardState[legalMoves[i].x, legalMoves[i].y].chessPiece)
            {
                if (board.boardState[legalMoves[i].x, legalMoves[i].y].chessPiece.team != SelectedPiece.team)
                {
                    visualSquares[legalMoves[i].x, legalMoves[i].y].color = ColourCapture;
                }
            }
        }

    }


    //Bit of a mess. Could this be cleaned up?
    public void SelectSquare( Vector2Int index )
    {
        //We're not holding a piece
        if (SelectedPiece == null)
        {
            //If its a Piece
            if (board.boardState[index.x, index.y].chessPiece != null)
            {
                //Debug.Log("Select A Peice");
                SelectPiece(board.boardState[index.x, index.y].chessPiece);

            }
            //Its not peice
            else
            {
                //Debug.Log("Do Nothing");
                ResetHighlight();
            }
        }
        //We're Holding a Peice
        else
        {
            //If the Move is a Legal Move
            if (SelectedPiece.GetLegalMoves(board).Contains(index))
            {
                //There is a Peice here. Capture it (Moving to allied space would not be legal. No need for team check)
                if (board.boardState[index.x, index.y].chessPiece != null)
                {
                    //Debug.Log("Capture");
                    board.Capture(SelectedPiece.index, index);
                    SelectedPiece = null;
                    ResetHighlight();
                }
                else
                {
                    //Debug.Log("Move Here");
                    board.MovePiece(SelectedPiece.index, index);
                    SelectedPiece = null;
                    ResetHighlight();
                }

            }
            //Not A legal Move. Cancel Out.
            else
            {
                //Debug.Log("Not Legal. Cancel");
                SelectedPiece = null;
                ResetHighlight();
            }

        }
    }

    void ResetHighlight()
    {
        foreach (SpriteRenderer renderer in visualSquares) {
            renderer.color = Color.clear;
        }
    }

}
