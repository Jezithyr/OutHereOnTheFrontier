using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "JobSystem/Create New Job")]
public class Job : ScriptableObject
{

    [SerializeField] private int JobPriority = 1;
    [SerializeField] private string JobDisplayName;
    [SerializeField] private float InProgressUpdateTickTime = 1;

    //=====================delegates===========================
    public delegate void ActionDelegate(Pawn pawn);

    public delegate byte ConditionDelegate(Pawn pawn);

    [SerializeField] private Action StartAction;
    [SerializeField] private Action InProgressAction;
    [SerializeField] private Action CancelAction;

    [SerializeField] private Condition CheckCondition;
    [SerializeField] private Action CompleteAction;

    //=========================properties=======================
    public int Priority{get =>JobPriority;}
    public string DisplayText{get =>JobDisplayName;}

    public bool HasTickAction{get => !(InProgressAction is null);}


    public ConditionDelegate CheckConditionFunc{get => CheckCondition.CheckStatement;}
    public ActionDelegate StartActionFunc{get =>StartAction.ActionFunction;}
    public ActionDelegate InProgressActionFunc{get =>InProgressAction.ActionFunction;}
    public ActionDelegate CancelActionFunc{get =>CancelAction.ActionFunction;}
    public ActionDelegate CompleteActionFunc{get =>CompleteAction.ActionFunction;}
}
