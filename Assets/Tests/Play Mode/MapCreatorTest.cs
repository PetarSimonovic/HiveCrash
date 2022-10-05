using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class MapCreatorTest
{
    private GameObject mapCreatorObject;
    private MapCreator mapCreator;

     [SetUp]
    public void SetUp()
    {
        mapCreatorObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Tools/MapCreator"));
        mapCreator = mapCreatorObject.GetComponent<MapCreator>();

        // gameControllerObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Controllers/GameController"));
        // gameController = gameControllerObject.GetComponent<GameController>();
    }


    [Test]
    public void MapCreatorGeneratesACollectionOfTiles()
    {
        List<GameObject> tiles = mapCreator.GetTiles();
        Assert.AreEqual(0, tiles.Count);
        mapCreator.CreateMap();
        tiles = mapCreator.GetTiles();
        Assert.AreNotEqual(0, tiles.Count);
    }

   
}
