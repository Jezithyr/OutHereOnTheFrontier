using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameFramework/Debug/DummyModule")]
public class DummyModule : Module
{
    public override void Initialize()
    {
        Debug.Log("DUMMYMODULE: Dummy module init");
    }

    public override void Reset()
    {
        
    }

    public override void Update()
    {
        Debug.Log("DUMMYMODULE: Dummy module tick");
    }
}
