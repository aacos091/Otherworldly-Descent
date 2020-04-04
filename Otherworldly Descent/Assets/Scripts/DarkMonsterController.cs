using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine;

public class DarkMonsterController : MonoBehaviour
{

    public Transform[] runAwayPoints;
    private int currentControlPointIndex = 0;
    public Transform playerPosition;
    private NavMeshAgent agent;
    private float runAwayTime;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = playerPosition.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FollowPlayer()
    {
        GetComponent<NavMeshAgent>().stoppingDistance = 5;
    }

    void SpookySound()
    {

    }

    void FollowPlayerAtDistance()
    {
        GetComponent<NavMeshAgent>().stoppingDistance = 15;
    }

    void AttackPlayer()
    {

    }

    void RunAway()
    {
        agent.destination = runAwayPoints[currentControlPointIndex].position;
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            if (runAwayPoints.Length > 0)
            {
                agent.destination = runAwayPoints[currentControlPointIndex].position;

                currentControlPointIndex++;
                currentControlPointIndex %= runAwayPoints.Length;
            }
        }
    }

    private IEnumerator RunAwayTime()
    {



        yield return new WaitForSecondsRealtime(runAwayTime);
        GetComponent<NavMeshAgent>().speed = 3.5f;
        agent.destination = playerPosition.position;

    }


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "FlashArea")
        {
            float distance = Vector3.Distance(playerPosition.position, transform.position) + 1;
            GetComponent<NavMeshAgent>().speed = 6;
            runAwayTime = 1 / distance * 50;
            RunAway();
            StartCoroutine(RunAwayTime());
        }
    }
}
