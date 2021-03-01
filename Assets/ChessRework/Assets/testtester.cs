using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testtester : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EndQuote());
    }

    public Text endQuote;
    IEnumerator EndQuote()
    {
        float fadeTime = 1.5f;
        float waitTime = 1f;
        float alpha = 0;

        while (alpha < 1)
        {

            alpha += Time.deltaTime / fadeTime;

            endQuote.color = new Color(endQuote.color.r,
                endQuote.color.g,
                endQuote.color.b,
                alpha);
            //Goes to next frame
            yield return null;
        }
        yield return new WaitForSeconds(waitTime);
        while (endQuote.color.a > 0)
        {

            alpha -= Time.deltaTime / fadeTime;

            endQuote.color = new Color(endQuote.color.r,
                endQuote.color.g,
                endQuote.color.b,
                alpha);
            //Goes to next frame
            yield return null;
        }

    }
}
