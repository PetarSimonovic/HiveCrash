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
    }

    private void checkInput()
    {
      if (Input.GetMouseButtonDown(0))
      {
        if (hiveIsPlaced) {
          loadBee();
        } else {
          placeHive();
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
        Vector3 hivePosition = tile.transform.position;
        GameObject hiveObject = Instantiate(hivePrefab, hivePosition, Quaternion.identity);
        hive = hiveObject.AddComponent(typeof(Hive)) as Hive;
        hiveIsPlaced = true;
        for (int i = 0; i < 5; i++)
        {
          hive.AddBee();
        }
        hive.SetPosition(hivePosition);
        beeLauncher.SetLaunchPosition(hive.GetPosition());
       }
    }
}
