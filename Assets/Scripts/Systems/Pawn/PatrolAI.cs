using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class PatrolAI : MonoBehaviour
{
    // True False parameter to determine whether the pawn will wait at the current waypoint
    [SerializeField] bool patrolWaitng;
    
    // The time the pawn will wait at the waypoint if patrolWaiting = true
    [Header("How long do you want it to wait")]
    [SerializeField] private float patrolWaitTime = 3.0f;

    // The probability of the pawn switching it's direction
    [Header("Adjust how random you want the patrol")]
    [Range(0.0f, 1f)]
    [SerializeField] private float switchChance = 0.2f;

    // The list of potential patrolPoints the pawn will cover
    [Header("Spawn in waypoints, and drag them all into this list")]
    [SerializeField] List<PawnPatrolWaypoint> patrolPoints;

    // ==========================================================
    // Private variables will be kept here

    NavMeshAgent navMeshAgent;
    int currentPatrolIndex;
    bool isTravelling;
    bool isWaiting;
    bool isPatrollingForward;
    float waitTimer;

    // =========================================================

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        if(patrolPoints != null && patrolPoints.Count >= 2)
        {
            currentPatrolIndex = 0;
            SetDestination();
        }
    }

    private void Update()
    {
        if (isTravelling && navMeshAgent.remainingDistance <= 1.0f)
        {
            isTravelling = false;

            if (patrolWaitng)
            {
                isWaiting = true;
                waitTimer = 0f;
            }
            else
            {
                ChangePatrolPoint();
                SetDestination();
            }
        }   

        if (isWaiting)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer >= patrolWaitTime)
            {
                isWaiting = false;

                ChangePatrolPoint();
                SetDestination();
            }
        }
    }



    // TODO: Implement the call for a new destination to patrol to
    private void SetDestination()
    {
        if (patrolPoints != null)
        {
            Vector3 targetVector = patrolPoints[currentPatrolIndex].transform.position;
            navMeshAgent.SetDestination(targetVector);
            isTravelling = true;
        }
    }

    private void ChangePatrolPoint()
    {
        if(UnityEngine.Random.Range(0f,1f) <= switchChance)
        {
            isPatrollingForward = !isPatrollingForward;
        }
        
        if (isPatrollingForward)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Count;
        }
        else
        {
            if (--currentPatrolIndex < 0)
            {
                currentPatrolIndex = patrolPoints.Count - 1;
            }
        }
    }
}
