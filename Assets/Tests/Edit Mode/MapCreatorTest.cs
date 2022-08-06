using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class MapCreatorTest
{

    private GameObject mapCreatorGameObject = new GameObject();
    private MapCreator mapCreator;
    private Vector3 testCoordinate;

    [SetUp]
    public void SetUp()
    {
      mapCreator = mapCreatorGameObject.AddComponent(typeof(MapCreator)) as MapCreator;
      Vector3 testCoordinate = new Vector3 (10, 10, 5);
    }

    [Test]
    public void CreatesATile()
    {
     GameObject tile = mapCreator.CreateTile(testCoordinate);
     Assert.IsNotNull(tile);
    }

    [Test]
    public void CreatesATileAtAGivenCoordinate()
    {
     GameObject tile = mapCreator.CreateTile(testCoordinate);
     Assert.AreEqual(testCoordinate, tile.transform.position);
    }

}
