using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayOverUnderPlayer : MonoBehaviour
{

    public Transform player;
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        spriteRenderer.sortingOrder = transform.position.y < player.position.y ? 100 : 10;
    }
}
