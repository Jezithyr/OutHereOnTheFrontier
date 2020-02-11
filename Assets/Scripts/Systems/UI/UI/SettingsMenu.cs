using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "UISystem/UI/Create New Settings Menu")]
public class SettingsMenu : ScriptedUI
{
    [SerializeField] private GameManager game;

    [SerializeField] private UIModule uiModule;

    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private MainMenu mainMenu;



    public void ReturnToPause(int instanceID)
    {
        ToggleUI(instanceID,false);
        uiModule.Show(pauseMenu,0);
    }

    public void ReturnToMain(int instanceID)
    {
        ToggleUI(instanceID,false);
        uiModule.Show(mainMenu,0);
    }

}
