using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private MapCreator mapCreator;

    [SerializeField]
    private CameraController cameraController;

    [SerializeField]
    private GameObject hivePrefab;

    [SerializeField]
    private BeeLauncher beeLauncher;

    [SerializeField]
    private BeeController beeController;

    [SerializeField]
    private FlowerController flowerController;

    private Hive hive;

    private List<GameObject> tiles;

    private bool hiveIsPlaced;

    private void Awake() 
    {
     instantiateObjects();
    }

    private void Start()
    {
      createMap();
    }

    // Update is called once per frame
    private void Update()
    {
      checkIfMapIsComplete();
      checkInput();
      if (hiveIsPlaced) {
        checkControllers();
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
      Vector3 touchPosition = Input.GetTouch(0).position;
      Ray raycast = Camera.main.ScreenPointToRay(touchPosition);
      Vector3 worldTouchPoint = Camera.main.ScreenToWorldPoint(touchPosition);
      RaycastHit raycastHit;
      GameObject tile;
      if (Physics.Raycast(raycast, out raycastHit))
      {
        tile = getTile(raycastHit);
        processMapInput(tile.transform.position, raycastHit.point);
      }
      else 
      {
        processCameraInput(touchPosition);
      }
    }

    private GameObject getTile(RaycastHit raycastHit)
    {
      return raycastHit.transform.gameObject;
    }

    private void processMapInput(Vector3 tilePosition, Vector3 worldTouchPoint)
    {
      if (Input.GetMouseButtonDown(0))
      {
        processClickOnMap(tilePosition);
      }
      else if (Input.GetMouseButton(0))
      {
        if (beeLauncher.IsLoaded())
        {
          beeLauncher.SetEndDragPosition(worldTouchPoint);
        }
      }
      else if (Input.GetMouseButtonUp(0) && beeLauncher.IsLoaded())
      {
        beeLauncher.LaunchBee();
      }
    }

    private void processCameraInput(Vector3 touchPosition)
    {
       if (Input.GetMouseButtonDown(0))
      {
        cameraController.SetStartTouchPosition(touchPosition);
      }
      else if (Input.GetMouseButton(0))
      {
      cameraController.ProcessTouch(touchPosition);
      }
    }

    private void processClickOnMap(Vector3 clickPosition)
    {
      if (hiveIsPlaced) 
        {
          loadBee();
        } else 
        {
          initaliseHive(clickPosition);
          updateFlowerController();
        }
    }

    private void loadBee()
    {
      Bee bee = hive.GetBee();
      if (bee is not null) {
        Debug.Log(bee);
        beeLauncher.LoadBee(bee);
      }
    }

    private void initaliseHive(Vector3 hivePosition)
    {
      Hive hive = createHive(hivePosition);
    }

    private Hive createHive(Vector3 hivePosition)
    {
      destroyTileBeneathHive(hivePosition);
      GameObject hiveObject = Instantiate(hivePrefab, hivePosition, Quaternion.identity);
      hive = hiveObject.AddComponent(typeof(Hive)) as Hive;
      hiveIsPlaced = true;
      hive.SetPosition(hivePosition);
      hiveObject.name = hive.GetId();
      beeLauncher.SetLaunchPosition(hive.GetPosition());
      beeController = Instantiate(beeController);
      beeController.SetHive(hive);
      beeController.AddBees(5);

      return hive;
    }

    private void destroyTileBeneathHive(Vector3 hivePosition)
    {
      foreach (GameObject tile in tiles)
      {
        if (tile.transform.position == hivePosition)
          {
            tiles.Remove(tile);
            Destroy(tile);
            break;
          }
      }
    }

    private void instantiateObjects()
    {
      beeLauncher = Instantiate(beeLauncher);
      mapCreator = Instantiate(mapCreator);
      flowerController = Instantiate(flowerController);
    }

    private void checkControllers()
    {
      flowerController.CheckMeadows();
    }

    private void createMap()
    {
      mapCreator.CreateMap();
      tiles = mapCreator.GetTiles();
    }

    private void updateFlowerController()
    {
      flowerController.SetMeadows(tiles);
    }

    private void checkIfMapIsComplete()
    {
      foreach (GameObject tile in tiles)
      {
        if (tile.GetComponent<Tile>().IsHidden() == true)
        {
          return;
        }
      }
      Debug.Log("Map Complete");
    }

}
