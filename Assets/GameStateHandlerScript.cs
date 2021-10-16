using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateHandlerScript : MonoBehaviour
{
    public int numberOfSieges = 3;
    int currentTurn;
    public float numberOfEnemies;

    public GameObject Timer;
    public GameObject SiegeText;
    public GameObject CalmText;
    public GameObject YouLoseText;
    public GameObject YouWinText;

    //EnemySpawners
    public GameObject North;
    public GameObject South;
    public GameObject West;
    public GameObject East;


    void Start()
    {
        // This is our first time
        currentTurn = 1;
        StartCalmTimes();
    }

    // Update is called once per frame
    void Update()
    {
        numberOfEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if (Timer.GetComponent<CountdownTimer>().timerTime <= 0)
        {
            GameEndLose();
        }
    }

    public void StartCalmTimes()
    {
        // If this is our first time?

            // Add time to the timer
            // Decide on where the enemies should come from next
            // Start Siege after a certain condition... X amount of time passed? X amount of music played?
        
    }

    public void EndCalmTimes()
    {

    }

    public void StartSiege()
    {
        // Spawn enemies
        // Display "Siege XX Text"
        // When all enemies are dead we call EndSiege();
    }

    public void EndSiege()
    {
        currentTurn++;
        if (currentTurn >= numberOfSieges)
        {
            GameEndWin();
        }
        else
        {
            StartCalmTimes();
        }
    }

    public void GameEndWin()
    {
        // You won! :D
    }

    public void GameEndLose()
    {
        // You lose! :(
        Time.timeScale = 0f;
        YouLoseText.SetActive(true);
        Debug.Log("You Lose");
    }
}
