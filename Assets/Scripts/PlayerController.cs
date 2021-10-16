using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;

    public GameObject spawnTurretPosition;
    private Renderer spawnTurretPositionCubeRenderer;

    public GameObject hill;

    [Header("Colors")]
    public Color cannotPlaceHereColor;
    public Color startColor;


    [Header("Turrets")]
    public GameObject missileTurret;
    public GameObject normalTurret;
    public GameObject machineGunTurret;



    void Start()
    {
        spawnTurretPositionCubeRenderer = spawnTurretPosition.GetComponent<Renderer>();
        startColor = spawnTurretPositionCubeRenderer.material.color;

        //transform.rotation = new Quaternion(0f, 90f, 0f, 0f);

    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        TurretPlacementHandler();
    }

    void ChooseCurrentTurret()
    {

    }

    void TurretPlacementHandler()
    {
        if (hill.gameObject.GetComponent<Collider>().bounds.Contains(spawnTurretPosition.transform.position))
        {
            Debug.Log("White");
            spawnTurretPositionCubeRenderer.material.SetColor("_EmissionColor", startColor);
        }
        else
        {
            Debug.Log("Red");
            spawnTurretPositionCubeRenderer.material.SetColor("_EmissionColor", cannotPlaceHereColor);
        }

        if (Input.GetKey(KeyCode.Mouse1))
        {
            // Higlight Current Turrent at position of
            // Make brighter
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
