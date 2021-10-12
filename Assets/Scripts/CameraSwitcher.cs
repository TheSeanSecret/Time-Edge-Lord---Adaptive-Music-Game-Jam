using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{

    public GameObject PlayerCamera;
    public GameObject GodCamera;
    public GameObject ControlsTextPlayerView;
    public GameObject ControlsTextGodView;

    void Start()
    {
        // Start with the player perspective and lock cursor
        // PlayerCamera.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(PlayerCamera.activeSelf == true)
            {
                PlayerCamera.SetActive(false);
                GodCamera.SetActive(true);
                ControlsTextPlayerView.SetActive(false);
                ControlsTextGodView.SetActive(true);

                Debug.Log("Switch to God Camera");

                Cursor.lockState = CursorLockMode.None;
            }
            else if (PlayerCamera.activeSelf == false)
            {
                GodCamera.SetActive(false);
                PlayerCamera.SetActive(true);
                ControlsTextGodView.SetActive(false);
                ControlsTextPlayerView.SetActive(true);

                Debug.Log("Switch to Player Camera");

                Cursor.lockState = CursorLockMode.Locked;
            }
        }


    }
}
