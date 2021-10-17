using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject pauseMenu;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
    }

    void ShowPauseMenu()
    {

    }

    void QuitGame()
    {
        Debug.Log("Application Quit");
        Application.Quit();
    }
}