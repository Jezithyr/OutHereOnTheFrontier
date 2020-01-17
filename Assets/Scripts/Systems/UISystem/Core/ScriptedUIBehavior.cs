using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptedUIBehavior : MonoBehaviour
{
    [SerializeField] private ScriptedUI parentScriptedUI;

    [SerializeField] private List<GameObject> hudObjects = new List<GameObject>();

    private Dictionary<string, GameObject> hudElements = new Dictionary<string, GameObject>();

    private void OnEnable()
    {
        hudElements.Clear();
        foreach (GameObject hudObject in hudObjects)
        {
            hudElements.Add(hudObject.name,hudObject);
        }
    }

    public GameObject GetElementByName(string Name)
    {
      return hudElements[Name];
    }

}
