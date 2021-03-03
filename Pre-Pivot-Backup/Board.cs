using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{

    public Color normalHighlight;
    public Color tintedHighlight;
    public Color moveAvailableHighlight;
    public Color captureAvailableHighlight;


    public enum HighlightType { Normal, MoveAvailable, CaptureAvailable };

    public GameObject baseSquare;
    Grid grid;
    public int gridSize = 30;
    [SerializeField]
    public int GridSize
    {
        get { return gridSize;}
        //guarantee that this is Always an even number
        set {
            if (gridSize % 2 != 0)
                gridSize++;
            gridSize = value;}
    }
    Square[,] squares;

    // Start is called before the first frame update
    void Start()
    {
        SetColourKey();
        grid = GetComponent<Grid>();
        InstantiateGrid();

    }

    void InstantiateGrid()
    {
        squares = new Square[gridSize,gridSize];

        Vector2Int index = Vector2Int.zero;
        for (index.x = 0; index.x < gridSize; index.x ++ ) {
            for (index.y = 0; index.y < gridSize; index.y++)
            {

                Vector3 newPosition = grid.GetCellCenterWorld(new Vector3Int(index.x - gridSize / 2, index.y - gridSize/2,0));
                squares[index.x, index.y] = new Square(baseSquare, index, newPosition, transform);

            }
        }
    }

    public void SetColourKey()
    {
        ColourKeys.normal = normalHighlight;
        ColourKeys.tinted = tintedHighlight;
        ColourKeys.moveAvailable = moveAvailableHighlight;
        ColourKeys.captureAvailable = captureAvailableHighlight;
    }

    public Vector3 Quantize(Vector3 rawPosition) {

        rawPosition = grid.WorldToCell(rawPosition) + grid.cellSize/2;
        return rawPosition;
    }

}
