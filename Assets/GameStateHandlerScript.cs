using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameStateHandlerScript : MonoBehaviour
{
    public int numberOfSieges = 3;
    int currentTurn;
    public int spawnedNumberOfEnemies;
    public int currentNumberOfEnemies;
    public float percentageOfEnemies;
    public float timeMultiplier;

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
        percentageOfEnemies = ((float)currentNumberOfEnemies / (float)spawnedNumberOfEnemies) * 100f;
        // add null exception

        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("PercentOfEnemiesRemaining", percentageOfEnemies);
        
        if (currentNumberOfEnemies <= 0 && (currentTurn < numberOfSieges))
        {
            // Debug.Log(currentTurn + ", " + numberOfSieges);
            RestartOrCheckWin();
        }
    }

    public void StartCalmTimes()
    {
        Debug.Log("StartCalmTimes");

        Invoke("StartSiege", 5f);

        // Enable colliders if enemies spawn inside each other and reach the hill by collisions with each other
        GameObject[] CollidersToEnable = GameObject.FindGameObjectsWithTag("EnemySpawnCollider");
        foreach (GameObject Collider in CollidersToEnable)
        {
            Collider.SetActive(true);
        }

        // Spawn enemies from random direction
        int randomDirection = Random.Range(1, 5);
        switch (randomDirection)
        {
            case 4:
                North.GetComponent<EnemySpawnerScript>().SetCurrentTurnAndSpawnEnemies(currentTurn);
                Debug.Log("Enemies will come from North");
                break;
            case 3:
                East.GetComponent<EnemySpawnerScript>().SetCurrentTurnAndSpawnEnemies(currentTurn);
                Debug.Log("Enemies will come from East");
                break;
            case 2:
                West.GetComponent<EnemySpawnerScript>().SetCurrentTurnAndSpawnEnemies(currentTurn);
                Debug.Log("Enemies will come from West");
                break;
            case 1:
                South.GetComponent<EnemySpawnerScript>().SetCurrentTurnAndSpawnEnemies(currentTurn);
                Debug.Log("Enemies will come from South");
                break;
        }
    }

    public void DisableAllEnemies()
        // Code run from enemyspanwerscript
    {
        // Find all enemies and disable renderer and movement except for sound
        GameObject[] EnemiesToDisable = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject Enemy in EnemiesToDisable)
        {
            Enemy.GetComponent<EnemyCubeAi>().enabled = false;
            Enemy.GetComponent<MeshRenderer>().enabled = false;
        }

        // Count number of enemies
        spawnedNumberOfEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        Debug.Log("There are " + spawnedNumberOfEnemies + " enemies");
    }

    public void StartSiege()
    {
        Debug.Log("StartSiege");

        CountdownTimer.AddTime(currentTurn * timeMultiplier);

        Debug.Log("Started Siege");
        SiegeDrums.GetComponent<TriggerSiegeDrums>().PlayDrums();

        GameObject[] CollidersToDisable = GameObject.FindGameObjectsWithTag("EnemySpawnCollider");
        foreach (GameObject Collider in CollidersToDisable)
        {
            Collider.SetActive(false);
        }

        // Find all enemies and enable
        GameObject[] EnemiesToEnable = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject Enemy in EnemiesToEnable)
        {
            Enemy.GetComponent<EnemyCubeAi>().enabled = true;
            Enemy.GetComponent<MeshRenderer>().enabled = true;
        }
        // Display "Siege XX Text"

        Debug.Log("Turn is: " + currentTurn);
        currentTurn++;

        // When all enemies are dead (checked in Update) we call RestartOrCheckWin();
    }

    public void RestartOrCheckWin()
    {
        SiegeDrums.GetComponent<TriggerSiegeDrums>().StopDrums();

        if (currentTurn >= (numberOfSieges - 1))
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
        YouWinText.SetActive(true);
    }

    public void GameEndLose()
    {
        // You lose! :(
        Time.timeScale = 0f;    
        YouLoseText.SetActive(true);
        Debug.Log("You Lose");
    }
}
