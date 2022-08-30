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
    private GameObject borderTilePrefab;

    private const float WidthOfTile = 1f;
    private const float StartingXPosition = 0f;
    private const float StartingZPosition = 0f;
    private const int NumberOfRows = 30;
    private const int NumberOfColumns = 9;
    private const int RockFrequency = 1;

    public void Awake()
    {

    }


    public void CreateMap()
    {
      float xPosition = StartingXPosition;
      float zPosition = StartingZPosition;
    //  CreateColumn(xPosition - WidthOfTile/2, zPosition - WidthOfTile/4, borderTilePrefab);
      for(int i = 0; i < NumberOfColumns; i ++)
      {
        CreateColumn(xPosition, zPosition);
        xPosition += WidthOfTile/2;
        zPosition = zPosition == 0 ? WidthOfTile/4 : 0;
      }
    //  CreateColumn(xPosition, zPosition - WidthOfTile/2, borderTilePrefab);
    }

    public void CreateColumn(float xPosition, float zPosition)
    {
  //    CreateTile(new Vector3(xPosition, 0, zPosition - WidthOfTile/2), borderTilePrefab);
      GameObject tilePrefab;
      int tileDecision;
      for (int i = 0; i < NumberOfRows; i++)
      {
        tileDecision = Random.Range(0, 10);
        tilePrefab =  tileDecision == RockFrequency ? rockTilePrefab : meadowTilePrefab;
        CreateTile(new Vector3(xPosition, 0, zPosition), tilePrefab);
        zPosition += WidthOfTile/2;
      }
  //    CreateTile(new Vector3(xPosition, 0, zPosition), borderTilePrefab);
    }


    public GameObject CreateTile(Vector3 position, GameObject tilePrefab)
    {
      GameObject tile = Instantiate(tilePrefab, position, Quaternion.identity);
      tile.SetActive(true);
      return tile;
    }
}
