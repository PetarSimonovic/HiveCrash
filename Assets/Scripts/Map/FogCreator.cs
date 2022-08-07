using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogCreator : MonoBehaviour
{

    [SerializeField]
    private GameObject fogTilePrefab;

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
      for(int i = 0; i < NumberOfColumns; i ++)
      {
        CreateColumn(xPosition, zPosition);
        xPosition += WidthOfTile/2;
        zPosition = zPosition == 0 ? WidthOfTile/4 : 0;
      }

    }

    public void CreateColumn(float xPosition, float zPosition)
    {
      for (float i = 0; i < NumberOfRows; i++)
      {
        CreateTile(new Vector3(xPosition, 0, zPosition));
        zPosition += WidthOfTile/2;
      }
    }


    public GameObject CreateTile(Vector3 position)
    {
      GameObject tile = Instantiate(fogTilePrefab, position, Quaternion.identity);
      tile.SetActive(true);
      return tile;
    }
}
