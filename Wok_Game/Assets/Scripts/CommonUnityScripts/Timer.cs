using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// starts a timer that updates UI text

public class Timer : MonoBehaviour
{
    public TMP_Text timerText;
    private float startTime;
    private bool finished = false;
    
    void Start()
    {
        startTime = Time.time;
    }

    void Update()
    {
        if (finished == true)
            return;

        float t = Time.time - startTime;

        string minutes = ((int)t / 60).ToString();
        string seconds = (t % 60).ToString("f2");

        timerText.text = "time: " + minutes + ":" + seconds;
    }
}
