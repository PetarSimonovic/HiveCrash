using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleController : MonoBehaviour
{
    [SerializeField]
    GameObject staticTextBubblePrefab;

    GameObject hiveCrashTitle;

    MapCreator mapCreator;

    private bool playerIsReady = false;



    // Start is called before the first frame update
    void Start()
    {        

    }

    public void CreateTitles() 
    {
        mapCreator.CreateMap(false);
        GameObject tile = mapCreator.GetTileAtPosition(Globals.centreTile);
        Destroy(tile);
        GameObject centreTile = mapCreator.CreateTile(Globals.centreTile, mapCreator.GetMeadowTilePrefab());
        centreTile.GetComponent<Tile>().Reveal();
        createTitleText();
    }

    public void RemoveTitles()
    {
        Destroy(hiveCrashTitle);
        mapCreator.ClearTiles();

    }


    // Update is called once per frame
    void Update()
    {

    }

   
    

    private void createTitleText()
    {
        hiveCrashTitle = Instantiate(staticTextBubblePrefab);
        hiveCrashTitle.GetComponent<StaticTextBubble>().SetText("HiveCrash");
        hiveCrashTitle.GetComponent<StaticTextBubble>().SetSize(90);
        hiveCrashTitle.transform.position = new Vector3(Globals.centreTile.x, 2f, Globals.centreTile.z);

    }

    public void SetMapCreator(MapCreator mapCreator)
    {
        this.mapCreator = mapCreator;
    }





}
