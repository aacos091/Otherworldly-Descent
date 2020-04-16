using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneSpawner : MonoBehaviour
{

    public Transform[]  runeSpawns;
    public GameObject[] runes;
    private int[] usedSpawns;


    public int currentRune = 1;
    public float runeAmount;
    public int randomSpawnInt;
    public int spawn;
    private int randomSpawnMin = 1;
    private int randomSpawnMax = 3;
    public bool randomSpawnBool;

    public bool deletedRunes;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DeleteRunes()
    {
        GameObject[] objs;
        objs = GameObject.FindGameObjectsWithTag("Rune");
        foreach (GameObject rune in objs)
            GameObject.Destroy(rune);
        deletedRunes = true;
        currentRune = 0;
        spawn = 0;
        randomSpawnMin = 0;
        randomSpawnMax = 3;
    }

    public void SpawnRunes()
    {



        if (deletedRunes)
        {
            if (randomSpawnBool)
            {
                randomSpawnInt = Random.Range(randomSpawnMin, randomSpawnMax);
                if (currentRune < runeAmount)
                {
                    GameObject newObj = Instantiate(runes[currentRune], runeSpawns[randomSpawnInt].position, Quaternion.identity) as GameObject;
                    currentRune++;
                    randomSpawnMin += 3;
                    randomSpawnMax += 3;
                    SpawnRunes();

                }
                else
                print("Runes Spawning Randomly");
            }
            else
            {
                if (currentRune < runeAmount)
                {
                    GameObject newObj = Instantiate(runes[currentRune], runeSpawns[spawn].position, Quaternion.identity) as GameObject;
                    currentRune++;
                    spawn += 3;
                    SpawnRunes();

                }
                else
                {
                    print("Runes Spawned");

                }
            }
        }
        
        
    }

}
