using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{





    //Spawn point stuff
    public Transform[] spawnPoints;

    public Transform playerPosition;
    private float distance;

    public GameObject darkMonster;
    DarkMonsterController darkMonsterController;



    private bool playerLost;

    //Spawn stuff
    public bool canSpawn;
    private bool startSpawn;
    public int behaviorChance;
    public int behaviorChanceMax;
    public int behaviorChanceMin;
    private int behaviorChanceAddition;
    private Transform currentPoint;
    private int randomSpawnPoint;
    

    //Spawn times
    public float monsterSpawnTimeStart;
    public float monsterSpawnTimeMinimum;
    public float monsterSpawnTimeMaximum;
    private float spawnDelay;

    // Start is called before the first frame update
    void Start()
    {
       
        startSpawn = true;
        //StartCoroutine(StartSpawn());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("escape"))
           Application.Quit();

        
        if (playerLost)
        {
            //GameObject.Find("PauseMenuCanvas").GetComponent<pausemenu>().Pause();
        }

        if(canSpawn)
        {
            //StartCoroutine(Spawn());
        }

    }


    public void FindSpawnPoint()
    {

        randomSpawnPoint = Random.Range(0, spawnPoints.Length);
        currentPoint = spawnPoints[randomSpawnPoint];
        distance = Vector3.Distance(playerPosition.position, currentPoint.position);
        
        if (distance > 30)
        {
            currentPoint = spawnPoints[randomSpawnPoint];
            GameObject newObj = Instantiate(darkMonster, spawnPoints[randomSpawnPoint].position, Quaternion.identity) as GameObject;
            darkMonsterController = newObj.GetComponent<DarkMonsterController>();
        }
        else
        {
            FindSpawnPoint();
        }
        
    }

    public IEnumerator StartSpawn()
    {
        while(startSpawn)
        {
            yield return new WaitForSeconds(monsterSpawnTimeStart);
            FindSpawnPoint();
            startSpawn = false;
            yield return new WaitForSeconds(1);
            darkMonsterController.behavior = darkMonsterController.AttackPlayer;
            darkMonsterController.checkLargeRange = true;
        }

            
    }

    private IEnumerator Spawn()
    {
        if(canSpawn)
        {
            canSpawn = false;
            behaviorChance = (Random.Range(behaviorChanceMin, behaviorChanceMax) + behaviorChanceAddition);

            spawnDelay = Random.Range(monsterSpawnTimeMinimum, monsterSpawnTimeMaximum);
            yield return new WaitForSeconds(spawnDelay);
            FindSpawnPoint();
            yield return new WaitForSeconds(1);


            if (behaviorChance < 1)
            {
                darkMonsterController.behavior = darkMonsterController.FollowAtDistance;
                darkMonsterController.checkLargeRange = true;
                behaviorChanceAddition += 10;
            }
            else if (behaviorChance < 2)
            {
                darkMonsterController.behavior = darkMonsterController.FollowThenAttack;
                darkMonsterController.checkLargeRange = true;
                behaviorChanceAddition += (Random.Range(5, 10));
            }
            else if (behaviorChance < 60)
            {
                darkMonsterController.behavior = darkMonsterController.AttackPlayer;
                behaviorChanceAddition += (Random.Range(5, 10));
            }
            else if (behaviorChance < 80)
            {
                darkMonsterController.behavior = darkMonsterController.FastAttack;
                behaviorChanceAddition = 20;
                darkMonsterController.agent.speed = 9;
            }
            else
            {
                darkMonsterController.behavior = darkMonsterController.AttackTwice;
                behaviorChanceAddition = 5;
            }

            yield return null;
        }

    }

    public void PlayerWins()
    {
        GameObject playerLook = GameObject.Find("Camera");
        Destroy(playerLook.GetComponent<PlayerLook>());
        GameObject playerMove = GameObject.Find("Player");
        Destroy(playerMove.GetComponent<PlayerMove>());
    }


   public void PlayerLoses()
    {
        playerLost = true;
        GameObject playerLook = GameObject.Find("Camera");
        Destroy(playerLook.GetComponent<PlayerLook>());
        GameObject playerMove = GameObject.Find("Player");
        Destroy(playerMove.GetComponent<PlayerMove>());
    }
}
