using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "GameFramework/SubSystems/PawnModule")]
public class PawnModule : Module
{

    [SerializeField] private JobModule linkedJobModule;

    private List<Pawn> activePawns = new List<Pawn>();

    private List<Pawn> inActivePawns = new List<Pawn>();

    private List<Pawn> pawnList = new List<Pawn>();


    public void AddPawn(Pawn newPawn)
    {
        if (pawnList.Contains(newPawn)) return; //stop the possibility of duplicates
        pawnList.Add(newPawn);
        inActivePawns.Add(newPawn);
    }

    private void SchedulePawn(Pawn targetPawn, Job newJob) 
    {
        if (activePawns.Contains(targetPawn)) return; //prevent scheduling a pawn to multiple tasks
        if (!targetPawn.startNewJob(newJob)) return; //sets the pawns job, if the pawn already has a job exit without overriding
        inActivePawns.Remove(targetPawn);
        activePawns.Add(targetPawn);
    }

    private void AssignPawns()
    {
        foreach (Pawn _pawn in inActivePawns)
        {
            foreach (var jobList in linkedJobModule.Jobs)
            {
                if (jobList.Value.Count > 0)
                {
                    SchedulePawn(_pawn,jobList.Value.Dequeue());
                    break;
                }
            }
        }
    }

    public void FreePawn(Pawn targetPawn)
    {
        
        targetPawn.CancelCurrentJob();
        linkedJobModule.AddJob(targetPawn.Job);
        activePawns.Remove(targetPawn);
        inActivePawns.Add(targetPawn);
    }

    private void initalize()
    {
        foreach (Job job in linkedJobModule.PossibleJobs) //initalize job prioritylist
        {
            if (!linkedJobModule.Jobs.ContainsKey(job.Priority))
            {
                linkedJobModule.Jobs.Add(job.Priority,new Queue<Job>());
            }
        }
    }

    private void TickPawns()
    {
        foreach (Pawn pawn in pawnList)
        {
            pawn.RunUpdate();
        }
    }

    private void UpdateTick()
    {
        if (inActivePawns.Count != 0) AssignPawns();
        TickPawns();
    }
}


