using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(menuName = "JobSystem/Conditions/Create New Timer Condition")]
public class TimerCondition : Condition
{
    [SerializeField] private int howManyLoops = 10;
    private int currentLoop = 0;
    public override byte CheckStatement(Pawn pawn)
    {
        if (currentLoop >= howManyLoops) return 2;//successfully met condition
        currentLoop++;
        return 0; //continue, condition not met
    }
}
