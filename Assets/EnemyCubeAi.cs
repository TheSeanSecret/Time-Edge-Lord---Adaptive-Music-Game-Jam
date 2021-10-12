using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCubeAi : MonoBehaviour
{

    public Rigidbody rb;

    public float XYZRotationForce = 500f;
    public float speed = 1;

    void Start()
    {
        rb.AddRelativeTorque(Random.Range(-XYZRotationForce, XYZRotationForce), Random.Range(-XYZRotationForce, XYZRotationForce), Random.Range(-XYZRotationForce, XYZRotationForce));
    }


    void FixedUpdate()
    {
        rb.AddRelativeTorque(Random.Range(-XYZRotationForce, XYZRotationForce), Random.Range(-XYZRotationForce, XYZRotationForce), Random.Range(-XYZRotationForce, XYZRotationForce));
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(0.5f, 0.5f, 1.0f), speed * Time.deltaTime);
    }
}
