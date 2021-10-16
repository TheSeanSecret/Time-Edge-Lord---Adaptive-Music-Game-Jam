using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;


public class CountdownTimer : MonoBehaviour
{
    public static float timeStartValue = 60;
    public static float timerTime;

    public GameObject timerText;


    void Start()
    {
        timerTime = timeStartValue;
    }
    void Update()
    {
        if (timerTime > 0)
        {
            timerTime -= Time.deltaTime;
        }
        else
        {
            timerTime = 0;
            // Game End
            Debug.Log("Game End");
        }

        DisplayTime(timerTime);
    }

    void DisplayTime(float timeToDisplay)
    {
        if(timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }

        float seconds = Mathf.FloorToInt(timeToDisplay);

        timerText.GetComponent<TextMeshProUGUI>().text = "Time Resources\n" + string.Format("{0000}", seconds);
    }

    public static void AddTime(float _extraTime)
    {
        timerTime += _extraTime;
    }

}