using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnTest : MonoBehaviour
{
    public GameObject model;
    public GameObject particles;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "FlashArea")
        {
            print("flashed");
            GameObject newObj = Instantiate(particles, model.transform.position, Quaternion.identity) as GameObject;
            model.SetActive(false);
        }
    }



}
