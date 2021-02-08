using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChessPlayerController : MonoBehaviour
{

    public SpriteRenderer spriteRenderer;
    public Animator anim;
    public Rigidbody2D rb;

    private Vector2 faceDirection;
    public bool isPlayer = true;
    public bool takeInput = true;


    // Start is called before the first frame update
    void Start()
    {
        if (!CheckForComponents())
            return;
        if (!isPlayer)
            takeInput = false;
    }

    public void HandleFacing( Vector2 newFacingDirection )
    {
        // If we're not really giving it any input
        if (newFacingDirection.SqrMagnitude() < 0.05f)
        {
            return;
        }
        spriteRenderer.flipX = newFacingDirection.x > 0f ? false : true;
        anim.SetFloat("yMovement", newFacingDirection.y);
    }

    //Check to ensure we have all required components, Add them if we can, if not return error.
    private bool CheckForComponents()
    {
        if (!spriteRenderer)
        {
            print("No Sprite Renderer Attached");
            return false;
        }
        if (!anim)
        {
            print("No Animator Attached");
            return false;
        }
        if (!rb)
        {
            print("No RigidBody2D Attached");
            return false;
        }
        else return true;
    }

    public void BasicMove( Vector2 movement )
    {
        rb.velocity = movement;
    }

}
