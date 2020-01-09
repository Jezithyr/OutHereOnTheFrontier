using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class JobManager : Module
{

    [SerializeField] 
    // There needs to be a list of possible jobs that the pawn can part-take in. 
    public List<Job> PossibleJobs;
    // In order to manage the jobs, there needs to be a list that has to contain more than one command.
    private SortedDictionary<int,Queue<Job>> ActiveJobs = new SortedDictionary<int,Queue<Job>>();
    // The list will need to pull from the list of jobs available.
    public SortedDictionary<int,Queue<Job>> Jobs {get => ActiveJobs;}

    public override void Initialize()
    {
        
    }


    // The player has to be able to add jobs to our pawns, allowing them to take on certain actions.
    public void AddJob(Job jobToAdd)
    {
        ActiveJobs[jobToAdd.Priority].Enqueue(jobToAdd);         // Adds the user inputted job into a queue, and adds it to the end of the list.  
    }
    
    public void AddJob(int priority, Job jobToAdd)
    {
        ActiveJobs[priority].Enqueue(jobToAdd);                 // depending on what the priority level of the job is, it will be placed into it's respective spot within the queue.
    }

    // There needs to be a way to remove jobs from the list once it has been completed.
    public void RemoveJob(Job jobToRemove)
    {
        if (!ActiveJobs[jobToRemove.Priority].Contains(jobToRemove)) return;    //exit if the job is not present (Quick Exit).
        List<Job> tempList =  new List<Job>(ActiveJobs[jobToRemove.Priority]);  // Create a temp reference to hold the desired job when completed.
        tempList.Remove(jobToRemove);                                           // remove the selected job.
        ActiveJobs[jobToRemove.Priority] = new Queue<Job>(tempList);            // point to the next job to remove within the list. 
    }

}
