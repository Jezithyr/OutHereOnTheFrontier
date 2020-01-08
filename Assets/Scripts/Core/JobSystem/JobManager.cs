using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobManager : ScriptableObject
{
    [SerializeField] 
    public List<Job> PossibleJobs;

    private SortedDictionary<int,Queue<Job>> ActiveJobs = new SortedDictionary<int,Queue<Job>>();

    public SortedDictionary<int,Queue<Job>> Jobs {get => ActiveJobs;}


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

}
