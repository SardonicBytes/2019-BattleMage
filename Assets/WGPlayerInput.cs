using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WGPlayerInput : MonoBehaviour
{
    public Transform phBanner;
    public WGController controller;
    public BannerInteraction bannerInteraction;
    public float bannerPickUpDistance = 1f;

    public bool controlled = false;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<WGController>(); ;
        bannerInteraction = GetComponent<BannerInteraction>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha0)){
            controlled = !controlled;
        }
        if (!controlled) return;

        Camera.main.transform.position = transform.position + new Vector3(0,0,-5);


        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (input.sqrMagnitude > 1)
            input.Normalize();
        controller.input = input;

        if (Input.GetMouseButtonDown(0))
        {
            if (Vector3.Distance(transform.position, phBanner.position) < bannerPickUpDistance)
            {
                bannerInteraction.ToggleBanner();
            }
            else
            {
                controller.Attack();
            }
        }

    }
}
