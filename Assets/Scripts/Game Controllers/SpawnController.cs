using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;
    private Hive playerHive;

    private Timer timer;

    private float spawnInterval = 5; 

    private float launchPositionY = 0.5f;

    private bool activated = false;    




    // Start is called before the first frame update
    void Start()
    {
        initialiseTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if (activated)  {
        checkTimer();
        }
    }

    private void spawn()
    {
        Vector3 position = choosePosition();
        Debug.Log("EnemyPrefab");
        Debug.Log(enemyPrefab, playerHive);
        GameObject enemyObject = Instantiate(enemyPrefab, position, Quaternion.LookRotation(playerHive.GetPosition(), Vector3.forward)); // Quaternion.identity affects rotation?
        EnemyBody enemyBody = enemyObject.GetComponent<EnemyBody>(); 
        enemyBody.SetHive(playerHive);
        
    }

      public void SetPlayerHive(Hive playerHive)
    {
        Debug.Log("Setting player hive in spawner");
        Debug.Log(playerHive);
        this.playerHive = playerHive;
        activated = true;
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

        private Vector3 choosePosition() {
            return new Vector3(Random.Range(-2.0f, 2.0f), launchPositionY, Random.Range(-2.0f, 2.0f));
        }
}
