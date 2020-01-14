using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "GameFramework/SubSystems/UIModule")]
public class UIModule : Module
{
    [SerializeField] 
    private List<GameObject> userInterfaces = new List<GameObject>();

    private Dictionary<string,Canvas> UIList = new Dictionary<string, Canvas>();


    private void OnEnable()
    {

    }



    public void EnableElement(string elementName)
    {
    }

    public void DisableElement(string elementName)
    {
    }






}
