using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine;

public class DarkMonsterController : MonoBehaviour
{
    //
    public Transform[] runAwayPoints;





    //Difficulty stuff
    public bool doubleDamage;
    public float speed;
    public float flashMultiplyer = 1;

    //Player information
    public PlayerMove playerScript;
    public Transform playerPosition;
    public ScoreController scoreController;

    //Behavior stuff
    public bool checkLargeRange;
    public bool playerAttacked;
    private bool canAttack = true;
    private bool attackOnce;
    public bool runAway;
    public bool alsoAttack;

    public delegate void MonsterBehavior();
    public MonsterBehavior behavior;

    
    public NavMeshAgent agent;
    private float runAwayTime;
    public int darkMonsterBehaviorSwitch;
    private int oldBehaviorSwitch;

    
    float longestDistance;
    float distance;
    private Transform farthestPoint;

    //Despawning stuff
    private int flashAmount;
    public int flashLimit;
    public bool despawn;
    private float playerDistance;
    public GameController gameController;
    public GameObject particles;
    private GameObject spawnedParticles;
    public GameObject particleSpawnPoint;
    public GameObject model;


    // Start is called before the first frame update
    void Start()
    {
        
        agent = GetComponent<NavMeshAgent>();
        agent.destination = playerPosition.position;
        darkMonsterBehaviorSwitch = 1;
        agent.speed = speed;
    }

    // Update is called once per frame
    void Update()
    {

        switch (darkMonsterBehaviorSwitch)
        {
            //Looks for the player
            case 1:
                //print("Finding player");
                agent.destination = playerPosition.position;
                break;

            //Once in range it will attack the player based on the set behavior
            case 2:
                //print("Attacking player");
                agent.destination = playerPosition.position;
                behavior();
                break;


            case 3:
                // print("Running Away!");
                RunAwayFunction();
                break;
        }
        if (despawn)
        {

            gameController.canSpawnDM = true;
            GameObject newObj = Instantiate(particles, particleSpawnPoint.transform.position, Quaternion.identity) as GameObject;
            Destroy(gameObject);



        }
    }

    public void AttackPlayer()
    {
        StartCoroutine(AttackingPlayer());
    }





    public void AttackTwice()
    {
        print("Attacking twice");
        StartCoroutine(AttackingPlayer());
    }

    public void FastAttack()
    {
        print("Fast attack");
        StartCoroutine(AttackingPlayer());
    }

    public void RunAwayFunction()
    {
        runAway = true;
        foreach (Transform point in runAwayPoints)
        {
            distance = Vector3.Distance(playerPosition.position, point.position);

            if (distance > longestDistance)
            {
                longestDistance = distance;
                farthestPoint = point;
            }
        }
        agent.destination = farthestPoint.position;
    }

    private IEnumerator AttackingPlayer()
    {
     
        print("Attacking the player");
        //Animation of monster attacking

        if (playerAttacked)
        {
            if(doubleDamage)
            {
                playerScript.health -= 2;
                playerAttacked = false;
                //scoreController.totalHealth =- 2;
            }
            else
            {
                playerScript.health-- ;
                //scoreController.totalHealth-- ;
            }

            playerAttacked = false;
            RunAwayFunction();

            despawn = true;
            darkMonsterBehaviorSwitch = 3;
            
        }

        else
        {
            yield return new WaitForSeconds(1);
            AttackPlayer();
        }
    }



    private IEnumerator RunAwayTime()
    {
        
        yield return new WaitForSeconds(0.1f);
        model.SetActive(false);
        agent.speed = 9;
        yield return new WaitForSeconds(runAwayTime * flashMultiplyer);
        Destroy(spawnedParticles);
        model.SetActive(true);
        agent.speed = speed;
        darkMonsterBehaviorSwitch = 1;
        runAway = false;
        canAttack = true;
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "FlashArea")
        {
            flashAmount++;
            if(flashAmount >= flashLimit)
            {
                despawn = true;
            }
            canAttack = false;
            darkMonsterBehaviorSwitch = 3;
            float distance = Vector3.Distance(playerPosition.position, transform.position) + 1;
            agent.speed = 0;
            runAwayTime = 1 / distance * 50;
            spawnedParticles = Instantiate(particles, particleSpawnPoint.transform.position, Quaternion.identity) as GameObject;
            
            StartCoroutine(RunAwayTime());
        }



        if (other.tag == "PlayerRange")
        {
            if(darkMonsterBehaviorSwitch == 1)
                darkMonsterBehaviorSwitch = 2;
        }

        if(other.tag == "PlayerHitbox")
        {
            if (canAttack)
            {
                playerAttacked = true;
                canAttack = false;
            }
        }
    }

}
