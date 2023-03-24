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
    

    void Update()
    {
        // if (hiveIsPlaced) 
        // {
        //     if (!activated)
        //     {
        //         activate();            
        //     }
        //     checkTimer();
        // }
    }


    public void PlaceHive(Vector3 tilePosition)
    {
        tilePosition.y = 5f;
        hiveObject = Instantiate(hiveObject, tilePosition, Quaternion.identity);
        hiveObject.GetComponent<Hive>().titleHive = true;
        
    }

    public void DestroyHive()
    {
        Destroy(hiveObject);
    }
}
