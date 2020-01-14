using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugHudScript : MonoBehaviour
{
    [SerializeField] private GameObject hudOBJ;
    [SerializeField] private GameObject metaldisplay;
    [SerializeField] private GameObject wooddisplay;
    [SerializeField] private GameObject alloyDisplay ;


    [SerializeField] private Resource metal;
    [SerializeField] private Resource wood;
    [SerializeField] private Resource alloy;

    [SerializeField] private ResourceModule resourcecontroller ;

    private void Start()
    {
       
    }

    private void Update()
    {
        metaldisplay.GetComponent<TMPro.TextMeshProUGUI>().text =  resourcecontroller.GetStorage[metal] + "/" + resourcecontroller.GetResourceLimit(metal);
       wooddisplay.GetComponent<TMPro.TextMeshProUGUI>().text =  resourcecontroller.GetStorage[wood] + "/" + resourcecontroller.GetResourceLimit(wood);
        //alloyDisplay.GetComponent<TextMesh>().text =  resourcecontroller.GetStorage[alloy] + "/" + resourcecontroller.GetResourceLimit(alloy);



    }



}
