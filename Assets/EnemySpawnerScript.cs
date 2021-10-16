using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour
{
    public GameObject CubeEnemy;
    public GameObject SphereEnemy;
    public GameObject TriangleEnemy;

    public GameObject GameStateHandlerScript;
    public int thisTimesCurrentTurn = 2;
    int currentTurn = 1;
    int numberOfEnemiesToSpawn;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCurrentTurnAndSpawnEnemies(int _currentTurn)
    {
        currentTurn = _currentTurn;
        numberOfEnemiesToSpawn = currentTurn * thisTimesCurrentTurn;

        for (int i = 0; i < numberOfEnemiesToSpawn; i++)
        {
            SpawnEnemy();
        }
    }

    public void SpawnEnemy()
    {
        GameObject RandomEnemy = CubeEnemy;
        int RandomNumber;

        RandomNumber = Random.Range(1, 4);
        if (RandomNumber == 1)
        {
            RandomEnemy = CubeEnemy;
        }
        if (RandomNumber == 2)
        {
            RandomEnemy = SphereEnemy;
        }
        if (RandomNumber == 3)
        {
            RandomEnemy = TriangleEnemy;
        }

        // Spawn a enemy (times currentTurn of gamestatehandler) at random positions of this objects X size
        Instantiate(RandomEnemy, new Vector3(Random.Range(GetComponent<Collider>().bounds.size.x / 2 + transform.position.x, -GetComponent<Collider>().bounds.size.x / 2 + transform.position.x), transform.position.y, transform.position.z), transform.rotation);
    }
}
