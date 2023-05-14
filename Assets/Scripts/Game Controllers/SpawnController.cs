using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    private Hive playerHive;

        private Timer timer;

        private float spawnInterval = 5; 




    // Start is called before the first frame update
    void Start()
    {
        initialiseTimer();
    }

    // Update is called once per frame
    void Update()
    {
        checkTimer();
    }

    private void spawn()
    {
        Debug.Log("Spawn!");
    }

      public void SetPlayerHive(Hive playerHive)
    {
        this.playerHive = playerHive;
    }

       private void checkTimer()
      {
        if (!timer.IsOn())
        {
            Debug.Log("Spawn Timer");
            timer.SetOn(true);
            timer.Restart();
            spawn();
        }
      }

       private void initialiseTimer()
        {
        timer = gameObject.GetComponent<Timer>();
        timer.SetOn(true);
        timer.SetCountdownSeconds(spawnInterval);
        }
}
