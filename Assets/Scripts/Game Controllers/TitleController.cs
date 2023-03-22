using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleController : MonoBehaviour
{
    [SerializeField]
    private GameObject staticTextBubblePrefab;
    private AutomatedController automatedController;
    private  GameObject hiveCrashTitle;

    private GameObject playTitle;

    private GameObject tutorialTitle;

    private MapCreator mapCreator;

    private Vector3 hivePosition;

    int hiveColumn = 4;
    int hiveRow = 4;

    private bool playerIsReady = false;



    // Start is called before the first frame update
    void Awake()
    {        
        automatedController = gameObject.GetComponent<AutomatedController>();

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void CreateTitleScreen() 
    {
        createTitleMap();
        createTitleText();
        placeHive();
    }

    public void RemoveTitleScreen()
    {
        Destroy(hiveCrashTitle);
        Destroy(automatedController);
        mapCreator.ClearTiles();

    }

    private void placeHive()
    {
        automatedController.PlaceHive(hivePosition);
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
