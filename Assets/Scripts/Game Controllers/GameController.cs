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
    private EnemyController enemyController;

    [SerializeField]
    private DisplayController displayController;

    private Hive hive;

    private bool hiveIsPlaced;

    private Touch touch;

    private bool gameIsOver = false;

    private bool gameStarted = false;

    
    private void Awake()
    {
    //  Application.targetFrameRate = 60;
      instantiateObjects();


    }

    private void Start()
    {
        Debug.Log("Starting level");
        StartCoroutine(StartLevel());
    }

      private void Update()
    {
      checkGameInput();
      if (hiveIsPlaced) {
        checkIfLevelIsComplete();
        checkHive();
      }
    }



    public IEnumerator StartLevel()
    {
      Debug.Log("In start level");
      mapCreator.ClearTiles();
      while (mapCreator.GetTiles().Count > 0) 
      {
        Debug.Log("waiting for tiles to clear");
        yield return null;
      }
      mapCreator.CreateMap();
      while (!mapCreator.IsMapCreated()) 
      {
          Debug.Log("waiting for map creation");
          yield return null;
      }
      gameStarted = true;
    }
  
    private void checkGameInput()
    {
      if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0))
      {
          processInput();
      }
    }

    private void processInput()
    {

      if (gameIsOver) {
        goToTitleScene();
        return;
        }
     
   
      Vector3 touchPosition = Input.mousePosition;
      Ray raycast = cameraController.GetCamera().ScreenPointToRay(touchPosition);
      Vector3 worldTouchPoint = cameraController.GetCamera().ScreenToWorldPoint(touchPosition);
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

      if (!hiveIsPlaced)
      {
          initialiseHive(tilePosition);
          initialiseEnemies();
          return;
      }
      if (Input.GetMouseButtonDown(0))
      {
        beeLauncher.LoadBee(hive.GetBee());
        return;
      }
      if (Input.GetMouseButton(0)) 
      {
          beeLauncher.SetEndPosition(worldTouchPoint);
        return;
      }
      if (Input.GetMouseButtonUp(0)) 
      {
          beeLauncher.LaunchBee();
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

    private void initialiseHive(Vector3 hivePosition)
    {
      int starterBees = 5;
      Hive hive = createHive(hivePosition);
      for (int i = 0; i < starterBees; i++)
      {
        hive.AddBee();
      }
    }

    private void initialiseEnemies()
    {
      enemyController.SetPlayerHive(hive);
      enemyController.PlaceEnemyTile(mapCreator.GetTiles());
    }

    private Hive createHive(Vector3 hivePosition)
    {
      Debug.Log(hivePosition);
      hivePosition.y = 0.0f;
      GameObject chosenTile = mapCreator.GetTileAtPosition(hivePosition);
      Debug.Log(chosenTile);
      int row = chosenTile.GetComponent<Tile>().row;
      int column = chosenTile.GetComponent<Tile>().column;
      mapCreator.DestroyTile(chosenTile);
      GameObject tile = mapCreator.CreateTile(hivePosition, mapCreator.GetTile("meadow"));
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

      return hive;
    }
  

    private void createMap(bool usePremadeMaps = true)
    {
      mapCreator.CreateMap(usePremadeMaps);
    }

  

    private void checkIfLevelIsComplete()
    {
      if (!enemyController.EnemyHasCrashed()) return;
      gameIsOver = true;
      hive.LaunchTextBubble("Garden is secure");
      goToTitleScene();
    }

    private void goToTitleScene()
    {
      StartCoroutine(LoadSceneAfterDelay(2));
    }

    private void instantiateObjects()
    {
      mapCreator = Instantiate(mapCreator);

      beeLauncher = Instantiate(beeLauncher);
      enemyController = Instantiate(enemyController);
      enemyController.SetMapCreator(this.mapCreator);
      // displayController = Instantiate(displayController);
      enemyController.SetDisplayController(displayController);
    }

    // Getters and Setters

    public List<GameObject> GetTiles() 
    {
      return mapCreator.GetTiles();
    }

    public void checkHive() 
    {
      if (hive.HasCrashed()) 
      {
        hive.LaunchTextBubble("HiveCrash", false);
        gameIsOver = true;

      }
    }

    public void SetCameraController(CameraController cameraController) 
    {
      this.cameraController = cameraController;
    }

    public bool isGameOver() 
    {
      return gameIsOver;
    }
    
        public IEnumerator LoadSceneAfterDelay(int seconds)
        {
        
            yield return new WaitForSecondsRealtime(seconds);
            SceneManager.LoadScene("TitleScene");

        }

}
