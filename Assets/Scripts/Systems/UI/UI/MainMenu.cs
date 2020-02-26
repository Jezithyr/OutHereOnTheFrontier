using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "UISystem/UI/Create New Main Menu")]
public class MainMenu : ScriptedUI
{
    [SerializeField]
    GameManager Game;
    [SerializeField] private UIModule uiModule;

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


    public void Show()
    {
        uiModule.Show(this,0);
    }

    public void Hide()
    {
        uiModule.Hide(this,0);
    }


    public void StartGame()
    {
        // Call the sound here.
        Game.SwitchSystemGameState(1);
        Debug.Log("GAME STARTED");
    }
}
