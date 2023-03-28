using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour
{
    [SerializeField]
    private GameObject staticTextBubblePrefab;
    
    [SerializeField]
    private GameObject automatedHiveController;

    [SerializeField]
    private MapCreator mapCreator;

       
    [SerializeField]
    private GameObject cameraController;
    private  GameObject hiveCrashTitle;

    private GameObject playTitle;

    private GameObject tutorialTitle;

    private Vector3 hivePosition;

    int hiveColumn = 4;
    int hiveRow = 4;

    private bool playerIsReady = false;



    // Start is called before the first frame update
    private void Awake()
    {
      Application.targetFrameRate = 60;
      instantiateObjects();
      CreateTitleScreen();


    }

    // Update is called once per frame
    void Update()
    {

      if (Input.anyKey)
      {
        StartCoroutine(LoadSceneAfterDelay(2));
      }

    }

     private void instantiateObjects()
    {
        automatedHiveController = Instantiate(automatedHiveController);

        mapCreator = Instantiate(mapCreator);


    }

    public void CreateTitleScreen() 
    {
        createTitleMap();
        createTitleText();
        initialiseHive();
    }


    private void initialiseHive()
    {
        automatedHiveController.GetComponent<AutomatedHiveController>().InitialiseHive(hivePosition);
    }

   
    private void createTitleMap() 
    {
        mapCreator.CreateMap(false);
        GameObject tile = mapCreator.GetTileAtPosition(hiveColumn, hiveRow);
        mapCreator.SurroundHiveWithMeadows(hiveColumn, hiveRow);
        Vector3 position = tile.transform.position;
        Destroy(tile);
        GameObject hiveTile = mapCreator.CreateTile(position, mapCreator.GetMeadowTilePrefab());
        hiveTile.GetComponent<Tile>().Reveal();
        hivePosition = hiveTile.transform.position;
    }

    private void createTitleText()
    {
        hiveCrashTitle = Instantiate(staticTextBubblePrefab);
        setTextProperties("HiveCrash", 90, 0f, 3.3f);
    }

    private void setTextProperties(string text, int fontSize, float xOffsetPosition, float yPosition)
    {
        hiveCrashTitle.GetComponent<StaticTextBubble>().SetText(text);
        hiveCrashTitle.GetComponent<StaticTextBubble>().SetSize(fontSize);
        hiveCrashTitle.transform.position = new Vector3(Globals.centreTile.x + xOffsetPosition, yPosition, Globals.centreTile.z);

    }

    public void SetMapCreator(MapCreator mapCreator)
    {
        this.mapCreator = mapCreator;
    }

      public IEnumerator LoadSceneAfterDelay(int seconds)
        {
        
            yield return new WaitForSecondsRealtime(seconds);
            SceneManager.LoadScene("GameScene");


        }


}
