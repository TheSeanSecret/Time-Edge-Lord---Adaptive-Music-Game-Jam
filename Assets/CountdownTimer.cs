using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;


public class CountdownTimer : MonoBehaviour
{
    public float timeStartValue = 60;
    private float timerTime;

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

    void AddTime()
    {
        timerTime += 0;
    }

}