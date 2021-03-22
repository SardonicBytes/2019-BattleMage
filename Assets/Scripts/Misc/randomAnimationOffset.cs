using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// For the love of all that is spaghetti Don't ever allow this quick hack into a final version
public class randomAnimationOffset : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    
    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(DelayedStart());
    }

    IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(Random.Range(0f,2f));
        anim.SetTrigger("Start");
    }

}
