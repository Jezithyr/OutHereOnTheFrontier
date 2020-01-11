using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(menuName = "Core/Debug/Create Virtual Pawn")]
public class Pawn : ScriptableObject
{
    private Job CurrentJob;
    private GameObject PawnObject;

    [SerializeField] 
    private PawnModule linkedPawnManager;

    public Job Job{get => CurrentJob;}

    public bool startNewJob(Job newJob)
    {
        if (CurrentJob != null) return false;
        CurrentJob = newJob;
        CurrentJob.StartActionFunc(this);
        return true;
    }    

    public void CancelCurrentJob()
    {
        CurrentJob.CancelActionFunc(this);
        CurrentJob = null; //TODO: add manual cleanup for old jobs
    }

    private void CompleteCurrentJob()
    {
        CurrentJob.CompleteActionFunc(this);
        CurrentJob = null;
    }

    private void CheckJobCondition()
    {
        switch (CurrentJob.CheckConditionFunc(this))
        {
            case 1: 
            { 
                CancelCurrentJob();
                break;
            };
            case 2:
            {
                CompleteCurrentJob();
                break;
            }
        }

    }

    private void UpdateTick()
    {
        if (CurrentJob){//only run the job update and monitoring code if there is a job
            if(CurrentJob.HasTickAction) CurrentJob.InProgressActionFunc(this);
            CheckJobCondition();
        }
    }

    public void RunUpdate() //shell method to access update tick, more checks/functionality will be added later
    {
        UpdateTick();
    }
}
