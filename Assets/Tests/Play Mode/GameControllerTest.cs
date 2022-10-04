using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class GameControllerTest
{

    private GameObject gameControllerObject;
    private GameController gameController;

    [SetUp]
    public void SetUp()
    {
        gameControllerObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Resources/Controllers/GameController"));
        gameController = gameControllerObject.GetComponent<GameController>();
    }

    // [Test]
    // public void GameControllerCreates()
    // {
    //  // Assert.IsFalse(hiveIsPlaced);
    // }

}
