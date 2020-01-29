using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "UISystem/UI/Create New Gameover Screen")]
public class GameOver : ScriptedUI
{

    public void EndGame()
    {
        Application.Quit();
    }

}
