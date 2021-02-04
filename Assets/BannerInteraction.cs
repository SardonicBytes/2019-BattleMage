using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BannerInteraction : MonoBehaviour
{
    public Vector2 holdPosition;
    public Transform myBanner;
    public bool holdingBanner;

    // Update is called once per frame
    public void PickupBanner()
    {
        holdingBanner = true;
    }
    public void DropBanner()
    {
        holdingBanner = false;
    }

    public void ToggleBanner()
    {
        holdingBanner = !holdingBanner;
    }

    private void Update()
    {
        if (holdingBanner) {

            myBanner.position = new Vector3(transform.position.x + holdPosition.x, transform.position.y + holdPosition.y, myBanner.position.z);

        }
    }

}
