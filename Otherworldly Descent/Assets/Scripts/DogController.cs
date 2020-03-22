using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine;

public class DogController : MonoBehaviour
{/*
    public GameObject pauseMenuUI;
    public Text downloadText;
    public Transform[] patrolPoints;
    public Transform[] patrolPointsOffice;
    public AudioClip playerFoundSound;
    public AudioSource sniffMeterSource;
    public AudioSource playerFoundSource;
    public static DogController instance;
    public Transform fallenItem;
    public Transform playerPosition;
    public Image content;

    private NavMeshAgent agent;
    private Vector3 lastPlayerPosition;

    public int sniffDelay;
    public int investagateTime;
    private int sniffMeter;
    private int sniffMeterDistance;
    private int sniffMeterHeight;
    private int sniffMeterSpeed;
    private int currentControlPointIndex = 0;

    public float waitTime;
    public float startWaitTime;

    private float progression;
    private bool playerMovement;
    private bool hit;

    public bool goToOffice;










    // Start is called before the first frame update
    void Start()
    {


        sniffMeter = Mathf.Clamp(sniffMeter, 0, 100);
        sniffMeterSource = GameObject.Find("Sniff meter source").GetComponent<AudioSource>();
        sniffMeterSource.pitch = 0.4f;
        sniffMeterSource.Play();

        instance = this;

        agent = GetComponent<NavMeshAgent>();

        startWaitTime = Time.time;


    }

    // Update is called once per frame
    void Update()
    {




        if (sniffMeter <= 40)
        {
            sniffMeterSource.pitch = 0.4f;
        }
        else
        {
            sniffMeterSource.pitch = (sniffMeter * 0.01f);
        }
        PlayerMovement();
        SetDistance();
        SetSpeed();
        SetHeight();

        //Sniff meter functions
        if (sniffMeter >= 100)
        {
            downloadText.text = "The dog found you! The mission is lost!";
            GameObject.Find("GameController").GetComponent<GameController>().PlayerLoses();
        }

        if (sniffMeter >= 50)
        {
            MoveTowardsPlayer();
        }
        else
        {
            Patrol();
        }

        SniffMeterUI();

    }


    //Moves towards the player
    public void MoveTowardsPlayer()
    {

        agent.destination = playerPosition.position;
    }



    //The dog roams around a set path
    void Patrol()
    {
        if (goToOffice)
        {


            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                if (patrolPointsOffice.Length > 0)
                {
                    agent.destination = patrolPointsOffice[currentControlPointIndex].position;

                    currentControlPointIndex++;
                    //sniffMeter++;
                    currentControlPointIndex %= patrolPointsOffice.Length;
                }


            }
        }
        else
        {
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                if (patrolPoints.Length > 0)
                {
                    agent.destination = patrolPoints[currentControlPointIndex].position;

                    currentControlPointIndex++;
                    //sniffMeter++;
                    currentControlPointIndex %= patrolPoints.Length;
                }


            }
        }

    }

    // When the player is moving, every second the sniff meter will go down by an amount
    public void DecreaseSniffMeter()
    {
        sniffMeter = sniffMeter - 10;
        if (sniffMeter <= -1)
        {
            sniffMeter = 0;
        }

    }




    public void PlayerMovement()
    {
        if (Time.time > startWaitTime + sniffDelay)
        {
            progression = Vector3.Distance(lastPlayerPosition, playerPosition.position);
            lastPlayerPosition = playerPosition.position;

            if (progression > 3)
            {
                DecreaseSniffMeter();
                playerMovement = true;
            }
            else
            {
                IncreaseSniffMeter();
                playerMovement = false;
            }
            startWaitTime = Time.time;
        }





    }

    void SetHeight()
    {
        // Gets the player's Y position.
        float height = playerPosition.position.y;

        // Sets the players height to a value for the sniff meter. Numbers will need to be changed.
        if (height >= 3)
        {
            sniffMeterHeight = 1;
        }

        if (height >= 6)
        {
            sniffMeterHeight = 2;
        }

        else
        {
            sniffMeterHeight = 3;
        }

    }


    void SetDistance()
    {
        //Check distance between dog and player
        float distance = Vector3.Distance(playerPosition.position, transform.position);

        // Set the value for the distance mechanic for the sniff meter. Numbers will need to be changed.
        if (distance >= 3)
        {
            sniffMeterDistance = 2;
        }

        if (distance >= 6)
        {
            sniffMeterDistance = 3;
        }

        else
        {
            sniffMeterDistance = 4;
        }
    }


    void SetSpeed()
    {
        // Sets the players height to a value for the sniff meter. Numbers will need to be changed.
        if (progression >= 3)
        {
            sniffMeterSpeed = 5;
        }

        if (progression >= 6)
        {
            sniffMeterSpeed = 1;
        }

        else
        {
            sniffMeterSpeed = 10;
        }
    }

    public void IncreaseSniffMeter()
    {


        sniffMeter = (sniffMeter + (1 * (sniffMeterDistance + sniffMeterHeight + sniffMeterSpeed)));
        if (sniffMeter >= 101)
        {
            sniffMeter = 100;
        }

    }

    public void HitByItem()
    {
        hit = true;
        downloadText.text = "You hit the dog!";
        if (hit)
        {
            sniffMeter = 0;
            startWaitTime = Time.time;
            if (Time.time > startWaitTime + waitTime)
            {
                hit = false;
            }
        }
    }

    public void Investagate()
    {
        if (sniffMeter < 50000)
        {
            agent.destination = fallenItem.position;
        }
    }

    void SniffMeterUI()
    {
        content.fillAmount = (sniffMeter * 0.01f);
    }*/
}
