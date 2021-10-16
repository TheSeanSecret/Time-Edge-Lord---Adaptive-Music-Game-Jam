using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed = 1;

    public bool tracking = false;
    public float trackingSpeed = 1;

    float currentTrackingSpeed;
    public float trackingSpeedLerpTime = 1;

    // Assign tracking target here
    public Transform targetTransform;


    private void Start()
    {
        // Find Closest Enemy In their current direction
    }

    // Update is called once per frame
    void Update()
    {
        Tracking();
    }

    void Tracking()
    {
        // If target transform is lost then choose another closest target OR continue on path til deathhit something and delete itself
        // Forward Movement
        transform.position += transform.forward * speed * Time.deltaTime;

        // Tracking
        if (tracking == true)
        {

            currentTrackingSpeed = Mathf.Lerp(currentTrackingSpeed, trackingSpeed, Time.deltaTime * trackingSpeedLerpTime / 100);

            // Lerp between our position and target position
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(targetTransform.position - transform.position), currentTrackingSpeed * Time.deltaTime);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Instantiate Explosion FX
        Destroy(gameObject);
    }
}
