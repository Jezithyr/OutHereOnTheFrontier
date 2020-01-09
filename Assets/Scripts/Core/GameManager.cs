using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : ScriptableObject
{

    [SerializeField] private ConstructionManager ConstructionController ;
    public ConstructionManager ConstructionSubSystem{get => ConstructionController;}

    [SerializeField] private JobManager JobController;
    public JobManager JobSubSystem{get => JobController;}

    [SerializeField] private PawnManager pawnManager;
    public PawnManager PawnSubSystem{get => pawnManager;}

    [SerializeField] private CameraManager cameraManager;
    public CameraManager CameraSubSystem{get => cameraManager;}

    [SerializeField] private GridSystem ActiveGrid;
    public GridSystem Grid{get => ActiveGrid;}
}
