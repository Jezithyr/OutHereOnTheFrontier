using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "GameFramework/Gamestate/MainMenu")]
public class MenuState : GameState
{
    [SerializeField]
    GameManager Game;

    [SerializeField] private GameState playState;

    [SerializeField] private ScriptedCamera menuCamData;

    [SerializeField] private CameraModule camModule;

    [SerializeField] private UIModule uiModule;

    [SerializeField] private MainMenu mainMenuUi;


    private int menuUIid;
    private ScriptedCamera MenuCam;
    
    public override void OnActivate(GameState lastState)
    {

        Debug.Log("Loaded Main Menu");        
        Game.Pause();

        MenuCam = camModule.AddScriptedCameraInstance(menuCamData);
        menuUIid = uiModule.CreateInstance(mainMenuUi);
        uiModule.Show(mainMenuUi,menuUIid);
    }

    public override void OnUpdate()
    {
        
    }

    public override void OnDeactivate(GameState newState)
    {

        camModule.Reset();
        uiModule.DestroyInstance(mainMenuUi,menuUIid);
        //if (newState == playState)
        //{
            
          //SceneManager.UnloadSceneAsync(sceneName:"MainMenu");
        //}
        Game.UnPause();
        SceneManager.LoadScene(sceneName:"Main");
    }

    public override void Reset()
    {
        
    }
}
