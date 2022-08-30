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
      if (Input.GetMouseButtonDown(0))
      {
        if (hiveIsPlaced) {
          loadBee();
        } else {
          placeHive();
          addBees(hive);
        }
      }
      else if (Input.GetMouseButton(0))
      {
        Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        RaycastHit raycastHit;
        if (Physics.Raycast(raycast, out raycastHit))
        {
          GameObject tile = raycastHit.transform.gameObject;
          Vector3 clickPosition = tile.transform.position;
          beeLauncher.SetEndDragPosition(clickPosition);
        }
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

    private void placeHive()
    {
      Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
      RaycastHit raycastHit;
      if (Physics.Raycast(raycast, out raycastHit))
      {
        GameObject tile = raycastHit.transform.gameObject;
        Debug.Log(tile);
        Vector3 hivePosition = tile.transform.position;
        Debug.Log(hivePosition);
      //  hivePosition.y += 0.5F;
        GameObject hiveObject = Instantiate(hivePrefab, hivePosition, Quaternion.identity);
        hive = hiveObject.AddComponent(typeof(Hive)) as Hive;
        hiveIsPlaced = true;
        hive.SetPosition(hivePosition);
        hiveObject.name = hive.GetId();
        Debug.Log(hiveObject.name);
        beeLauncher.SetLaunchPosition(hive.GetPosition());
       }
    }

    private void addBees(Hive hive)
    {
      for (int i = 0; i < 5; i++)
      {
        Debug.Log("HERE!");
        hive.AddBee();
      }
    }

}
