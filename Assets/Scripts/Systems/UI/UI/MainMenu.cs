using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "UISystem/UI/Create New Main Menu")]
public class MainMenu : ScriptedUI
{
    [SerializeField]
    GameManager Game;

    private void OnEnable()
    {
        ClearBehaviors(); //this needs to be present in every ui otherwise unity serialization breaks everything
    }
    
    public override void Start()
    {
       
    }

    public override void Update()
    {

    }


    public void StartGame()
    {
        Game.SwitchSystemGameState(1);
        Debug.Log("GAME STARTED");
    }
}
