using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    public bool isRunning;
    public TimeSpan timeToFinish { get; private set; }
    public double secondsLeft { get; private set; }
    public UnityEvent TimerFinishedEvent;

    public void Initialise(TimeSpan time) {
        isRunning = false;
        timeToFinish = time;
        secondsLeft = timeToFinish.TotalSeconds;
        TimerFinishedEvent = new UnityEvent();
    }

    public void Reset() {
        isRunning = false;
        secondsLeft = timeToFinish.TotalSeconds;
    }

    public void StartTimer() {
        secondsLeft = timeToFinish.TotalSeconds;
        isRunning = true;
    }

    private void Update() {
        if (isRunning) { 
            if (secondsLeft > 0) {
                secondsLeft -= Time.deltaTime;
            } else {
                isRunning = false;
                secondsLeft = 0;
                TimerFinishedEvent.Invoke();
            }
        }
    }

    public string DisplayTime() {
        string text = "";
        TimeSpan timeLeft = TimeSpan.FromSeconds(secondsLeft);

        if (timeLeft.Days != 0) {
            text += timeLeft.Days + "d ";
            text += timeLeft.Hours + "h";
        } else if (timeLeft.Hours != 0) {
            text += timeLeft.Hours + "h ";
            text += timeLeft.Minutes + "min";
        } else if (timeLeft.Minutes != 0) {
            text += timeLeft.Minutes + "m ";
            text += timeLeft.Seconds + "s";
        } else if (secondsLeft > 0) {
            text += Mathf.FloorToInt((float) secondsLeft) + "s";
        } else {
            text = "Done";
        }

        return text;
    }
}
