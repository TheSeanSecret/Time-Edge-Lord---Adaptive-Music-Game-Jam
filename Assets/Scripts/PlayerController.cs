using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public GameObject turretMissilePosition;

    public float speed = 12f;

    void Start()
    {
        //transform.rotation = new Quaternion(0f, 90f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);
    }

    void TurretPlacementHandler()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            // Higlight Current Turrent at position of
        }


        if (Input.GetKeyDown(KeyCode.Mouse0) && Input.GetKey(KeyCode.Mouse1))
        {
            // Place Turret
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            // Delete Turret that player is looking at
        }
    }
}
