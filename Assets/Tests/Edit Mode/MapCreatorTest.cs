using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class MapCreatorTest
{

    private GameObject mapCreatorGameObject = new GameObject();
    private MapCreator mapCreator;

    [SetUp]
    public void CreateHive()
    {
      mapCreator = mapCreatorGameObject.AddComponent(typeof(MapCreator)) as MapCreator;

    }

    [Test]
    public void CreatesATile()
    {
     GameObject tile = mapCreator.CreateTile();
     Assert.IsNotNull(tile);
    }


}
