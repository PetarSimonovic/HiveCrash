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
        checkTimer();
        reduceTimer();
    }

    public void SetTime(float seconds)
    {
        this.seconds = seconds;
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
