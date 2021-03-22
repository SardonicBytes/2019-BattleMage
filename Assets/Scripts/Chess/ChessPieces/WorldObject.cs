using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldObject : MonoBehaviour
{
    public ChessPiece chessPiece;
    public BoardVisuals boardVisuals;

    private void Start()
    {
        boardVisuals = GameObject.FindObjectOfType<BoardVisuals>();
        Vector2Int newIndex = new Vector2Int(Mathf.FloorToInt(transform.position.x), Mathf.FloorToInt(transform.position.y));
        chessPiece.UpdateIndex (boardVisuals.board, newIndex);
    }

}
