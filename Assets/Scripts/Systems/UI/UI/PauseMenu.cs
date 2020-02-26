using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "UISystem/UI/Create New Pause Menu")]
public class PauseMenu : ScriptedUI
{
    [SerializeField] private GameManager game;

    [SerializeField] private UIModule uiModule;

    [SerializeField] private SettingsMenu inGameSettings;

    [SerializeField] private PlayerHUD Playerhud ;


    public void ResumeGame(int instanceID)
    {
        ToggleUI(instanceID,false);
        uiModule.Show(Playerhud,0);
        game.UnPause();
    }

    public void OpenSettings(int instanceID)
    {
        ToggleUI(instanceID,false);
        uiModule.Show(inGameSettings,0);
    }

    public void Show()
    {
        uiModule.Show(this,0);
    }

    public void Hide()
    {
        uiModule.Hide(this,0);
    }


    public void ReturnToMenu()
    {
    }

    public void EndGame()
    {
        Application.Quit();
    }
}
