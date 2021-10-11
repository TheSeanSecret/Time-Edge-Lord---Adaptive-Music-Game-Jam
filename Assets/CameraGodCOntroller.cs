using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraGodCOntroller : MonoBehaviour
{
    // some parts adapted from: https://www.youtube.com/watch?v=rnqF6S7PfFA&t=259s&ab_channel=GameDevGuide

    public float movementSpeed;
    public float movementTime;
    public float rotationAmount;

    public Quaternion newRotation;
    public Vector3 newPosition;


    private Camera mainCamera;

    void Start()
    {
        newPosition = transform.position;
        newRotation = transform.rotation;

        mainCamera = FindObjectOfType<Camera>();
    }


    void Update()
    {
        HandleMovementInput();
    }

    void HandleMovementInput()
    {
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            newPosition += (transform.forward * movementSpeed);
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            newPosition += (transform.forward * -movementSpeed);
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            newPosition += (transform.right * movementSpeed);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            newPosition += (transform.right * -movementSpeed);
        }



        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);

        //Camera Movement with mouse
        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        
        float rayLength;

        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 grabbedPoint = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(cameraRay.origin, grabbedPoint, Color.red);
            Debug.Log(grabbedPoint);

            /*
            if (Input.GetKey(KeyCode.Mouse0))
            {
                
                
                newPosition += grabbedPoint.;
            }
            */
        }


    }
}
