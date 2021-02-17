using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(ChessPlayerController))]
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Animator))]

///</summary> Intended to handle movement in the form of walking + running, 
public class MovementController : MonoBehaviour
{
    private Animator anim;
    private ChessPlayerController chessPlayerController;
    private PlayerInput playerInput;

    //Base Movement
    public float moveSpeed = 8;
    public float accelleration = 5;

    //Steering
    public float peiceAvoidance = 1;
    public float avoidanceMaxDistance = 1;
    public float angularAvoidanceWeight = 1;
    public List<Transform> obstacles = new List<Transform>();

    public Vector2 moveDirection;
    public Vector2 currentSpeed;

    public Vector2 moveDir = new Vector2();

    public 


    // Start is called before the first frame update
    void Start()
    {
        if(!chessPlayerController)
            chessPlayerController = GetComponent<ChessPlayerController>();
        //if (!playerInput && chessPlayerController.isPlayer)

            playerInput = GetComponent<PlayerInput>();
        if (!anim)
            anim = GetComponentInChildren<Animator>();

        //Garbage for Debug/Test
        GameObject[] obstacleObjects = GameObject.FindGameObjectsWithTag("Respawn");
        for (int i = 0; i < obstacleObjects.Length; i++)
        {
            obstacles.Add(obstacleObjects[i].transform);
        }

    }

    private void Update()
    {
        Animate();
        if (!chessPlayerController.takeInput){
            return;
        }

        HandlePlayerMovement();

        chessPlayerController.HandleFacing(moveDir);

    }

    //Only called by direct controlled player Characters
    public void HandlePlayerMovement()
    {
        //Get Basic Accelleration + Movement from input
        moveDir = Vector2.Lerp(
            moveDir,
            playerInput.LeftStick * moveSpeed,
            accelleration * Time.deltaTime);

        //Add Steering for smoother movement through narrow spaces.
        if (playerInput.LeftStick.sqrMagnitude > 0.2)
        {
            moveDir += SteerForAvoidance(obstacles);
            //moveDir += SteerForAvoidance(obstacles[0].position);
        }

        //Send it
        chessPlayerController.BasicMove(moveDir);

    }

    public Vector2 SteerForAvoidance(List<Transform> transforms)
    {
        if (transforms.Count == 0)
            return Vector2.zero;
        List<Vector2> avoidanceVectors = new List<Vector2>();
        for (int i = 0; i < obstacles.Count; i++)
        {
            //Optimize this please. Shouldnt need to check for distance twice.
            if (Vector2.Distance(transform.position, obstacles[i].position) < avoidanceMaxDistance)
            {
                avoidanceVectors.Add( SteerForAvoidance(obstacles[i].position));
            }

        }

        if (avoidanceVectors.Count == 0)
            return Vector2.zero;

        Vector2 averageSteering = new Vector2(
            avoidanceVectors.Average(x=>x.x),
            avoidanceVectors.Average(x => x.y)
            );
        return averageSteering;
    }

    public float distanceMod;
    public float angleMod;

    public Vector2 SteerForAvoidance(Vector3 spotToAvoid)
    {

        //Simple Garbage Version
        distanceMod = Vector2.Distance(transform.position, spotToAvoid);
        distanceMod = Mathf.Max(avoidanceMaxDistance - distanceMod, 0);

        angleMod = -Vector2.SignedAngle(moveDir, spotToAvoid - transform.position);
        angleMod = Mathf.Clamp(angleMod, -90, 90);
        angleMod = angleMod > 0 ? (angleMod - 90)/90 : (angleMod + 90)/90;


        Vector2 steerDir = -Vector2.Perpendicular(moveDir)
            * distanceMod * angleMod * peiceAvoidance;

        return steerDir;
    }

    void Animate()
    {
        float animSpeed = moveDir.sqrMagnitude < 0.2f ? 0 : 1;
        anim.SetFloat("Speed", animSpeed);
    }

}

