using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraGodCOntroller : MonoBehaviour
{
    // some parts adapted from: https://www.youtube.com/watch?v=rnqF6S7PfFA&t=259s&ab_channel=GameDevGuide

    public float movementSpeed;
    public float movementTime;
    public float rotationAmount;
    public float rotationTime;

    public Quaternion newRotation;
    public Vector3 newPosition;

    private Vector3 previousMouseRayPosition;

    private Camera mainCamera;
    public GameObject plane;

    void Start()
    {
        previousMouseRayPosition = Vector3.zero;

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
        /*
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
        */

        if (Input.GetKey(KeyCode.Mouse0))
        {
            newRotation *= Quaternion.Euler(Vector3.up * Input.GetAxis("Mouse X") * rotationAmount);
            newRotation *= Quaternion.Euler(Vector3.right * Input.GetAxis("Mouse Y") * -rotationAmount);
            //Debug.Log(Input.GetAxis("Mouse X"));
        }


        // Lerped
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * rotationTime);

        // Hard
        // transform.position = newPosition;
        // transform.rotation = newRotation;



        //Camera Movement with mouse
        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        
        float rayLength;

        //plane.GetComponent<MeshCollider>().Raycast


        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 rayHitPosition = cameraRay.GetPoint(rayLength);

            Debug.DrawLine(cameraRay.origin, rayHitPosition, Color.red);
            // Debug.Log(grabbedPoint);

            RaycastHit hit;
            
            if (Input.GetKey(KeyCode.Mouse2) && plane.GetComponent<MeshCollider>().Raycast(cameraRay, out hit, rayLength))
            {
                if (previousMouseRayPosition == Vector3.zero) // First  time?
                {
                //    previousMouseRayPosition = grabbedPoint;
                }
                else
                {
                    Vector3 difference;
                    difference = previousMouseRayPosition - rayHitPosition;
                    Debug.Log(difference);

                    newPosition += difference;
                //    previousMouseRayPosition = grabbedPoint;
                } // if
                previousMouseRayPosition = rayHitPosition;
            }

            if (Input.GetKeyUp(KeyCode.Mouse2))
            {
                // Debug.Log("Mouse released");
                previousMouseRayPosition = Vector3.zero;
            }
        }
    }
}
