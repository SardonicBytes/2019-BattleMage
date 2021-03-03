using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : SpellEffect
{
    public override bool ActAsObstacle()
    {
        return true;
    }

    public override bool MoveThisPiece(Vector2Int newStartIndex, Vector2Int newEndIndex)
    {

        if (newStartIndex != index)
        {
            Debug.LogError("Something Moved Into An Invulnerable space");
            return false;
        }
        transform.position = Board.V2ToWorld(newEndIndex);
        boardVisuals.board.MoveSpellEffect(newStartIndex, newEndIndex);
        return false;
    }
}
