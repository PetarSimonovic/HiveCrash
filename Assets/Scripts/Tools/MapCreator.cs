using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreator : MonoBehaviour
{

    [SerializeField]
    private GameObject meadowTilePrefab;

    [SerializeField]
    private GameObject rockTilePrefab;

    [SerializeField]
    private GameObject lakeTilePrefab;

    private List<GameObject> tiles = new List<GameObject>();

    private List<List<int>> map = new List<List<int>>(); 

    bool usePremadeMaps = true;

    private Cartographer cartographer;

    private const float WidthOfTile = 1f;
    private const float StartingXPosition = 0f;
    private const float StartingZPosition = 0f;
    private const int NumberOfRows = 9;
    private const int NumberOfColumns = 9;
    private const int RockFrequency = 1;


    public void CreateMap()
    {
      if (usePremadeMaps) 
      {
        drawMap();
      } 
      else 
      {
        generateMap();
      }
    }

    private void drawMap() 
    {
      cartographer = GetComponent<Cartographer>();
      map = cartographer.GetHexMap();
      generateMap(map);
    }

    private void generateMap(List<List<int>> map) 
    {
      float xPosition = StartingXPosition;
      float zPosition = StartingZPosition;
      int columnNumber = 0;
   //   CreateColumn(xPosition - WidthOfTile/2, zPosition - WidthOfTile/4, lakeTilePrefab);
      foreach (List<int> column in map)
      {
        CreateColumn(xPosition, zPosition, column, columnNumber);
        xPosition += WidthOfTile/2;
        zPosition = zPosition == 0 ? WidthOfTile/4 : 0;
        columnNumber++;
      }
    // CreateColumn(xPosition, zPosition - WidthOfTile/2, lakeTilePrefab);
    }

    private void generateMap() 
    {
      float xPosition = StartingXPosition;
      float zPosition = StartingZPosition;
   //   CreateColumn(xPosition - WidthOfTile/2, zPosition - WidthOfTile/4, lakeTilePrefab);
      for(int column = 0; column < NumberOfColumns; column ++)
      {
        CreateColumn(xPosition, zPosition, column);
        xPosition += WidthOfTile/2;
        zPosition = zPosition == 0 ? WidthOfTile/4 : 0;
      }
    // CreateColumn(xPosition, zPosition - WidthOfTile/2, lakeTilePrefab);
    }

      public void CreateColumn(float xPosition, float zPosition, List<int> column, int columnNumber)
      {
        int row = 0;
        foreach (int tileType in column)
        {
          GameObject tilePrefab = tileType == 0 ? lakeTilePrefab : chooseRandomTile();
          GameObject tile = CreateTile(new Vector3(xPosition, 0, zPosition), tilePrefab);
          tile.GetComponent<Tile>().row = row;
          tile.GetComponent<Tile>().column = columnNumber;
          if (tileType == 0) {configureLakeBorderTile(tile);}
          zPosition += WidthOfTile/2;
          row++;
        }
    //   CreateTile(new Vector3(xPosition, 0, zPosition), lakeTilePrefab);
      }

      public void configureLakeBorderTile(GameObject tileGameObject)
      {
        Tile tile = tileGameObject.GetComponent<Tile>();
        tile.Reveal();
        tile.SetBorderTile();
      }

    

    public void CreateColumn(float xPosition, float zPosition, int column)
    {
   //   CreateTile(new Vector3(xPosition, 0, zPosition - WidthOfTile/2), lakeTilePrefab);
      GameObject tilePrefab;
      for (int row = 0; row < NumberOfRows; row++)
      
      {
        tilePrefab = chooseRandomTile();
        GameObject tile = CreateTile(new Vector3(xPosition, 0, zPosition), tilePrefab);
        tile.GetComponent<Tile>().row = row;
        tile.GetComponent<Tile>().column = column;
        zPosition += WidthOfTile/2;
      }
  //   CreateTile(new Vector3(xPosition, 0, zPosition), lakeTilePrefab);
    }


    public GameObject CreateTile(Vector3 position, GameObject tilePrefab)
    {
      GameObject tile = Instantiate(tilePrefab, position, Quaternion.identity);
      tile.SetActive(true);
      this.tiles.Add(tile);
      return tile;
    }

    private GameObject chooseRandomTile()
    {
      if (Globals.test) {
        return meadowTilePrefab;
      }
      int tileDecision = Random.Range(0, 10);
      switch(tileDecision)
      {
        case 1:
          return rockTilePrefab;
        case 2:
          return lakeTilePrefab;
        default:
          return meadowTilePrefab;
      }
    }

    public GameObject GetTile(string tile)
    {
      switch(tile)
      {
        case "rock":
          return rockTilePrefab;
        case "lake":
          return lakeTilePrefab;
        default:
          return meadowTilePrefab;
      }
    }
  

    public List<GameObject> GetTiles()
    {
      return this.tiles;
    }

    public void DestroyTile(GameObject tile)
    {
      tiles.Remove(tile);
      Destroy(tile);
    }

    public GameObject GetMeadowTilePrefab() 
    {
      return this.meadowTilePrefab;

    }

    public void SurroundHiveWithMeadows(int hiveColumn, int hiveRow) {

      List<Coordinates> adjacentTiles = gethiveAdjacentTiles(hiveColumn);
      adjacentTiles.ForEach(coordinates => {
        GameObject tileObject = tiles.Find(
          tileObject => 
          tileObject.GetComponent<Tile>().row == (hiveRow + coordinates.row) &&  
          tileObject.GetComponent<Tile>().column == (hiveColumn + coordinates.column));
          if (!tileObject.GetComponent<Tile>().IsBorderTile()) {
            Vector3 tilePosition = tileObject.transform.position;
            DestroyTile(tileObject);
            GameObject newMeadow = CreateTile(tilePosition, meadowTilePrefab);
            newMeadow.GetComponent<Tile>().Reveal();
          }
      });


    }

    private List<Coordinates> gethiveAdjacentTiles(int column) {

        bool even = column % 2  == 0 ? true : false;
        List<Coordinates> adjacentHives = new List<Coordinates>();


        switch (even) {
          case true:
            adjacentHives.Add(new Coordinates(0, 1));
            adjacentHives.Add(new Coordinates(1, 0));
            adjacentHives.Add(new Coordinates(1,  -1));
            adjacentHives.Add(new Coordinates(0, -1));
            adjacentHives.Add(new Coordinates(-1, -1));
            adjacentHives.Add(new Coordinates(-1, 0));
            break;
          default:
            adjacentHives.Add(new Coordinates(0, 1));
            adjacentHives.Add(new Coordinates(1, 1));
            adjacentHives.Add(new Coordinates(1, 0));
            adjacentHives.Add(new Coordinates(-1, 0));
            adjacentHives.Add(new Coordinates(0,-1));
            adjacentHives.Add(new Coordinates(-1, 1));

            break;
        }
        return adjacentHives;
    }
}

class Coordinates {
  public int row;
  public int column;

  public Coordinates(int column, int row)
  {
    this.column = column;
    this.row = row;
  }

}