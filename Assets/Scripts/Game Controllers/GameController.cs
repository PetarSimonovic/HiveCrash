using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
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
    private PlayerBeeLauncher beeLauncher;

    [SerializeField]
    private BeeController beeController;

    [SerializeField]
    private FlowerController flowerController;

    [SerializeField]
    private EnemyController enemyController;

    [SerializeField]
    private DisplayController displayController;

    private Hive hive;

    private List<GameObject> tiles;

    private bool hiveIsPlaced;

    private Touch touch;


    private void Awake() 
    {
      Application.targetFrameRate = 60;
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
        checkHive();
      }
    }

    private void checkInput()
    {
      if (Input.touchCount > 0)
      {
        touch = Input.GetTouch(0);
          processInput();
      }
    }

    private void processInput()
    {
      Vector3 touchPosition = touch.position;
      Ray raycast = Camera.main.ScreenPointToRay(touchPosition);
      Vector3 worldTouchPoint = Camera.main.ScreenToWorldPoint(touchPosition);
      RaycastHit raycastHit;
      GameObject tile;
      if (Physics.Raycast(raycast, out raycastHit))
      {
        tile = getTile(raycastHit);
        if (tile.layer == 14) {return;}
        if (Globals.test) {tile.GetComponent<Tile>().PrintPosition();}
        if (tile.tag == "hive") {
          Debug.Log(tile.tag);
          processMapInput(tile.transform.position, raycastHit.point);
        }
        else if (tile.GetComponent<Tile>().IsBorderTile() && !hiveIsPlaced) 
        {
          Debug.Log("Clicked on border");
        } 
        else 
        {
          processMapInput(tile.transform.position, raycastHit.point);
        }
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
      switch(touch.phase)
      {
      case TouchPhase.Began:
        processTouchOnMap(tilePosition);
        break;
      case TouchPhase.Moved:
          beeLauncher.SetEndPosition(worldTouchPoint);
        break;
      case TouchPhase.Ended:
          beeLauncher.LaunchBee();
        break;
      }
    }

    private void processCameraInput(Vector3 touchPosition)
    {
      switch (touch.phase)
        {
          case TouchPhase.Began:
            cameraController.SetStartTouchPosition(touchPosition);
            break;
          case TouchPhase.Moved: 
            cameraController.ProcessTouch(touchPosition);
            break;
          default:
            cameraController.ProcessTouch(touchPosition);
            break;
        }
      }

    private void processTouchOnMap(Vector3 clickPosition)
    {
      switch (hiveIsPlaced) 
        {
        case false:
          initialiseHive(clickPosition);
          initialiseEnemies();
          updateFlowerController();
          break;
        default:
          beeLauncher.LoadBee(hive.GetBee());
          break;
        }
    }

    private void initialiseHive(Vector3 hivePosition)
    {
      Hive hive = createHive(hivePosition);
      initialiseBeeController();
    }

    private void initialiseEnemies()
    {
      enemyController.SetPlayerHive(hive);
      enemyController.PlaceEnemyTile(tiles);
    }

    private Hive createHive(Vector3 hivePosition)
    {
      GameObject chosenTile = getTileBeneathHive(hivePosition);
      int row = chosenTile.GetComponent<Tile>().row;
      int column = chosenTile.GetComponent<Tile>().column;
      mapCreator.DestroyTile(chosenTile);
      GameObject tile = mapCreator.CreateTile(hivePosition, mapCreator.GetTile("meadow"));
      tile.tag = "playerHiveTile";
      tile.GetComponent<Tile>().row = row;
      tile.GetComponent<Tile>().column = column;
      hivePosition = new Vector3(hivePosition.x, (tile.transform.position.y + (tile.GetComponent<Tile>().GetHeight()/4)), hivePosition.z);
      GameObject hiveObject = Instantiate(hivePrefab, hivePosition, Quaternion.identity);
      hive = hiveObject.GetComponent<Hive>();
      hiveIsPlaced = true;
      hive.SetPosition(hivePosition);
      hiveObject.name = hive.GetId();
      Debug.Log("Hive is on: ");
      tile.GetComponent<Tile>().PrintPosition();
      beeLauncher.SetHive(hive);
      mapCreator.SurroundHiveWithMeadows(column, row);
      flowerController.SetHivePosition(hivePosition);

      return hive;
    }

    private GameObject getTileBeneathHive(Vector3 hivePosition) 
    {
      
      return tiles.Find(tile => tile.transform.position == hivePosition);

    }

  

    private void checkControllers()
    {
      flowerController.PlantRevealedMeadows();
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
      restartGame();
    }

    private void initialiseBeeController()
    {
      beeController = Instantiate(beeController);
      beeController.SetHive(hive);
      beeController.AddBees(5);
    }

    private void restartGame()
    {
      SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void instantiateObjects()
    {
      beeLauncher = Instantiate(beeLauncher);
      mapCreator = Instantiate(mapCreator);
      flowerController = Instantiate(flowerController);
      enemyController = Instantiate(enemyController);
      enemyController.SetMapCreator(this.mapCreator);
      //displayController = Instantiate(displayController);
      enemyController.SetDisplayController(displayController);
    }

    // Getters and Setters

    public List<GameObject> GetTiles() 
    {
      return this.tiles;
    }

    public void checkHive() 
    {
      if (hive.HasCrashed()) 
      {
        hive.LaunchTextBubble("HiveCrash", false);
        restartGame();
      }
    }


}
