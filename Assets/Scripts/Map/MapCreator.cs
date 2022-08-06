using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreator : MonoBehaviour
{

    [SerializeField]
    private GameObject tilePrefab;

    private const float WidthOfTile = 0.8f;
    private const float StartingXPosition = 0f;
    private const float StartingZPosition = 0f;
    private const int NumberOfRows = 10;
    private const int NumberOfColumns = 10;

    public void Awake()
    {

    }


    public GameObject CreateTile(Vector3 position)
    {
      GameObject tile = Instantiate(tilePrefab, position, Quaternion.identity);
      tile.SetActive(true);
      return tile;
    }

    public void CreateMap()
    {
      float xPosition = StartingXPosition;
      float zPosition = StartingZPosition;
      for(int i = 0; i < NumberOfColumns; i ++)
      {
        CreateRow(xPosition, zPosition);
        xPosition -= WidthOfTile/2;
        zPosition -= WidthOfTile/4;;
      }

    }

    public void CreateRow(float xPosition, float zPosition)
    {
      for (float i = 0; i < NumberOfRows; i++)
      {
        CreateTile(new Vector3(xPosition, 0, zPosition));
        zPosition -= WidthOfTile/2;
      }
    }
}
