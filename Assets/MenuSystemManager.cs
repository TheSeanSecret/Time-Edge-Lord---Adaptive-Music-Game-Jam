using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class MenuSystemManager : MonoBehaviour
{
    // Post Processings
    public PostProcessVolume volume;
    DepthOfField depthOfField;

    // Values
    private float focusdistance = 10;
    private float newfocusdistance = 0;
    public float focusSpeed = 1;
    public float timeScaleSwitchLerpTime = 1;
    public float newTimeScaleValue = 2;

    // Cameras
    public GameObject PlayerCamera;
    public GameObject GodCamera;

    // Camera Movement Scripts
    public GameObject godCameraMovement;
    public GameObject playerCameraMovement;
    public GameObject playerCameraLookAround;
    public GameObject cameraSwitcher;


    // Menus
    public GameObject turretMenu;
    public GameObject trapMenu;

    void Start()
    {
        volume.profile.TryGetSettings(out depthOfField);
        // depthOfField.active = true;
        newfocusdistance = 10;


        turretMenu.SetActive(false);
        trapMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        ShowHideTabMenu();
    }

    // TODO, make this work by copying logic from camera switcher

    void ShowHideTabMenu()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // Mouse is enabled
            Cursor.lockState = CursorLockMode.None;

            // Set new focus
            newfocusdistance = 0;

            //Disable camera movement on both views
            // godCameraMovement.GetComponent<CameraGodCOntroller>().enabled = false;
            // playerCameraMovement.GetComponent<PlayerController>().enabled = false;
            playerCameraLookAround.GetComponent<MouseLook>().enabled = false;


            if (PlayerCamera.activeSelf == true)
            {
                turretMenu.SetActive(true);
            }

            if (GodCamera.activeSelf == true)
            {
                trapMenu.SetActive(true);
            }

            // Slow down time
            newTimeScaleValue = 0.2f;
        }

        if (Input.GetKeyUp(KeyCode.Tab))
        {
            // Set new focus
            newfocusdistance = 10;

            //Enable camera movement on both views
            // godCameraMovement.GetComponent<CameraGodCOntroller>().enabled = true;
            // playerCameraMovement.GetComponent<PlayerController>().enabled = true;
            playerCameraLookAround.GetComponent<MouseLook>().enabled = true;

            turretMenu.SetActive(false);
            trapMenu.SetActive(false);

            // Mouse disabled if in fps view
            if (PlayerCamera.activeSelf == true)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }

            // Mouse enabled if in god view
            if (GodCamera.activeSelf == true)
            {
                Cursor.lockState = CursorLockMode.None;
            }

            // Time back to normal
            newTimeScaleValue = 1f;
        }

        depthOfField.focusDistance.value = focusdistance;
        focusdistance = Mathf.Lerp(focusdistance, newfocusdistance, focusSpeed * Time.deltaTime);

        Time.timeScale = Mathf.Lerp(Time.timeScale, newTimeScaleValue, timeScaleSwitchLerpTime * Time.deltaTime);
    }
}
