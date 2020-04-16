using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightMonster : MonoBehaviour
{
    //Difficulty stuff
    public bool doubleDamage;
    public float phaseMultiplyer;


    public float lightPeriod;

    
    public GameObject player;
    public GameObject lightMonster;
    public PlayerMove playerScript;
    public Light lightSource;
    public static bool gameState = true;
    private Transform location;
    RaycastHit hit;
    bool damaged = true;
    private DarkMonsterController darkMonsterController;


    //variables used to change settings of game in editor
    public float lowTime, highTime, waitAfterDamage, lowLRange, highLRange, lowLIntensity, highLIntensity;
    

    // Start is called before the first frame update
    void Start()
    {
        location = GetComponent<Transform>();
        StartCoroutine(LightController());
        lightSource.intensity = lowLIntensity;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(lightSource.intensity);
        transform.LookAt(player.transform.position);
        if (lightSource.range == highLRange && lightSource.intensity >= (highLIntensity - 0.1f))
        {

            Vector3 playerPosition = new Vector3(player.transform.position.x, lightMonster.transform.position.y, player.transform.position.z);
            lightMonster.transform.LookAt(playerPosition);

            if (Physics.Raycast(location.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
            {
                if (hit.collider.gameObject.tag == "Player")
                {
                    StartCoroutine(HealthLoss());
                }
            }
        }

    }

    private IEnumerator LightController()
    {
        

        while (gameState)
        {




            //sets a random amount of time the light will stay in this intensity
            lightPeriod = (Random.Range(lowTime, highTime) * phaseMultiplyer);
            yield return new WaitForSecondsRealtime(lightPeriod);

            playerScript.checkMonsterLOS = true;
            //darkMonsterController.RunAwayFunction();
            lightSource.range = highLRange;
            //Increases intenisty over time. Multiply "Time.deltaTime" by a number to speed it up, or divide by a number to slow it down.
            for (float i = lowLIntensity; i < highLIntensity; i += (Time.deltaTime))
            {

                lightSource.intensity = i;
                yield return null;
            }

            //sets a random amount of time the light will stay in this intensity
            lightPeriod = Random.Range(lowTime, highTime);
            yield return new WaitForSecondsRealtime(lightPeriod);

                        lightSource.range = lowLRange;
            //Decreases intensity overtime. Multiply "Time.deltaTime" by a number to speed it up, or divide by a number to slow it down.
            for (float i = highLIntensity; i >= lowLIntensity; i -= (Time.deltaTime))
            {
                
                lightSource.intensity = i;
                yield return null;
            }
        }
    }

    private IEnumerator HealthLoss()
    {
        if (damaged)
        {
            if (doubleDamage)
            {
                playerScript.health = -2;
            }
            else
            {
                playerScript.health-- ;
            }
            damaged = false;
            yield return new WaitForSecondsRealtime(waitAfterDamage);
            damaged = true;
        }
    }
}
