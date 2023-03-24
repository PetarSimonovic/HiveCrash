using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomatedController : MonoBehaviour
{
    [SerializeField]
    private GameObject hiveObject;

    private Hive hive;

    [SerializeField]
    private EnemyBeeLauncher beeLauncher; 

    [SerializeField]
    private BeeController beeController; 

    private Timer timer;

    private MapCreator mapCreator;

    private bool hiveIsPlaced;

    private float beeLaunchInterval = 4f;
    

    void Update()
    {
       if (hiveIsPlaced) 
       {
        checkTimer();
       }
    }

    private void initialiseTimer()
        {
        timer = gameObject.GetComponent<Timer>();
        timer.SetOn(true);
        timer.SetCountdownSeconds(beeLaunchInterval);
        }


    public void PlaceHive(Vector3 tilePosition)
    {
        tilePosition.y = 5f;
        hiveObject = Instantiate(hiveObject, tilePosition, Quaternion.identity);
        hiveObject.GetComponent<Hive>().titleHive = true;
        hiveIsPlaced = true;
        
    }



      private void launchBee()
      {
        Vector3 target = new Vector3 (Random.Range(-4.0f, 4.0f), 0.5f, Random.Range(-4.0f, 4.0f));
        beeLauncher.SetTarget(target);
        if (beeLauncher.IsLoaded()) {
          beeLauncher.LaunchBee();
        }
      }


      private void checkTimer()
      {
        if (!timer.IsOn())
        {
            timer.SetOn(true);
            timer.Restart();
            launchBee();
        }
      }
}
