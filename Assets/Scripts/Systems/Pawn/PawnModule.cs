using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

struct NavAgent
{
    public NavMeshAgent agent;
    public int currentPatrolIndex;
    public bool isTravelling;
    public bool isWaiting;
    public bool isPatrollingForward;
    public float waitTimer;

    public void SetPatrolIndex(int cpi)
    {
        currentPatrolIndex = cpi;
    }

    public void SetTraveling(bool newTraveling)
    {
        isTravelling = newTraveling;
    }

     public void SetWaiting(bool newWait)
    {
        isWaiting = newWait;
    }

     public void SetPF(bool newPf)
    {
        isPatrollingForward = newPf;
    }

     public void SetWait(float newTimer)
    {
        waitTimer = newTimer;
    }

    public NavAgent(NavMeshAgent a, int p, bool t, bool w, bool f,float wt)
    {
        agent = a;
        currentPatrolIndex = p;
        isTravelling = t;
        isWaiting = w;
        isPatrollingForward = f;
        waitTimer = wt;
    }
}


[CreateAssetMenu(menuName = "GameFramework/SubSystems/PawnModule")]
public class PawnModule : Module
{
    // True False parameter to determine whether the pawn will wait at the current waypoint
    [SerializeField] bool patrolWaitng = true;
    
    // The time the pawn will wait at the waypoint if patrolWaiting = true
    [Header("How long do you want it to wait")]
    [SerializeField] private float patrolWaitTime = 3.0f;

    // The probability of the pawn switching it's direction
    [Header("Adjust how random you want the patrol")]
    [Range(0.0f, 1f)]
    [SerializeField] private float switchChance = 0.2f;

    // The list of potential patrolPoints the pawn will cover
    [Header("Spawn in waypoints, and drag them all into this list")]
    [SerializeField] List<PawnPatrolWaypoint> startingPatrolPoints = new List<PawnPatrolWaypoint>();

    private List<NavAgent> Agents = new List<NavAgent>();

    private List<GameObject> NPCObjs = new List<GameObject>();

    private List<Vector3> spawnPositions = new List<Vector3>();

    private List<PawnPatrolWaypoint> masterPatrolList = new List<PawnPatrolWaypoint>();

    private void initalize()
    {
        Reset();
        foreach (var item in startingPatrolPoints)
        {
            masterPatrolList.Add(item);
        }        
    }

    private void TickPawns()
    {
        for (int i = 0; i < Agents.Count; i++)
        {
            if (Agents[i].isTravelling && Agents[i].agent.remainingDistance <= 1.0f)
            {
                Agents[i].SetTraveling(false);
                if (patrolWaitng)
                {
                    Agents[i].SetWaiting(true);
                    Agents[i].SetWait(0f);
                }
                else
                {
                    ChangePatrolPoint( Agents[i]);
                    SetDestination(Agents[i]);
                }
            }   
            if (Agents[i].isWaiting)
            {
                Agents[i].SetWait(Agents[i].waitTimer + Time.deltaTime);
                if (Agents[i].waitTimer >= patrolWaitTime)
                {
                Agents[i].SetWaiting(false);

                ChangePatrolPoint( Agents[i]);
                SetDestination(Agents[i]);
                }
            }
        }
    }


    private void SetDestination(NavAgent agent)
    {
        if (masterPatrolList != null)
        {
            Vector3 targetVector = masterPatrolList[agent.currentPatrolIndex].transform.position;
            agent.agent.SetDestination(targetVector);
            agent.SetTraveling(true);
        }
    }

    private void ChangePatrolPoint(NavAgent agent)
    {
        if(UnityEngine.Random.Range(0f,1f) <= switchChance)
        {
           agent.SetPF(! agent.isPatrollingForward);
        }
        if (agent.isPatrollingForward)
        {
            agent.SetPatrolIndex((agent.currentPatrolIndex + 1) % masterPatrolList.Count);
        }
        else
        {
            agent.SetPatrolIndex(agent.currentPatrolIndex - 1);
            if (agent.currentPatrolIndex < 0)
            {
                agent.SetPatrolIndex(masterPatrolList.Count - 1) ;
            }
        }
    }

    private void UpdateTick()
    {
        TickPawns();
    }

    public override void Reset()
    {
        masterPatrolList.Clear();
        Agents.Clear();
        spawnPositions.Clear();
        NPCObjs.Clear();
    }
}


