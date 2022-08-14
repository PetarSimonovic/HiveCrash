using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreator : MonoBehaviour
{

    [SerializeField]
    private GameObject fogTilePrefab;

    [SerializeField]
    private GameObject borderTilePrefab;

    private const float WidthOfTile = 1f;
    private const float StartingXPosition = 0f;
    private const float StartingZPosition = 0f;
    private const int NumberOfRows = 30;
    private const int NumberOfColumns = 9;

    public void Awake()
    {

    }


    public void CreateMap()
    {
      float xPosition = StartingXPosition;
      float zPosition = StartingZPosition;
      CreateColumn(xPosition - WidthOfTile/2, zPosition - WidthOfTile/4, borderTilePrefab);
      for(int i = 0; i < NumberOfColumns; i ++)
      {
        CreateColumn(xPosition, zPosition, fogTilePrefab);
        xPosition += WidthOfTile/2;
        zPosition = zPosition == 0 ? WidthOfTile/4 : 0;
      }
      CreateColumn(xPosition, zPosition - WidthOfTile/2, borderTilePrefab);
    }

    public void CreateColumn(float xPosition, float zPosition, GameObject tilePrefab)
    {
      CreateTile(new Vector3(xPosition, 0, zPosition - WidthOfTile/2), borderTilePrefab);
      for (float i = 0; i < NumberOfRows; i++)
      {
        CreateTile(new Vector3(xPosition, 0, zPosition), tilePrefab);
        zPosition += WidthOfTile/2;
      }
      CreateTile(new Vector3(xPosition, 0, zPosition), borderTilePrefab);
    }


    public GameObject CreateTile(Vector3 position, GameObject tilePrefab)
    {
      GameObject tile = Instantiate(tilePrefab, position, Quaternion.identity);
      tile.SetActive(true);
      return tile;
    }
}
