using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomatedHiveController : MonoBehaviour
{
    [SerializeField]
    private GameObject hiveObject;

    [SerializeField]
    private BeeController beeController;


    [SerializeField]
    private AutomatedBeeLauncher beeLauncher; 

    private Hive hive;

    private Timer timer;

    private MapCreator mapCreator;

    private bool hiveIsPlaced = false;

    private float beeLaunchInterval = 4f;
    

       void Awake()
      {
        beeLauncher = Instantiate(beeLauncher);
      }

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


    public void InitialiseHive(Vector3 tilePosition)
    {
        tilePosition.y = 5f;
        hiveObject = Instantiate(hiveObject, tilePosition, Quaternion.identity);
        hive = hiveObject.GetComponent<Hive>();
        hive.titleHive = true;
        hive.SetPosition(hiveObject.transform.position);
        beeLauncher.SetHive(hive);
        initialiseBeeController();
        initialiseTimer();
        hiveIsPlaced = true;


      }  
        

       private void initialiseBeeController()
        {
            Debug.Log("Initialising beeController");
            Debug.Log(hive);
            beeController = Instantiate(beeController);
            beeController.SetHive(hive);
            beeController.AddBees(5);
        }




       private void launchBee()
      {
        beeLauncher.LoadBee(hive.GetBee());
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
