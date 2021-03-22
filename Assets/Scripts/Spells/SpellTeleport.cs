using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellTeleport : Spell
{
    public BoardVisuals boardVisuals;
    private Vector3Int mouseWorldPosition;


    public GameObject highlightSquare;
    private SpriteRenderer highlight;
    public float highlightValue;

    private Vector3Int pieceIndexA;
    private Vector3Int pieceIndexB;

    private void Start()
    {
        highlight = Instantiate(highlightSquare, transform.position + -Vector3.forward, Quaternion.identity, transform).GetComponent<SpriteRenderer>();
        highlight.color = Color.clear;
    }

    //When the spell is selected. Don't immediately cast by default, as additional inputs may be required.
    public override void PrepareToCast()
    {
        Debug.Log("Prepare");
        StopAllCoroutines();
        StartCoroutine(IPrepareToCast());
    }

    //Cast the spell with the given inputs
    public override void Cast()
    {
        Debug.Log("Cast");
        pieceIndexA = mouseWorldPosition;
        StartCoroutine(IPrepareSecondCast());
    }

    IEnumerator IPrepareToCast()
    {
        highlight.color = new Color(255, 255, 255, highlightValue);
        yield return null;
        while (true)
        {
            if (Input.GetMouseButtonDown(1)){

                break;
            }

            if (Input.GetMouseButtonDown(0)) {

                mouseWorldPosition = Vector3Int.RoundToInt( Camera.main.ScreenToWorldPoint(Input.mousePosition));
                mouseWorldPosition.z = 0;

                //Hacky placeholder test to see if we're hovering over the board.
                if (mouseWorldPosition.x < boardVisuals.boardSize && mouseWorldPosition.x >= 0 && mouseWorldPosition.y < boardVisuals.boardSize && mouseWorldPosition.y >= 0 )
                {
                    Cast();
                    break;
                }
                else
                {
                    break;
                }
                
            }
            
            yield return null;
        }
        highlight.color = Color.clear;
    }

    IEnumerator IPrepareSecondCast()
    {
        highlight.color = new Color(255, 255, 255, highlightValue);
        yield return null;
        while (true)
        {
            if (Input.GetMouseButtonDown(1))
            {

                break;
            }

            if (Input.GetMouseButtonDown(0))
            {

                mouseWorldPosition = Vector3Int.RoundToInt(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                mouseWorldPosition.z = 0;

                //Hacky placeholder test to see if we're hovering over the board.
                if (mouseWorldPosition.x < boardVisuals.boardSize && mouseWorldPosition.x >= 0 && mouseWorldPosition.y < boardVisuals.boardSize && mouseWorldPosition.y >= 0)
                {
                    SecondCast();
                    break;
                }
                else
                {
                    break;
                }

            }

            yield return null;
        }
        highlight.color = Color.clear;
    }

    void SecondCast()
    {
        Debug.Log("Second Cast");

        pieceIndexB = mouseWorldPosition;
        boardVisuals.board.SwitchPlaces( new Vector2Int (pieceIndexA.x,pieceIndexA.y), new Vector2Int (pieceIndexB.x, pieceIndexB.y) );

    }


}
