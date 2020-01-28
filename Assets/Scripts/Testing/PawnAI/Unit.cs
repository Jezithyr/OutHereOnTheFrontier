// using System;
// using System.Collections.Generic;
// using UnityEngine;

// [RequireComponent(typeof(Rigidbody))]  // This script MUST have a rigidbody to work. Do not need to NULL check
// [RequireComponent(typeof(Animator))]  // This script MUST have a rigidbody to work. Do not need to NULL check
// public abstract class Unit : MonoBehaviour
// {
//     [SerializeField] private Renderer Rend;

//     protected Rigidbody RB;
//     protected Animator Anim;
    
//     // The player will need to use the Physics components of the model. 
//     private void Awake()
//     {
//         RB = GetComponent<Rigidbody>();
//         Anim = GetComponent<Animator>();

//         UnitAwake();
//     }
//     abstract protected void UnitAwake();
// }
