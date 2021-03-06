﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCreateShield : Spell
{
    public BoardVisuals boardVisuals;
    private Vector3Int mouseWorldPosition;
    public GameObject shieldPrefab;

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
        StopAllCoroutines();
        StartCoroutine(IPrepareToCast());
    }

    //Cast the spell with the given inputs
    public override void Cast()
    {
        if (boardVisuals.board.boardState[mouseWorldPosition.x, mouseWorldPosition.y].spellEffect != null)
        {
            if (boardVisuals.board.boardState[mouseWorldPosition.x, mouseWorldPosition.y].spellEffect.GetType() == typeof(Shield))
            {
                boardVisuals.board.DestroySpell(new Vector2Int(mouseWorldPosition.x, mouseWorldPosition.y));
                return; 
            }
            return;
        }
        if (boardVisuals.board.boardState[mouseWorldPosition.x, mouseWorldPosition.y].chessPiece == null)
        {
            return;
        }
        GameObject newShield = Instantiate(shieldPrefab, mouseWorldPosition,Quaternion.identity);
        newShield.GetComponent<Shield>().boardVisuals = boardVisuals;
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
}
