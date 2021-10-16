using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameStateHandlerScript : MonoBehaviour
{
    public int numberOfSieges = 3;
    int currentTurn;
    public float spawnedNumberOfEnemies;
    public float currentNumberOfEnemies;
    public float percentageOfEnemies;

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

    public GameObject SiegeDrums;

    public bool Calm;


    void Start()
    {
        // This is our first time
        currentTurn = 1;
        StartCalmTimes();
        //StartSiege();
    }

    // Update is called once per frame
    void Update()
    {
        currentNumberOfEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        percentageOfEnemies = (currentNumberOfEnemies / spawnedNumberOfEnemies) * 100f;
        // add null exception

        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("PercentOfEnemiesRemaining", percentageOfEnemies);

        if (currentNumberOfEnemies <= 0)
        {
            EndSiege();
        }
    }

    public void StartCalmTimes()
    {
        // If this is our first time?

        Invoke("StartSiege", 10f);
            // Add time to the timer
            // Decide on where the enemies should come from next
            // Start Siege after a certain condition... X amount of time passed? X amount of music played?
    }

    public void StartSiege()
    {
        SiegeDrums.GetComponent<TriggerSiegeDrums>().PlayDrums();

        // Spawn enemies
        North.GetComponent<EnemySpawnerScript>().SpawnEnemy();
        South.GetComponent<EnemySpawnerScript>().SpawnEnemy();
        East.GetComponent<EnemySpawnerScript>().SpawnEnemy();
        West.GetComponent<EnemySpawnerScript>().SpawnEnemy();

        spawnedNumberOfEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        // Display "Siege XX Text"
        // When all enemies are dead we call EndSiege();

    }

    public void EndSiege()
    {
        SiegeDrums.GetComponent<TriggerSiegeDrums>().StopDrums();

        currentTurn++;
        Debug.Log("Turn is: " + currentTurn);
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
