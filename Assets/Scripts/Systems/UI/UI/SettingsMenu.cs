using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "UISystem/UI/Create New Settings Menu")]
public class SettingsMenu : ScriptedUI
{
    [SerializeField] private GameManager game;

    [SerializeField] private UIModule uiModule;

    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private MainMenu mainMenu;

    [SerializeField] private Color darkestBrightness;
    [SerializeField] private Color lightestBrightness;


    private float newGamma = 1;
    private float newBrightness = 1;
    private float newVolume = 1;

    protected override void Initialize()
    {
        //discardChanges();
    }

    public void applyChanges()
    {
      //  SetNewGamma(newGamma);
        SetNewBrightness(newBrightness);
        SetNewVolume(newVolume);
    }

    public void discardChanges()
    {
       // SetNewGamma(0.5f);
        SetNewBrightness(1f);
        SetNewVolume(1);
    }

    private void SetNewGamma(float newGamma)
    {
        
    }

    private void SetNewBrightness(float newBrightness)
    {
        GameObject mainLight = GameObject.FindGameObjectWithTag("MainLight");
        if (mainLight == null) return;
        mainLight.GetComponent<Light>().color = Color.Lerp(darkestBrightness,lightestBrightness,newBrightness);
    }

    private void SetNewVolume(float volume)
    {
        AudioListener.volume = volume;
    }    


    public void sliderSetVolume()
    {
        if (!started) return;
        newVolume = linkedUI.GetElementByName("VolumeSlider").GetComponentInChildren<Slider>().value;
        Debug.Log("New Volume = "+ newVolume);
    }

    public void sliderSetGamma()
    {
        if (!started) return;
        newGamma = linkedUI.GetElementByName("GammaSlider").GetComponentInChildren<Slider>().value;
    }

    public void sliderSetBrightness()
    {
        if (!started) return;
        newBrightness = linkedUI.GetElementByName("BrightnessSlider").GetComponentInChildren<Slider>().value;
    }




    public void ReturnToPause(int instanceID)
    {
        applyChanges();
        ToggleUI(instanceID,false);
        uiModule.Show(pauseMenu,0);
    }

    public void ReturnToMain(int instanceID)
    {
        applyChanges();
        ToggleUI(instanceID,false);
        uiModule.Show(mainMenu,0);
    }

}
