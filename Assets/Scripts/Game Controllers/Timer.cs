using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float timer;

    private float seconds;

    private bool on = false;

    private void Update()
    {
        Debug.Log("In update on timer");
        checkTimer();
        reduceTimer();
    }

    public void SetTime(float seconds)
    {
        this.seconds = seconds;
        Debug.Log("Timer set to " + seconds);

    }

    private void checkTimer()
    {
        if (timer <= 0) {
            on = on ? false : true;
        }
    }

    private void reduceTimer()
    {
       timer = timer < 0 ? seconds : timer;
       timer -= Time.deltaTime;
    }

    public bool IsOn()
    {
        return this.on;
    }
}
