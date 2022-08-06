using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreator : MonoBehaviour
{

    [SerializeField]
    private GameObject tilePrefab;


    public GameObject CreateTile()
    {
      GameObject tile = Instantiate(tilePrefab, new Vector3(0, 0, 0), Quaternion.identity);
      Debug.Log("Creating Tile");
      return tile;
    }

    public bool PlaceHex()
    {
      return true;
    }
}
