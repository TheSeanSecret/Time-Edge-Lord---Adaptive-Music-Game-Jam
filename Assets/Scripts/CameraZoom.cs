using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public Vector3 newPosition;
    public float movementTime;
    public float scrollSpeed;


    public Transform lookAtTransform;


    void Start()
    {
        newPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Zoom
        if (Input.mouseScrollDelta.y != 0)
        {
            newPosition = Vector3.MoveTowards(transform.position, lookAtTransform.position, Input.mouseScrollDelta.y * scrollSpeed);

        }

        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);


    }
}
