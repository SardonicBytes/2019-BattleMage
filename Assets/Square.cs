using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square
{
    public enum HighlightType { Normal, MoveAvailable, CaptureAvailable };

    public GameObject gameObj;
    public Vector2Int Index;
    private bool isTinted;
    public bool IsTinted
    {
        get { return isTinted; }
        private set { isTinted = value; }
    }
    private Vector3 worldPosition;
    public Vector3 WorldPosition
    {
        get { return worldPosition; }
        private set { worldPosition = value; }
    }

    public SpriteRenderer renderer;
    /// <summary>
    /// Generates a new square
    /// </summary>
    /// <param name="newGameObject"></param>
    /// <param name="newIndex"></param>
    public Square(GameObject newGameObject, Vector2Int newIndex, Vector3 newWorldPosition, Transform parentTransform)
    {
        IsTinted = (newIndex.x + newIndex.y) % 2 == 0;
        gameObj = MonoBehaviour.Instantiate(newGameObject, newWorldPosition, Quaternion.identity, parentTransform);
        renderer = gameObj.GetComponent<SpriteRenderer>();
        SetColour(HighlightType.Normal);
        worldPosition = newWorldPosition;

    }

    public void SetColour(HighlightType type)
    {
        Color newColour = Color.magenta;
        switch (type)
        {
            case HighlightType.Normal:
                if (isTinted)
                    newColour = ColourKeys.normal;
                else
                    newColour = ColourKeys.tinted;
                break;
            case HighlightType.MoveAvailable:
                newColour = ColourKeys.moveAvailable;
                break;
            case HighlightType.CaptureAvailable:
                newColour = ColourKeys.captureAvailable;
                break;
        }
        renderer.color = newColour;
    }


}
