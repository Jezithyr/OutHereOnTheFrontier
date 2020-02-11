﻿using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;


public enum PlayerHudModes
{
    None,
    Building,
    Demolishing,
    Events
}


[CreateAssetMenu(menuName = "UISystem/UI/Create New PlayerHud")]
public class PlayerHUD : ScriptedUI
{


    [SerializeField] private ResourceModule resourceSystem;

    [SerializeField] private ConstructionModule buildingSystem ;

    [SerializeField] private PlayingState playingState;

    [SerializeField] private CameraModule cameraModule;

    [SerializeField] private UIModule uiModule ;

    [SerializeField] private List<Resource>  ResourceList = new List<Resource>();

    [SerializeField] private List<string> ResourceDisplayNames = new List<string>();

    [SerializeField] private List<Building> buildings = new List<Building>();
    [SerializeField] private LayerMask BuildingLayerMask;

    private List<TextMeshProUGUI> linkedResourceDisplays = new List<TextMeshProUGUI>();

    private GameObject buildingMenuObj = null;


    private PlayerHudModes hudMode = PlayerHudModes.None;
    public PlayerHudModes Mode{get => hudMode;}


    private GameObject previewBuilding  = null;
    private GameObject DemolishExit;
    private TextMeshProUGUI timerDisplay;

    private int previewBuildingIndex = -1;

    private bool showingPreview = false;
    public bool BuildMode{get =>showingPreview;}

    private void OnEnable()
    {
        ClearBehaviors(); //this needs to be present in every ui otherwise unity serialization breaks everything 
        showingPreview = false;

    }

    public override void Start()
    {
        if (linkedResourceDisplays.Count > 0)   linkedResourceDisplays.Clear(); //possible fix for hotreloading
        for (int i = 0; i < ResourceDisplayNames.Count; i++)
        {

            linkedResourceDisplays.Add(linkedUI.GetElementByName(ResourceDisplayNames[i]).GetComponentInChildren<TextMeshProUGUI>());
        }
        buildingMenuObj = linkedUI.GetElementByName("BuildingMenu");
        DemolishExit = linkedUI.GetElementByName("DemolishExit");
        timerDisplay = linkedUI.GetElementByName("TimerDisplay").GetComponentInChildren<TextMeshProUGUI>();
        buildingMenuObj.SetActive(false);
        DemolishExit.SetActive(false);
    }

    public void SetHudMode(int modeIndex)
    {
        SetHudmode((PlayerHudModes)modeIndex);
    }

    public void SetHudmode(PlayerHudModes newMode)
    {
        switch (hudMode)
        {
            case PlayerHudModes.Building:
            {
                HideBuildingMenu();
                break;
            };
            case PlayerHudModes.Demolishing:
            {
                HideDemolishExit();
                break;
            };
            case PlayerHudModes.Events:
            {
                Debug.Log("TODO: HIDE event ui here");
                break;
            };
        }

        switch (newMode)
        {
            case PlayerHudModes.Building:
            {
                ShowBuildingMenu();
                break;
            };
            case PlayerHudModes.Demolishing:
            {
                Debug.Log("Entered demolish mode");
                ShowDemolishExit();
                break;
            };
            case PlayerHudModes.Events:
            {
                Debug.Log("TODO: SHOW event ui here");
                break;
            };
        }

        hudMode = newMode;
    }


    public override void Update()
    {
        
        for (int i = 0; i < linkedResourceDisplays.Count; i++)
        {
            linkedResourceDisplays[i].SetText(Mathf.FloorToInt(resourceSystem.GetResourceStorage(ResourceList[i]))+ "/"+ Mathf.FloorToInt(resourceSystem.GetResourceLimit(ResourceList[i])));
        }
        if (showingPreview)
        {
            previewBuilding.transform.position = uiModule.CursorToWorld(cameraModule.ActiveCameraObject, BuildingLayerMask);
        }
        timerDisplay.SetText(playingState.GameTimer/60 + ":"+ playingState.GameTimer%60);
    }

    public void ShowDemolishExit()
    {
        DemolishExit.SetActive(true);
    }

    public void HideDemolishExit()
    {
        DemolishExit.SetActive(false);
    }

    public void ShowBuildingMenu()
    {
        buildingMenuObj.SetActive(true);
    }

    public void  HideBuildingMenu()
    {
        DestroyPreview();
        buildingMenuObj.SetActive(false);
    }


    public void selectBuilding(int selectionIndex)
    {
        if (showingPreview) DestroyPreview();
        CreateBuildingPreview(selectionIndex);
    }

    public void CreateBuildingPreview(int buildingIndex)
    {
        if (showingPreview) return;
        previewBuilding = buildingSystem.CreatePreviewAtPos(buildings[buildingIndex],uiModule.CursorToWorld(cameraModule.ActiveCameraObject, BuildingLayerMask));
        previewBuildingIndex = buildingIndex;
        showingPreview = true;
    }

    public void DestroyPreview()
    {
        if (previewBuilding == null) return;
        Destroy(previewBuilding);
        previewBuilding = null;
        showingPreview = false;
    }

    public void CreateBuildingFromPreview()
    {
        if (!showingPreview) return;

        if (!buildings[previewBuildingIndex].CheckPlacement(previewBuilding)) return;
        buildingSystem.CreateBuildingAtWorldPos(uiModule.CursorToWorld(cameraModule.ActiveCameraObject, BuildingLayerMask),new Quaternion(),buildings[previewBuildingIndex]);
        DestroyPreview();
    }
}
