using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;

    public GameObject spawnTurretPosition;
    public GameObject spawnTurretPositionCube;
    private Renderer spawnTurretPositionCubeRenderer;

    public GameObject hill;
    public GameObject Timer;
    float timeToRemove = 0f;

    [Header("Colors")]
    public Color cannotPlaceHereColor;
    public Color startColor;


    [Header("Turrets")]
    public GameObject missileTurret;
    public GameObject normalTurret;
    public GameObject machineGunTurret;

    private GameObject currentChoosenTurret = null;


    void Start()
    {
        spawnTurretPositionCubeRenderer = spawnTurretPositionCube.GetComponent<Renderer>();
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

    public void ChooseNormalTurret()
    {
        Debug.Log("Chose Normal Turret");
        currentChoosenTurret = normalTurret;
        timeToRemove = -10;
    }
    public void ChooseMissileTurret()
    {
        Debug.Log("Chose Missile Turret");
        currentChoosenTurret = missileTurret;
        timeToRemove = -30;
    }

    public void ChooseMachineGunTurret()
    {
        Debug.Log("Chose Machine Gun Turret");
        currentChoosenTurret = machineGunTurret;
        timeToRemove = -60;
    }

    void TurretPlacementHandler()
    {
        bool mayPlaceTurret = false;

        if (hill.gameObject.GetComponent<Collider>().bounds.Contains(spawnTurretPosition.transform.position))
        {
            //Debug.Log("White");
            spawnTurretPositionCubeRenderer.material.SetColor("_EmissionColor", startColor);
            mayPlaceTurret = true;
        }
        else
        {
            //Debug.Log("Red");
            spawnTurretPositionCubeRenderer.material.SetColor("_EmissionColor", cannotPlaceHereColor);
            mayPlaceTurret = false;
        }

        if (Input.GetKey(KeyCode.Mouse1))
        {
            spawnTurretPositionCubeRenderer.enabled = true;
        }
        else
        {
            spawnTurretPositionCubeRenderer.enabled = false;
        }


        if (Input.GetKeyDown(KeyCode.Mouse0) && Input.GetKey(KeyCode.Mouse1) && mayPlaceTurret)
        {
            CountdownTimer.AddTime(timeToRemove);

            Instantiate(currentChoosenTurret, new Vector3(spawnTurretPosition.transform.position.x, (hill.transform.localScale.y / 2), spawnTurretPosition.transform.position.z), spawnTurretPosition.transform.rotation);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            // Delete Turret that player is looking at
        }
    }
}
