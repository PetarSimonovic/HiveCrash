using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleController : MonoBehaviour
{
    [SerializeField]
    private GameObject staticTextBubblePrefab;
    
    [SerializeField]
    private GameObject automatedController;

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

    }

     private void instantiateObjects()
    {
      mapCreator = Instantiate(mapCreator);
      automatedController = Instantiate(automatedController);


    }

    public void CreateTitleScreen() 
    {
        createTitleMap();
        createTitleText();
        placeHive();
    }


    private void placeHive()
    {
        automatedController.GetComponent<AutomatedController>().PlaceHive(hivePosition);
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





}
