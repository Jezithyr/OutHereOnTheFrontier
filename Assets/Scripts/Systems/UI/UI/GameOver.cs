using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[CreateAssetMenu(menuName = "UISystem/UI/Create New Gameover Screen")]
public class GameOver : ScriptedUI
{

    public void ChangeGameOverReason(string text)
    {
        linkedUI.GetElementByName("ReasonText").GetComponent<TextMeshProUGUI>().SetText(text);
    }

    public void HideLoseText()
    {
        linkedUI.GetElementByName("LoseText").GetComponent<TextMeshProUGUI>().enabled = false;
    }

    public void EndGame()
    {
        Application.Quit();
    }

}
