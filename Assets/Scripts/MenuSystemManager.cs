using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class MenuSystemManager : MonoBehaviour
{
    // Post Processings
    [Header("Post Processing")]
    public PostProcessVolume volume;
    DepthOfField depthOfField;

    // Values
    [Header("Values")]
    private float focusdistance = 10;
    private float newfocusdistance = 0;
    float currentFocus;
    public float defaultFocusDistance = 10;
    public float focusSpeed = 1;
    public float timeScaleSwitchLerpTime = 1;
    public float newTimeScaleValue = 2;


    // Cameras
    [Header("Camera")]
    public GameObject mainCamera;
    public GameObject PlayerCamera;
    public GameObject GodCamera;

    // Camera Movement Scripts
    [Header("Movement Scripts")]
    public GameObject godCameraMovement;
    public GameObject playerCameraMovement;
    public GameObject playerCameraLookAround;
    public GameObject cameraSwitcher;

    // Menus
    [Header("Menus")]
    public GameObject turretMenu;
    public GameObject trapMenu;
    public GameObject loseMenu;
    public GameObject winMenu;

    void Start()
    {
        volume.profile.TryGetSettings(out depthOfField);
        // depthOfField.active = true;
        newfocusdistance = 10;
        newTimeScaleValue = 1f;

        turretMenu.SetActive(false);
        trapMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        NewTabMenuScript();
    }

    void NewTabMenuScript()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (mainCamera.GetComponent<CameraSwitcher>().activeCamera == CameraSwitcher.CurrentCamera.PlayerCam)
            {
                // Enabled Mouse
                Cursor.lockState = CursorLockMode.None;
                // Set new focus
                newfocusdistance = 0;
                // Disable camera movement
                playerCameraLookAround.GetComponent<MouseLook>().enabled = false;
                // Show menu
                turretMenu.SetActive(true);
                // Slow down time
                newTimeScaleValue = 0.2f;
            }

            if (mainCamera.GetComponent<CameraSwitcher>().activeCamera == CameraSwitcher.CurrentCamera.GodCam)
            {
                // Enabled Mouse
                Cursor.lockState = CursorLockMode.None;
                // Set new focus
                newfocusdistance = 0;
                // Disable camera movement
                godCameraMovement.GetComponent<CameraGodCOntroller>().enabled = false;
                playerCameraMovement.GetComponent<PlayerController>().enabled = false;
                // Show menu
                trapMenu.SetActive(true);
                // Slow down time
                newTimeScaleValue = 0.2f;
            }
        }

        if (Input.GetKeyUp(KeyCode.Tab))
        {
            if (mainCamera.GetComponent<CameraSwitcher>().activeCamera == CameraSwitcher.CurrentCamera.PlayerCam)
            {
                // Enabled Mouse
                Cursor.lockState = CursorLockMode.Locked;
                // Set new focus
                newfocusdistance = 10;
                // ENable camera movement
                playerCameraLookAround.GetComponent<MouseLook>().enabled = true;
                // Show menu
                turretMenu.SetActive(false);
                // Slow down time
                newTimeScaleValue = 1f;
            }

            if (mainCamera.GetComponent<CameraSwitcher>().activeCamera == CameraSwitcher.CurrentCamera.GodCam)
            {
                // Enabled Mouse
                Cursor.lockState = CursorLockMode.None;
                // Set new focus
                newfocusdistance = 10;
                //Disable camera movement
                godCameraMovement.GetComponent<CameraGodCOntroller>().enabled = true;
                playerCameraMovement.GetComponent<PlayerController>().enabled = true;
                // Show menu
                trapMenu.SetActive(false);
                // Slow down time
                newTimeScaleValue = 1f;
            }
        }

        // Lerp focus
        depthOfField.focusDistance.value = focusdistance;
        focusdistance = Mathf.Lerp(focusdistance, newfocusdistance, focusSpeed * Time.deltaTime);

        // Disable DOF if focus is further that default, enable if closer that default
        currentFocus = newfocusdistance;
        if (currentFocus >= defaultFocusDistance)
        {
            depthOfField.active = false;
        }
        else if (currentFocus < defaultFocusDistance)
        {
            depthOfField.active = true;
        }

        // Lerp time
        Time.timeScale = Mathf.Lerp(Time.timeScale, newTimeScaleValue, timeScaleSwitchLerpTime * Time.deltaTime);
    }  
}
