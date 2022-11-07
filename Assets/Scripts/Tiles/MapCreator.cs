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


    //
    // [SerializeField]
    // private GameObject borderTilePrefab;

    private const float WidthOfTile = 1f;
    private const float StartingXPosition = 0f;
    private const float StartingZPosition = 0f;
    private const int NumberOfRows = 9;
    private const int NumberOfColumns = 9;
    private const int RockFrequency = 1;


    public void CreateMap()
    {
      float xPosition = StartingXPosition;
      float zPosition = StartingZPosition;
    //  CreateColumn(xPosition - WidthOfTile/2, zPosition - WidthOfTile/4, borderTilePrefab);
      for(int column = 0; column < NumberOfColumns; column ++)
      {
        CreateColumn(xPosition, zPosition, column);
        xPosition += WidthOfTile/2;
        zPosition = zPosition == 0 ? WidthOfTile/4 : 0;
      }
    //  CreateColumn(xPosition, zPosition - WidthOfTile/2, borderTilePrefab);
    }

    public void CreateColumn(float xPosition, float zPosition, int column)
    {
  //    CreateTile(new Vector3(xPosition, 0, zPosition - WidthOfTile/2), borderTilePrefab);
      GameObject tilePrefab;
      for (int row = 0; row < NumberOfRows; row++)
      {
        tilePrefab = chooseRandomTile();
        GameObject tile = CreateTile(new Vector3(xPosition, 0, zPosition), tilePrefab);
        tile.GetComponent<Tile>().row = row;
        tile.GetComponent<Tile>().column = column;
        zPosition += WidthOfTile/2;
      }
  //    CreateTile(new Vector3(xPosition, 0, zPosition), borderTilePrefab);
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
}
