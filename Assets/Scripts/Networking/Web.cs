using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Web : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetText());
    }

    IEnumerator GetText() {
        using (UnityWebRequest www = UnityWebRequest.Get("http://localhost/battlemage/GetDate.php"))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                //show results as text
                Debug.Log(www.downloadHandler.text);
                //get as binary
                byte[] results = www.downloadHandler.data;
            }



        }
    }
}
