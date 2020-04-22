using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class runeController : MonoBehaviour
{
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerHitbox")
        {

            scoreController.runesCollected++;
            Destroy(this.gameObject);
        }
    }
}
