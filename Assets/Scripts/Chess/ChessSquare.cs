using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessSquare : MonoBehaviour
{
    public BoardVisuals boardVisuals;
    private Board board;
    private Vector2Int index;

    public void Start()
    {
        index = new Vector2Int(Mathf.FloorToInt(transform.position.x), Mathf.FloorToInt(transform.position.y));
        board = boardVisuals.board;
    }

    private void OnMouseDown()
    {
        boardVisuals.SelectSquare(index);
    }
}
