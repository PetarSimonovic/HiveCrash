using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    MapCreator mapCreator;

    [SerializeField]
    GameObject hivePrefab;

    void Start()
    {
      mapCreator.CreateMap();

    }

    // Update is called once per frame
    void Update()
    {
      checkInput();
    }

    private void checkInput()
    {

      if (Input.GetMouseButtonDown(0))
      {
        Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        RaycastHit raycastHit;
        if (Physics.Raycast(raycast, out raycastHit))
        {
          GameObject tile = raycastHit.transform.gameObject;
          Vector3 hivePosition = tile.transform.position;
          Debug.Log(tile);
          Destroy(tile);
          GameObject hive = Instantiate(hivePrefab, hivePosition, Quaternion.identity);
         }

      }
    }
}
