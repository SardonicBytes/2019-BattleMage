using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Controller : MonoBehaviour
{


    public SpriteRenderer spriteRenderer;
    public Animator anim;
    public Rigidbody2D rb;
    public NavMeshAgent NMA;

    public GameObject phAttackEffects;
    public SpriteRenderer phAttackEffectsSpriteRenderer;
    public Animator phAttackEffectsAnimator;

    public float MoveSpeed;

    private Vector2 faceDirection;
    public Vector2 input;
    private Vector2 moveDirection;

    


    // Start is called before the first frame update
    void Start()
    {

        if (!CheckForComponents())
            return;
        

    }

    // Update is called once per frame
    void Update()
    {

        HandleFacing(); ;
        Movement();



    }

    void Movement()
    {
        // If we're not really giving it any input
        if (input.SqrMagnitude() < 0.05f)
        {
            CCMove(Vector2.zero);
            anim.SetFloat("Speed", 0);
            return;
        }
        moveDirection = input;
        CCMove(moveDirection * MoveSpeed);
        anim.SetFloat("Speed", 1);
    }

    void HandleFacing()
    {
        // If we're not really giving it any input
        if (moveDirection.SqrMagnitude() < 0.05f)
        {
            return;
        }
        spriteRenderer.flipX = moveDirection.x > 0f ? false : true;
        anim.SetFloat("yMovement", moveDirection.y);
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

    void CCMove(Vector2 Movement) {
        Vector3 v3Movement = new Vector3 (Movement.x ,Movement.y ,0f);
        rb.velocity = v3Movement;
    }

    public void Attack()
    {
        phAttackEffectsSpriteRenderer.flipX = spriteRenderer.flipX;
        phAttackEffectsAnimator.SetTrigger("Slash");
    }
}
