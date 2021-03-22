using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellEffect : MonoBehaviour
{
    public BoardVisuals boardVisuals;
    public Vector2Int index;

    // Start is called before the first frame update
    public virtual void Start()
    {
        Vector2Int newIndex = new Vector2Int(Mathf.FloorToInt(transform.position.x), Mathf.FloorToInt(transform.position.y));
        UpdateIndex(boardVisuals.board, newIndex);
    }

    public virtual bool MoveToThisPoint( Vector2Int newIndex )
    {
        return false;
    }

    public virtual bool MoveThisPiece( Vector2Int newStartIndex, Vector2Int newEndIndex)
    {
        return false;
    }

    public virtual bool ActAsObstacle()
    {
        return false;
    }

    public virtual void UpdateIndex(Board board, Vector2Int newIndex )
    {
        board.boardState[newIndex.x, newIndex.y].spellEffect = this;
        index = newIndex;
    }



}
