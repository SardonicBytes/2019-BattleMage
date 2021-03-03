using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCreateRock : Spell
{
    public BoardVisuals boardVisuals;
    private Vector3Int mouseWorldPosition;
    public GameObject Rock;

    public GameObject highlightSquare;
    private SpriteRenderer highlight;
    public float highlightValue;

    private void Start()
    {
        highlight = Instantiate(highlightSquare, transform.position + -Vector3.forward, Quaternion.identity, transform).GetComponent<SpriteRenderer>();
        highlight.color = Color.clear;
    }

    //When the spell is selected. Don't immediately cast by default, as additional inputs may be required.
    public override void PrepareToCast()
    {
        Debug.Log("Prepare To Cast");
        StopAllCoroutines();
        StartCoroutine(IPrepareToCast());
    }

    //Cast the spell with the given inputs
    public override void Cast()
    {
        Debug.Log("Cast");
        GameObject newRock = Instantiate(Rock, mouseWorldPosition,Quaternion.identity);
        newRock.GetComponent<Obstacle>().boardVisuals = boardVisuals;
    }

    IEnumerator IPrepareToCast()
    {
        highlight.color = new Color(255, 255, 255, 0.4f);
        yield return null;
        while (true)
        {
            if (Input.GetMouseButtonDown(1)){
                Debug.Log("Cancel");
                break;
            }

            if (Input.GetMouseButtonDown(0)) {

                mouseWorldPosition = Vector3Int.RoundToInt( Camera.main.ScreenToWorldPoint(Input.mousePosition));
                mouseWorldPosition.z = 0;

                //Hacky placeholder test to see if we're hovering over the board.
                if (mouseWorldPosition.x < 9 && mouseWorldPosition.x > -1 && mouseWorldPosition.y < 9 && mouseWorldPosition.y > -1)
                {
                    Cast();
                }
                else
                {
                    Debug.Log("Cancel");
                    break;
                }
                
            }
            
            yield return null;
        }
        highlight.color = Color.clear;
    }
}
