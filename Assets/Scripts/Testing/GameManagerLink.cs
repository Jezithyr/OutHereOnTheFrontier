using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerLink : MonoBehaviour
{
    [SerializeField] private GameManager gameManager ;


    private void Start()
    {

        Debug.Log("Starting GameManager "+gameManager + "\n");
        gameManager.Start();
    }

}
