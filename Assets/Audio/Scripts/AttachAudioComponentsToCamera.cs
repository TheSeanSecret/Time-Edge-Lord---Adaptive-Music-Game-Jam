using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Adds components needed for FMOD to the Main Camera.
public class AttachAudioComponentsToCamera : MonoBehaviour
{
    GameObject cameraObject;

    private void Start()
    {
        AttachObjects();
    }

    private void Reset()
    {
        AttachObjects();
    }

    private void AttachObjects()
    {
        if (!cameraObject)
        {
            cameraObject = Camera.main.gameObject;
        }

        if (cameraObject.GetComponent<AudioListener>())
        {
            Destroy(cameraObject.GetComponent<AudioListener>());
        }

        if (!cameraObject.GetComponent<FMODUnity.StudioListener>())
        {
            cameraObject.AddComponent<FMODUnity.StudioListener>();
        }

        if (!cameraObject.GetComponent<BoxCollider>())
        {
            var col = cameraObject.AddComponent<BoxCollider>();
            col.isTrigger = true;
        }

        if (!cameraObject.GetComponent<Rigidbody>())
        {
            var rb = cameraObject.AddComponent<Rigidbody>();
            rb.useGravity = false;
            rb.isKinematic = true;
            rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        }


    }

}
