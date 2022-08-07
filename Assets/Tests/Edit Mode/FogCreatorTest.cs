using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class FogCreatorTest
{

    private GameObject fogCreatorGameObject = new GameObject();
    private FogCreator fogCreator;
    private Vector3 testCoordinate;

    [SetUp]
    public void SetUp()
    {
      fogCreator = fogCreatorGameObject.AddComponent(typeof(FogCreator)) as FogCreator;
      Vector3 testCoordinate = new Vector3 (10, 10, 5);
    }

    [Test]
    public void CreatesATile()
    {
     GameObject tile = fogCreator.CreateTile(testCoordinate);
     Assert.IsNotNull(tile);
    }

    [Test]
    public void CreatesATileAtAGivenCoordinate()
    {
     GameObject tile = fogCreator.CreateTile(testCoordinate);
     Assert.AreEqual(testCoordinate, tile.transform.position);
    }


}
