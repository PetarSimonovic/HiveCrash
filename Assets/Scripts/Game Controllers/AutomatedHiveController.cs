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

    private float beeLaunchInterval = 8f;
    

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
        tilePosition.y = 0.5f;
        hiveObject = Instantiate(hiveObject, tilePosition, Quaternion.identity);
        hive = hiveObject.GetComponent<Hive>();
        hive.titleHive = true;
        hive.SetPosition(hiveObject.transform.position);
        Debug.Log(hive.GetPosition());
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
        Vector3 hivePosition = hive.GetPosition();
        float hivePositionX =  hivePosition.x;
        float hivePositionZ = hivePosition.z;
        beeLauncher.LoadBee(hive.GetBee());
        Vector3 target = new Vector3 (Random.Range(-(hivePositionX - 1f), (hivePositionX + 1)), 0.5f, Random.Range(-(hivePositionZ - 1), (hivePositionZ + 1)));
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
