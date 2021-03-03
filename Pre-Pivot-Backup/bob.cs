using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bob : MonoBehaviour
{
    public int seconds;
    //GameObject player;
    public UnityEngine.UI.Text display;

    private void Start()
    {
        display = GetComponent<UnityEngine.UI.Text>();
        //player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (seconds <= 0)
        {
            seconds = 0;
            //player.GetComponent<controller>().Die();
        }

        if (seconds != 0)
        {
            Invoke("Subtract", 1f);
        }

        display.text = seconds.ToString();

    }
}
