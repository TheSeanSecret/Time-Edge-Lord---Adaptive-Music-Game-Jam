using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraGodCOntroller : MonoBehaviour
{
    // Main functionality adapted from: https://www.youtube.com/watch?v=rnqF6S7PfFA&t=259s&ab_channel=GameDevGuide

    // TODO:
    // Get Tilt Rotation to work
    // Clamp Tilt Rotation if we choose to use it
    // Clamp Zoom
    // Limit camera movement outside of level

    //Movement
    public Transform cameraTransform;
    public Transform tiltObjectTransform;

    public float movementSpeed;
    public float movementTime;
    public float rotationAmount;
    public float rotationTime;
    public float scrollSpeed;
    public float zoomMin;
    public float zoomMax;

    public Quaternion newTiltRotation;
    public Quaternion newOrbitRotation;
    public Vector3 newPosition;
    public Vector3 newZoom;

    private Vector3 previousMouseRayPosition;

    private Camera mainCamera;
    public GameObject plane;

    // Trap Gameobjects
    private Vector3 rayHitPositionForTrapPlacemenet;

    public GameObject tempTrap;

    void Start()
    {
        previousMouseRayPosition = Vector3.zero;

        newPosition = transform.position;

        newOrbitRotation = transform.rotation;

        newTiltRotation = tiltObjectTransform.rotation;
        newZoom = cameraTransform.localPosition;

        mainCamera = FindObjectOfType<Camera>();
    }


    void Update()
    {
        HandleMovementInput();
        TrapPlacementController();
    }

    void HandleMovementInput()
    {
        // Camera Movement with WASD/Arrow Keys - tilt rotation causes issues with Y position not staying at y: 0 (Vector3.right).
        
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

        float zoomDistance = Vector3.Distance(cameraTransform.localPosition, transform.position);

        //Debug.Log(zoomDistance);

        // Zoom
        if (Input.mouseScrollDelta.y != 0)
        {
            newZoom = Vector3.MoveTowards(cameraTransform.localPosition, transform.position, Input.mouseScrollDelta.y * scrollSpeed);
        }

        // Janky way to limit zoom
        if (zoomDistance < 50)
        {
            newZoom = Vector3.MoveTowards(cameraTransform.localPosition, transform.position, -20);
        }

        if (zoomDistance > 150)
        {
            newZoom = Vector3.MoveTowards(cameraTransform.localPosition, transform.position, 20);
        }


        // Camera Rotation
        if (Input.GetKey(KeyCode.Mouse0))
        {
            newOrbitRotation *= Quaternion.Euler(Vector3.up * Input.GetAxis("Mouse X") * rotationAmount);
            //newTiltRotation *= Quaternion.Euler(Vector3.right * Input.GetAxis("Mouse Y") * -rotationAmount);

            //Debug.Log(Input.GetAxis("Mouse X"));
        }

        // Lerp
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, newOrbitRotation, Time.deltaTime * rotationTime);

        //tiltObjectTransform.rotation = Quaternion.Lerp(tiltObjectTransform.rotation, newTiltRotation, Time.deltaTime * rotationTime);

        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * scrollSpeed);



        // Camera Movement with mouse
        // This took fckung forever to get to work
        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        
        float rayLength;

        //plane.GetComponent<MeshCollider>().Raycast


        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 rayHitPosition = cameraRay.GetPoint(rayLength);

            rayHitPositionForTrapPlacemenet = rayHitPosition;

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
                    // Debug.Log(difference);

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
    void TrapPlacementController()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Instantiate(tempTrap, new Vector3(rayHitPositionForTrapPlacemenet.x, rayHitPositionForTrapPlacemenet.y + tempTrap.transform.localScale.y/2, rayHitPositionForTrapPlacemenet.z), Quaternion.identity);
            
            
        }
    }
}
