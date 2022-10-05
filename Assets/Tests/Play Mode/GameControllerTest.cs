using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class GameControllerTest
{

    private GameObject gameControllerObject;
    private GameController gameController;
    private Vector3 tilePosition;
    private Vector3 worldTouchPoint;

    [SetUp]
    public void SetUp()
    {
        gameControllerObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Controllers/GameController"));
        gameController = gameControllerObject.GetComponent<GameController>();
    }

      
    [TearDown]
    public void Teardown()
    {
        Object.Destroy(gameControllerObject);
    }

    [Test]
    public void GameControllerHasACollectionOfTiles()
    {
    //   gameController.createMap();
    //   int tiles = gameController.GetTiles().Count;
    //   Assert.NotNull(tiles);
    //   Assert.AreNotEqual(0, tiles);
    }

}
