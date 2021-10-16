using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCubeAi : MonoBehaviour
{
    GameObject GameStateHandler;

    public float XYZRotationForce = 500f;
    public float speed = 1;

    void Start()
    {
        GetComponent<Rigidbody>().AddRelativeTorque(Random.Range(-XYZRotationForce, XYZRotationForce), Random.Range(-XYZRotationForce, XYZRotationForce), Random.Range(-XYZRotationForce, XYZRotationForce));
        GameStateHandler = GameObject.Find("GameStateHandler");
    }


    void FixedUpdate()
    {
        GetComponent<Rigidbody>().AddRelativeTorque(Random.Range(-XYZRotationForce, XYZRotationForce), Random.Range(-XYZRotationForce, XYZRotationForce), Random.Range(-XYZRotationForce, XYZRotationForce));
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(0.5f, 0.5f, 1.0f), speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            Destroy(gameObject);
        }

        // If player hits checkpoint then set new respanwCheckpoint
        if (other.gameObject.tag == "Hill")
        {
            GameStateHandler.GetComponent<GameStateHandlerScript>().GameEndLose();
        }
    }
}
