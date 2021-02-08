using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Vector2 LeftStick {
        get { return leftStick; }
//        private set { leftStick = value; }
    }
    private Vector2 leftStick;

    // Update is called once per frame
    void Update()
    {
        leftStick.x = Input.GetAxisRaw("Horizontal");
        leftStick.y = Input.GetAxisRaw("Vertical");
    }
}
