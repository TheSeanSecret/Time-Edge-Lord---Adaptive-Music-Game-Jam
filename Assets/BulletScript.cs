using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed = 1;


    public bool tracking = false;
    public float trackingSpeed = 1;

    // Assign tracking target here
    public Transform targetTransform;


    // Update is called once per frame
    void Update()
    {
        // Forward Movement
        transform.position += transform.forward * speed * Time.deltaTime;

        // Tracking
        if (tracking == true)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(targetTransform.position - transform.position), trackingSpeed * 2 * Time.deltaTime);
        }
    }
}
