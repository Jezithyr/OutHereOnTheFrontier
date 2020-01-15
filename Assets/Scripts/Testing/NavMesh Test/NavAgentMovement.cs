// This is a test script to get the Agent to move along the mesh path

using UnityEngine;
using UnityEngine.AI;

public class NavAgentMovement : MonoBehaviour
{
    public Transform goal;

    private void Start()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.destination = goal.position;
    }
}
