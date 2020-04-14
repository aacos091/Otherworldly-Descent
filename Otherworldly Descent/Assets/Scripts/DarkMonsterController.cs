using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine;

public class DarkMonsterController : MonoBehaviour
{
    //
    public Transform[] runAwayPoints;
    private int currentControlPointIndex = 0;

    //Stuff for picking behavior
    /*public int behaviorChance;
    public int behaviorChanceMax;
    public int behaviorChanceMin;
    public int behaviorChanceAddition;*/

    //Player information
    public PlayerMove playerScript;
    public Transform playerPosition;
    public GameObject playerBlock;
    public ScoreController scoreController;

    //Behavior stuff
    public bool checkLargeRange;
    public bool playerAttacked;
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
    public bool despawn;
    private float playerDistance;
    public GameController gameController;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = playerPosition.position;
        darkMonsterBehaviorSwitch = 1;
    }

    // Update is called once per frame
    void Update()
    {
        switch (darkMonsterBehaviorSwitch)
        {
            //Looks for the player
            case 1:
                print("Finding player");
                agent.destination = playerPosition.position;
                break;

                //Once in range it will attack the player based on the set behavior
            case 2:
                print("Attacking player");
                agent.destination = playerPosition.position;
                behavior();
                break;

                //If 
            case 3:
                print("Following player");
                agent.destination = playerPosition.position;
                behavior();
                break;

            case 4:
                print("Running Away!");
                RunAwayFunction();
                break;
        }
        if (despawn)
        {
            playerScript.checkMonsterLOS = true;
            if (playerScript.canSeeMonster == false)
            {
                gameController.canSpawn = true;
                playerScript.checkMonsterLOS = false;
                Destroy(gameObject);

            }
        }
    }

    public void AttackPlayer()
    {
        StartCoroutine(AttackingPlayer());
    }

    public void FollowAtDistance()
    {

        StartCoroutine(FollowAtDistanceTime());
        agent.stoppingDistance = 20;
    }

    public void FollowThenAttack()
    {
        StartCoroutine(FollowAtDistanceTime());
        alsoAttack = true;

        print("Following then attacking");
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
            RunAwayFunction();
            playerScript.health--;
            scoreController.totalHealth--;
            despawn = true;
            darkMonsterBehaviorSwitch = 4;
            
        }

        else
        {
            yield return new WaitForSeconds(1);
            AttackPlayer();
        }
    }


    private IEnumerator FollowAtDistanceTime()
    {
        print("Follow time");
        //Sppoky stuff goes here!
        yield return new WaitForSecondsRealtime(7);
        if(alsoAttack)
        {
            behavior = AttackPlayer;
            checkLargeRange = false;
            darkMonsterBehaviorSwitch = 1;
        }
        else
        {
            despawn = true;
            RunAwayFunction();
        }
    }


    private IEnumerator RunAwayTime()
    {

        yield return new WaitForSecondsRealtime(runAwayTime);
        agent.speed = 6f;
        darkMonsterBehaviorSwitch = 1;
        playerBlock.SetActive(false);
        runAway = false;
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "FlashArea")
        {
            darkMonsterBehaviorSwitch = 4;
            float distance = Vector3.Distance(playerPosition.position, transform.position) + 1;
            agent.speed = 9;
            runAwayTime = 1 / distance * 50;
            playerBlock.SetActive(true);
            StartCoroutine(RunAwayTime());
        }

        if (other.tag == "LargePlayerRange")
        {
            if (checkLargeRange == true)
            {
                darkMonsterBehaviorSwitch = 3;
            }
        }

        if (other.tag == "PlayerRange")
        {
            darkMonsterBehaviorSwitch = 2;
        }

        if(other.tag == "PlayerHitbox")
        {
            playerAttacked = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "LargePlayerRange")
        {
            if (checkLargeRange == true)
                if (runAway)
                    darkMonsterBehaviorSwitch = 4;
                else
                    darkMonsterBehaviorSwitch = 1;
        }

        if (other.tag == "PlayerRange")
        {
            if (runAway)
                darkMonsterBehaviorSwitch = 4;
            else
            darkMonsterBehaviorSwitch = 1;
        }
    }
}
