// using System;
// using System.Collections;
// using UnityEngine;
// using UnityEngine.AI;

// [RequireComponent(typeof(NavMeshAgent))]

// public class AIController : Unit
// {
    

//     private NavMeshAgent Agent;
//     private IEnumerator CurrentState;
//     private Outpost TargetOutpost;

//     protected override void UnitAwake()
//     {
//         Agent = GetComponent<NavMeshAgent>();
//         RB.isKinematic = true;
//     }

//     private void Update()
//     {
//         Anim.SetFloat("Vertical", Agent.velocity.magnitude);  // Handle the animations.
//     }

//     private void SetState(IEnumerator newState)
//     {
//         if(CurrentState != null)
//         {
//             StopCoroutine(CurrentState);
//         }

//         CurrentState = newState;
//         StartCoroutine(CurrentState);
//     }

//     private void OnEnable()
//     {
//         SetState(State_Idle());
//     }

//     private IEnumerator State_Idle()
//     {
//         while(TargetOutpost == null)
//         {
//             yield return null;
//             TargetOutpost = Outpost.OutpostList.GetRandomItem();   // get a random outpost
//         }
//         SetState(State_MovingToOutpost());

//     }

//     private IEnumerator State_MovingToOutpost()
//     {
//         Agent.SetDestination(TargetOutpost.transform.position);
//         while (Agent.remainingDistance > Agent.stoppingDistance)
//         {
//             yield return null;    
//         }
//         // Get a new random outpost
//         TargetOutpost = null;
//         SetState(State_Idle());
//     }
// }
