using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    MapCreator mapCreator;
    void Start()
    {
      mapCreator.CreateRow();

    }

    // Update is called once per frame
    void Update()
    {

    }
}
