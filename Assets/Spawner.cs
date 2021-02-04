using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject Wizard;
    public GameObject Minion;

    public float spawnDelay;
    public float spawnLightIntensity;

    public Light spawnLight;
    public SpriteRenderer lightSprite;



    public void debugSpawn()
    {
        Spawn(true, 0);
    }

    public void Spawn ( bool isWizard, int team )
    {
        GameObject thingToSpawn = null;
        if (isWizard)
        {
            thingToSpawn = Wizard;
        }
        else
        {
            thingToSpawn = Minion;
        }

        StartCoroutine(InitiateSpawn(thingToSpawn));


    }
    IEnumerator InitiateSpawn( GameObject thingToSpawn )
    {
        float spawnTime = Time.timeSinceLevelLoad + spawnDelay;
        float myLightIntensity = 0f;
        float speedOfLightChange = spawnLightIntensity / spawnDelay;

        while (Time.timeSinceLevelLoad < spawnTime) {
            myLightIntensity += Time.deltaTime * speedOfLightChange;
            lightSprite.color = new Color (lightSprite.color.r, lightSprite.color.g, lightSprite.color.b, Mathf.Lerp(lightSprite.color.a, 255, Time.deltaTime));
            yield return 0;
        }

        GameObject newSpawn = GameObject.Instantiate(thingToSpawn,transform.position,Quaternion.identity);

        while (myLightIntensity > 0f) {
            myLightIntensity -= Time.deltaTime * speedOfLightChange;
            yield return 0;
        }



    }
}
