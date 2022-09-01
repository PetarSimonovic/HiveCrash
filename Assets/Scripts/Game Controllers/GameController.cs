using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    MapCreator mapCreator;

    [SerializeField]
    GameObject hivePrefab;

    [SerializeField]
    BeeLauncher beeLauncher;

    [SerializeField]
    BeeController beeController;

    Hive hive;

    private bool hiveIsPlaced;

    void Start()
    {
      mapCreator.CreateMap();
    }

    // Update is called once per frame
    void Update()
    {
      checkInput();
      if (hiveIsPlaced) {
        beeController.CheckBees(hive.GetBees());
      }
    }

    private void checkInput()
    {
      if (Input.anyKey || Input.anyKeyDown || Input.GetMouseButtonUp(0))
      {
        processInput();
      }
    }

    private void processInput()
    {
      Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
      RaycastHit raycastHit;
      GameObject tile;
      if (Physics.Raycast(raycast, out raycastHit))
      {
        tile = getTile(raycastHit);
        processMapInput(tile.transform.position);
      }
      else 
      {
        Debug.Log("CAMERA INPUT");
      }
    }

    private GameObject getTile(RaycastHit raycastHit)
    {
      return raycastHit.transform.gameObject;
    }

    private void processMapInput(Vector3 clickPosition)
    {
      if (Input.GetMouseButtonDown(0))
      {
        if (hiveIsPlaced) 
        {
          loadBee();
        } else {
          initaliseHive(clickPosition);
        }
      }
      else if (Input.GetMouseButton(0))
      {
        beeLauncher.SetEndDragPosition(clickPosition);
      }
      else if (Input.GetMouseButtonUp(0) && beeLauncher.IsLoaded())
      {
        beeLauncher.LaunchBee();
      }
    }

    private void loadBee()
    {
      Bee bee = hive.GetBee();
      if (bee is not null) {
        beeLauncher.LoadBee(bee);
      }
    }

    private void initaliseHive(Vector3 hivePosition)
    {
      Hive hive = createHive(hivePosition);
      addBees(hive, 5);
    }

    private Hive createHive(Vector3 hivePosition)
    {
      
    //  hivePosition.y += 0.5F;
      GameObject hiveObject = Instantiate(hivePrefab, hivePosition, Quaternion.identity);
      hive = hiveObject.AddComponent(typeof(Hive)) as Hive;
      hiveIsPlaced = true;
      hive.SetPosition(hivePosition);
      hiveObject.name = hive.GetId();
      beeLauncher.SetLaunchPosition(hive.GetPosition());
      return hive;
    }

    private void addBees(Hive hive, int beeCount)
    {
      for (int i = 0; i < beeCount; i++)
      {
        hive.AddBee();
      }
    }

    private void getClickPosition()
    {

    }

}
