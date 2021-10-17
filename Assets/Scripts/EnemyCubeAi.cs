using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCubeAi : MonoBehaviour
{
    GameObject GameStateHandler;
    GameObject Hill;

    public float XYZRotationForce = 500f;
    public float speed = 1;

    void Start()
    {
        GetComponent<Rigidbody>().AddRelativeTorque(Random.Range(-XYZRotationForce, XYZRotationForce), Random.Range(-XYZRotationForce, XYZRotationForce), Random.Range(-XYZRotationForce, XYZRotationForce));
    }


    void FixedUpdate()
    {
        GetComponent<Rigidbody>().AddRelativeTorque(Random.Range(-XYZRotationForce, XYZRotationForce), Random.Range(-XYZRotationForce, XYZRotationForce), Random.Range(-XYZRotationForce, XYZRotationForce));
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(0.5f, 0.5f, 1.0f), speed * Time.deltaTime);

        // Because some enemies shake so much that they glitch through the level and fall down
        if (transform.position.y <= -20)
        {
            Destroy(gameObject);
        }

        // some objects reach the hill even though the shouldn't
        Hill = GameObject.Find("Hill");
        if (Hill.gameObject.GetComponent<Collider>().bounds.Contains(transform.position))
        {
            Invoke("EnemyDeath", 1);
        }


        if (GetComponent<MeshRenderer>().enabled == false)
        {
            GetComponent<EnemyCubeAi>().enabled = false;
        }
    }

    void EnemyDeath()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        /*
        if (other.gameObject.tag == "Bullet")
        {
            Debug.Log("Hit by bullet");
            Destroy(this.gameObject);
        }
        */

        // If player hits checkpoint then set new respanwCheckpoint
        if (other.gameObject.tag == "Hill")
        {
            if (GetComponent<MeshRenderer>().enabled == false)
            {
                Destroy(gameObject);
            }
            else if (GetComponent<MeshRenderer>().enabled == true)
            {
                GameStateHandler = GameObject.Find("GameStateHandler");
                GameStateHandler.GetComponent<GameStateHandlerScript>().GameEndLose();
                Debug.Log("Enemy Reached Hill");
            }
        }
    }

    void OnCollisionEnter(Collider other)
    {
        if (other.gameObject.tag == "EnemySpawnCollider")
        {
            Debug.Log("Killed myself");
            Destroy(gameObject);
        }
    }
}
