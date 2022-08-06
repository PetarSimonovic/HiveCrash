using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreator : MonoBehaviour
{

    [SerializeField]
    private GameObject tilePrefab;

    private const float WidthOfTile = 1;

    public void Awake()
    {

    }


    public GameObject CreateTile(Vector3 position)
    {
      GameObject tile = Instantiate(tilePrefab, position, Quaternion.identity);
      tile.SetActive(true);
      return tile;
    }


    public void CreateRow()
    {
      float zPosition = 0.0f;
      for (float i = 0; i < 10; i++)
      {
        CreateTile(new Vector3(0, 0, zPosition));
        zPosition -= WidthOfTile/2;
      }
    }
}
