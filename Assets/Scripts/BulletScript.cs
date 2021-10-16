using System;
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
    private Transform target;
    public GameObject Timer;


    public void Seek(Transform _target)
    {
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }
        
        //Debug.Log(target);
        
        Tracking();
    }

    void Tracking()
    {
        // Forward Movement
        // transform.position += transform.forward * speed * Time.deltaTime;

        // Tracking
        if (tracking == true)
        {
            Vector3 dir = target.position - transform.position;
            float distanceThisFrame = speed * Time.deltaTime;

            if (dir.magnitude <= distanceThisFrame)
            {
                HitTarget();
                return;
            }
            transform.Translate(dir.normalized * distanceThisFrame, Space.World);



            // currentTrackingSpeed = Mathf.Lerp(currentTrackingSpeed, trackingSpeed, Time.deltaTime * trackingSpeedLerpTime / 100);

            // Lerp between our position and target position
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(target.position - transform.position), currentTrackingSpeed * Time.deltaTime);
        }
    }



    
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Destroy(other.gameObject);
            Debug.Log("Hit Enemy");
            CountdownTimer.AddTime(5);
        }

        // Instantiate Explosion FX
        Destroy(gameObject);
    }

    void HitTarget()
    {
        // Debug.Log("Hit " + target);
    }
}
