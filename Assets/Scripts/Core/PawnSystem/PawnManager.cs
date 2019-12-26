using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Core/Systems/PawnSystem/Create Pawn Manager")]
public class PawnManager : SingletonScriptableObject<PawnManager>
{
    private List<Pawn> activePawns = new List<Pawn>();

    [SerializeField] private List<Job> PossibleJobs;


    private List<Pawn> inActivePawns = new List<Pawn>();

    private List<Pawn> pawnList = new List<Pawn>();

    private SortedDictionary<int,Queue<Job>> ActiveJobs = new SortedDictionary<int,Queue<Job>>();

    public void AddJob(Job jobToAdd)
    {
        ActiveJobs[jobToAdd.Priority].Enqueue(jobToAdd);
    }
    
    public void AddJob(int priority, Job jobToAdd)
    {
        ActiveJobs[priority].Enqueue(jobToAdd);
    }

    public void RemoveJob(Job jobToRemove)
    {
        if (!ActiveJobs[jobToRemove.Priority].Contains(jobToRemove)) return; //exit if the job is not present
        List<Job> tempList =  new List<Job>(ActiveJobs[jobToRemove.Priority]);
        tempList.Remove(jobToRemove);
        ActiveJobs[jobToRemove.Priority] = new Queue<Job>(tempList);
    }

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
            foreach (var jobList in ActiveJobs)
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
        
        targetPawn.cancelCurrentJob();
        AddJob(targetPawn.Job);
        activePawns.Remove(targetPawn);
        inActivePawns.Add(targetPawn);
    }

    private void initalize()
    {
        foreach (Job job in PossibleJobs) //initalize job prioritylist
        {
            if (!ActiveJobs.ContainsKey(job.Priority))
            {
                ActiveJobs.Add(job.Priority,new Queue<Job>());
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


