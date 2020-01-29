using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "UISystem/UI/Create New Debug Menu")]
public class DebugMenu : ScriptedUI
{
    
    [SerializeField] private List<Resource> resourceList = new List<Resource>();

    [SerializeField] private ResourceModule resourceManager;

    [SerializeField] private PlayingState playstate;

    [SerializeField] private EventModule eventManager;

    [SerializeField] private int popResourceID = 7;


    public void setGameTimer(int newtime)
    {
        playstate.GameTimer = newtime;
    }

    public void addGameTimer(int newtime)
    {
        playstate.GameTimer -= newtime;
    }





    public void increasePopCap(int addCap)
    {
        resourceManager.SetResourceLimit(resourceList[popResourceID],resourceManager.GetResourceLimit(resourceList[popResourceID])+ addCap);
    }

    public void removePopCap(int remCap)
    {
        resourceManager.SetResourceLimit(resourceList[popResourceID],resourceManager.GetResourceLimit(resourceList[popResourceID])- remCap);
    }

    public void setPopCap(int newCap)
    {
        resourceManager.SetResourceLimit(resourceList[popResourceID],newCap);
    }

    public void addPop(int amount)
    {
        resourceManager.AddResource(resourceList[popResourceID],amount);
    }

    public void setPop(int amount)
    {
        resourceManager.SetResource(resourceList[popResourceID],amount);
    }

    public void removePop(int amount)
    {
        resourceManager.RemoveResource(resourceList[popResourceID],amount);
    }

    public void triggerEvent(int eventIndex)
    {
        eventManager.showEvent(eventIndex);
    }


    private void addResource(Resource resource, int amount)
    {
        resourceManager.AddResource(resource,amount);
    }

    public void addResource100(int index)
    {
        addResource(resourceList[index],100);
    }

    public void addResource300(int index)
    {
        addResource(resourceList[index],300);
    }

    public void addResource500(int index)
    {
        addResource(resourceList[index],500);
    }

    public void addResourceMax(int index)
    {
        addResource(resourceList[index],999999999); //lol
    }

}
