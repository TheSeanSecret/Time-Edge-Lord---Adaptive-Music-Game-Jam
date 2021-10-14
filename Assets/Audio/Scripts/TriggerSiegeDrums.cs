using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Call PlayDrums when the siege starts, call StopDrums when the calm times begin.

public class TriggerSiegeDrums : MonoBehaviour
{
    public FMODUnity.EventReference siegeDrumsEvent;
    FMOD.Studio.EventInstance siegeDrums;
    bool isPlaying = false;

    public void PlayDrums()
    {
        if (isPlaying) return;

        siegeDrums = FMODUnity.RuntimeManager.CreateInstance(siegeDrumsEvent);
        siegeDrums.start();

        isPlaying = true;
    }

    public void StopDrums()
    {
        if (!isPlaying) return;
        Debug.Log("stopping");

        siegeDrums.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

        isPlaying = false;
    }

    private void OnDestroy()
    {
        StopDrums();
    }

    /* To test in inspector
    // public bool triggerDrums;
    // private void Update()
    // {
    //     if (triggerDrums)
    //     {
    //         PlayDrums();
    //     }
    //     else
    //     {
    //         StopDrums();

    //     }
    // }
    */

}
