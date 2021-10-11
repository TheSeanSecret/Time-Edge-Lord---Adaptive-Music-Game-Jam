using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{

    public GameObject PlayerCamera;
    public GameObject GodCamera;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            if(PlayerCamera.activeSelf == true)
            {
                PlayerCamera.SetActive(false);
                GodCamera.SetActive(true);
                Debug.Log("Switch to God Camera");
            }
            else if (PlayerCamera.activeSelf == false)
            {
                GodCamera.SetActive(false);
                PlayerCamera.SetActive(true);
                Debug.Log("Switch to Player Camera");
            }
        }
    }
}
