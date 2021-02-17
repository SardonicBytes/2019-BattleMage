using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ChessPlayerController))]
public class ObjectPickup : MonoBehaviour
{

    public Board ActiveBoard
    {
        get { if (activeBoard != null)
            {
                return activeBoard;
            }
            else
            {
                return FindObjectOfType<Board>();
            }
        }
        set { activeBoard = value; }
    }
    private Board activeBoard;

    public float offset = 0.5f;
    public float pickUpSensitivity = 0.25f;
    public float smoothing = 1f;
    private float snap = 0.01f;
    private float switchSideSpeed = 20f;
    Pickupable heldObject;
    ChessPlayerController chessPlayerController;

    public float holdOffset;

    //Vector3 origin = transform.position + MathTools.V2ToV3(chessPlayerController.facingDirection) * offset;
    public Vector3 Origin
    {
        get { return transform.position + MathTools.V2ToV3(chessPlayerController.facingDirection) * offset; }
    }

    private void Start()
    {
        chessPlayerController = GetComponent<ChessPlayerController>();
    }


    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && chessPlayerController.isPlayer)
        {
            if (heldObject == null)
            {
                AttemptPickup();
            }
            else
            {
                DropObject();
            }
        }

        DebugUpdate();
    }

    void AttemptPickup()
    {


        //Find all objects in a small area around the front of our character
        //Vector3 origin = transform.position + MathTools.V2ToV3(chessPlayerController.facingDirection) * offset;
        RaycastHit2D[] hit = Physics2D.CircleCastAll(Origin, pickUpSensitivity, Vector2.right);
        if (hit.Length == 0)
            return;

        List<Pickupable> allPickupableObjects = new List<Pickupable>();
        for (int i = 0; i < hit.Length; i++)
        {
            //OPT
            Pickupable thisPickup = hit[i].transform.gameObject.GetComponent<Pickupable>();
            if (thisPickup != null)
            {
                allPickupableObjects.Add(thisPickup);
            }

        }
        if (allPickupableObjects.Count == 0)
            return;
        //OPT Clean up this mess of a sorting loop
        //Find the closest object to our origin point
        float shortestDistance = 10f;
        Pickupable closestPickupable = allPickupableObjects[0];
        for (int i = 0; i < allPickupableObjects.Count; i++)
        {
            float thisDistance = Vector3.Distance(Origin, allPickupableObjects[i].transform.position);
            if (shortestDistance > thisDistance)
            {
                shortestDistance = thisDistance;
                closestPickupable = allPickupableObjects[i];
            }
        }
        Pickup(closestPickupable);

    }

    void Pickup( Pickupable objectToPickup)
    {
        heldObject = objectToPickup;
        //Let the Object know it has been picked up
        objectToPickup.Pickup();
        StartCoroutine(ICarryObject(objectToPickup));
    }

    void DropObject()
    {
        if (heldObject == null)
        {
            return;
        }
        Vector3 placeLocation = transform.position + (MathTools.V2ToV3(chessPlayerController.facingDirection.normalized) * holdOffset / 2);
        placeLocation = FindObjectOfType<Board>().Quantize(placeLocation);
        StartCoroutine(IMoveSmoothly(heldObject.transform, placeLocation)); 
        heldObject = null;

    }

    IEnumerator IMoveSmoothly(Transform trans, Vector3 endLocation )
    {
        while (Vector3.Distance(trans.position, endLocation) > snap) {
            trans.position = Vector3.Slerp(trans.position, endLocation, Time.deltaTime* smoothing);
            yield return 0;
        }
        trans.position = endLocation;


        //Hack
        Transform glowEffect = transform.Find("Glow Effect");
        glowEffect.gameObject.SetActive(false);
        ///
    }


    public Vector3 trueOffset;
    IEnumerator ICarryObject( Pickupable objectToCarry )
    {
        //Hack
        GetComponent<MovementController>().obstacles.Remove(objectToCarry.transform);
        Transform glowEffect = transform.Find("Glow Effect");
        glowEffect.gameObject.SetActive(true);
        //EndHack
        while (heldObject != null) {

            trueOffset = chessPlayerController.facingDirection.normalized * (holdOffset);
            Vector3 goalPosition = transform.position + trueOffset;
            objectToCarry.transform.position = Vector3.Slerp(objectToCarry.transform.position, goalPosition, Time.deltaTime * switchSideSpeed);

            //Possibly remove from here?
            glowEffect.position = objectToCarry.transform.position + new Vector3(0f,0.5f,0f);
            ///////
            yield return 0;
        }
        //Hack
        GetComponent<MovementController>().obstacles.Add(objectToCarry.transform);
        //EndHack

    }

    void DebugUpdate()
    {
        //DO SOME STUFF 
    }
}
