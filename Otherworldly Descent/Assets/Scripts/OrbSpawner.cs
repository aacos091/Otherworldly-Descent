using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbSpawner : MonoBehaviour
{
    public Transform[] orbSpawns;
    public Transform orbContainer;
    public GameObject orb;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void DeleteOrbs()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Orb");
        foreach (GameObject orb in objs)
            GameObject.Destroy(orb);
        SpawnOrbs();
    }

    




    public void SpawnOrbs()
    {
        foreach (Transform currentSpawn in orbSpawns)
        {
            GameObject newObj = Instantiate(orb, currentSpawn.transform.position, Quaternion.identity);
            newObj.transform.parent = orbContainer;
        }
    }

}
