using System;
using UnityEngine;
using UnityEngine.AI;

public class PawnMove : MonoBehaviour
{

    [SerializeField] Transform destination;

    NavMeshAgent navMeshAgent;


    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
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
}
