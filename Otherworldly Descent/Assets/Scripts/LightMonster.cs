using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightMonster : MonoBehaviour
{
    public GameObject player;
    public GameObject lightMonster;
    public PlayerMove playerScript;
    public Light lightSource;
    public static bool gameState = true;
    private Transform location;
    RaycastHit hit;
    bool damaged = true;

    //variables used to change settings of game in editor
    public float lowTime, highTime, waitAfterDamage, lowLRange, highLRange, lowLIntensity, highLIntensity;
    

    // Start is called before the first frame update
    void Start()
    {
        location = GetComponent<Transform>();
        StartCoroutine(LightController());
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform.position);
        if (lightSource.range == highLRange && lightSource.intensity == highLIntensity)
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
        float i;

        while (gameState)
        {
            lightSource.range = lowLRange;
            lightSource.intensity = lowLIntensity;
            i = Random.Range(lowTime, highTime);
            yield return new WaitForSecondsRealtime(i);

            lightSource.range = highLRange;
            lightSource.intensity = highLIntensity;
            i = Random.Range(lowTime, highTime);
            yield return new WaitForSecondsRealtime(i);
        }
    }

    private IEnumerator HealthLoss()
    {
        if (damaged)
        {
            playerScript.health--;
            damaged = false;
            yield return new WaitForSecondsRealtime(waitAfterDamage);
            damaged = true;
        }
    }
}
