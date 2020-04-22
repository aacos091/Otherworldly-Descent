using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrbController : MonoBehaviour
{

    private bool respawn;
    public bool uniqueOrb = true;
    public float orbRespawnTime;
    public GameObject orbYellow;
    public GameObject orbRed;
    private ScoreController scoreController;
    // Start is called before the first frame update
    void Start()
    {
     scoreController = GameObject.Find("ScoreController").GetComponent<ScoreController>();


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator RespawnTime()
    {
        if (respawn)
        {
            yield return new WaitForSeconds(orbRespawnTime);
            orbRed.SetActive(true);
            respawn = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerHitbox")
        {
            if(uniqueOrb)
            {
                scoreController.orbsCollected++;
                uniqueOrb = false;
            }
            Destroy(orbYellow);
            orbRed.SetActive(false);
            respawn = true;
            StartCoroutine(RespawnTime());
        }
    }
}
