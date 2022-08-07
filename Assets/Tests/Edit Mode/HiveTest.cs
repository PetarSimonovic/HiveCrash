using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class HiveTest
{

    private GameObject testHiveGameObject = new GameObject();
    private Hive testHive;

    [SetUp]
    public void SetUp()
    {
      testHive = testHiveGameObject.AddComponent(typeof(Hive)) as Hive;
    }

    [Test]
      public void HiveKnowsWhenItsNotPlaced()
      {
        Assert.IsFalse(testHive.IsPlaced());
      }

    [Test]
      public void HiveKnowsWhenItsPlaced()
      {
        testHive.Place();
        Assert.IsTrue(testHive.IsPlaced());
      }

}
