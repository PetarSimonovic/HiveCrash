using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float timer;

    private float countdownSeconds;

    private bool on;
    

    private void Update()
    {
        checkTimer();
        reduceTimer();
    }

    public void SetCountdownSeconds(float countdownSeconds)
    {
        this.countdownSeconds = countdownSeconds;
    }

    public void Restart()
    {
       this.timer = this.countdownSeconds;
    }

    private void checkTimer()
    {
        if (timer <= 0) {
            on = on ? false : true;
        }
    }

    private void reduceTimer()
    {
       timer = timer < 0 ? countdownSeconds : timer;
       timer -= Time.deltaTime;
    }

    public bool IsOn()
    {
        return this.on;
    }

    public float GetTime()
    {
        return this.timer;
    }

    public void SetOn(bool on)
    {
        this.on = on;
    }

}
