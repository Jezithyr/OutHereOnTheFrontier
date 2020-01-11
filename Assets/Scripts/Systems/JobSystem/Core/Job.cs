using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "JobSystem/Create New Job")]
public class Job : ScriptableObject
{

    [SerializeField] private int JobPriority = 1;                           // Allow the designer to modify the Priority Levels of a job
    [SerializeField] private string JobDisplayName;                         // Allow the designer to name the specified job
    [SerializeField] private float InProgressUpdateTickTime = 1;            // Allow the designer to modify how long the pawn is going to commit the the job

    //=====================delegates===========================
    public delegate void ActionDelegate(Pawn pawn);

    public delegate byte ConditionDelegate(Pawn pawn);

    // The Designer will want to manipulate and trigger the states of the pawn for more control.
    [SerializeField] private Action StartAction;                            
    [SerializeField] private Action InProgressAction;                       
    [SerializeField] private Action CancelAction;

    // The Designer will want to be able to the progress of the job, and when it will want to complete the action.
    [SerializeField] private Condition CheckCondition;
    [SerializeField] private Action CompleteAction;

    //=========================properties=======================
    public int Priority{get =>JobPriority;}                     // Retrieve the JobPriority var of the selected job
    public string DisplayText{get =>JobDisplayName;}            // Retrieve the name of the job

    public bool HasTickAction{get => !(InProgressAction is null);}  // Make a check if the progress of the action is not null. (True or False)

    private void Awake()
    {
        if (!CheckCondition) throw new Exception("Error! Job" +this+ " Has no end condition!");     // If we cannot find an end condition of the job, throw an exception.
        
        CheckCondition = ScriptableObject.Instantiate(CheckCondition); //make an instance of the condition object to allow for it to store instanced variables
    }

    //=====================Pawn Triggers==========================
    // The pawns will need to access the action.cs class to commit the actions the player gives them.
    public ConditionDelegate CheckConditionFunc{get => CheckCondition.CheckStatement;}
    public ActionDelegate StartActionFunc{get =>StartAction.ActionFunction;}
    public ActionDelegate InProgressActionFunc{get =>InProgressAction.ActionFunction;}
    public ActionDelegate CancelActionFunc{get =>CancelAction.ActionFunction;}
    public ActionDelegate CompleteActionFunc{get =>CompleteAction.ActionFunction;}
}
