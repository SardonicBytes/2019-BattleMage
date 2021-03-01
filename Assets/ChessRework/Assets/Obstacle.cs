using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public BoardVisuals boardVisuals;
    public Vector2Int index;

    // Start is called before the first frame update
    void Start()
    {
        Vector2Int newIndex = new Vector2Int(Mathf.FloorToInt(transform.position.x), Mathf.FloorToInt(transform.position.y));
        UpdateIndex(boardVisuals.board, newIndex);
    }

    public virtual void UpdateIndex(Board board, Vector2Int newIndex)
    {
        board.boardState[newIndex.x, newIndex.y].obstacle = this;
        index = newIndex;
    }

}
