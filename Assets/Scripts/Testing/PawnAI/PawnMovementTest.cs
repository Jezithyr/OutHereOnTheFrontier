using System;
using UnityEngine;
using UnityEngine.AI;

public class PawnMovementTest : MonoBehaviour
{
    [SerializeField]
    private Transform waypoint1; 
    [SerializeField]
    private Transform waypoint2;

    Transform destination;

    NavMeshAgent navMeshAgent;
    private bool hasArrivedAtFirstWay = false;

    void Start()
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        SetDestination();

    }

    private void SetDestination()
    {
        if (destination != null)
        {
        Vector3 targetVector = destination.transform.position;
        navMeshAgent.SetDestination(targetVector);
        }
    }

    private void Update()
    {
        if(Time.frameCount%30 == 0)//every second
        {
        if(Vector3.Distance(waypoint1.position,gameObject.transform.position) < 5.0)
        {
            hasArrivedAtFirstWay = true;
            if (hasArrivedAtFirstWay == true)
            {
            destination = waypoint2;
            SetDestination();
            }
        }

        if(Vector3.Distance(waypoint2.position,gameObject.transform.position) > 5.0)
        {
            hasArrivedAtFirstWay = false;
            if (hasArrivedAtFirstWay == false)
            {
            destination = waypoint1;
            SetDestination();
            }
        }
        }
    }
} 
