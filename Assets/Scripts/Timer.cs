using System;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private TextMeshProUGUI timerText;

    [Header("Timer Settings")]
    [SerializeField] private string text;
    [SerializeField] private float currentTime;

    private TimeSpan timeLimit;

    private void Start()
    {
        timeLimit = new TimeSpan(99, 00, 00);
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = currentTime += Time.deltaTime;
        TimeSpan time = TimeSpan.FromSeconds(currentTime);

        if (time >= timeLimit)
        {
            SetTimerText();
            timerText.color = Color.red;
            enabled = false;
        }

        SetTimerText();
    }

    private void SetTimerText()
    {
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        if (time < timeLimit) timerText.text = text + string.Format("{0:D2}:{1:D2}:{2:D2}", time.Hours, time.Minutes, time.Seconds);
        else timerText.text = text + string.Format("{0:D2}:{1:D2}:{2:D2}", timeLimit.Hours, timeLimit.Minutes, timeLimit.Seconds);
    }
}
