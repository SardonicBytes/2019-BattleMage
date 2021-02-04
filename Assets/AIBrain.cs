using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AIBrain : MonoBehaviour
{
    public Controller controller;
    public AIPath aiPath;
    public AIDestinationSetter aiDestinationSetter;
    public Seeker seeker;
    public Transform banner;
    bool inFormationLastframe;
    private bool inFormation;

    public float maxDistanceFromFormation = 2f;
    public float graceDistance = 1f;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<Controller>();
        aiPath = GetComponent<AIPath>();
        aiDestinationSetter = GetComponent<AIDestinationSetter>();
        seeker = GetComponent<Seeker>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.P)){
            //aiPath.
        }

        controller.input = aiPath.velocity;

        inFormationLastframe = inFormation;
        float distance = Vector3.Distance(transform.position, banner.position);

        if (distance > maxDistanceFromFormation + graceDistance)
        {
            aiDestinationSetter.target = banner;
        }
        else
        {
            aiPath.destination = transform.position;
            aiDestinationSetter.target = null;
        }


    }



}
