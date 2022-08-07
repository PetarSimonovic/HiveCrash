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

    [Test]
    public void HiveHasAnId()
    {
      Assert.IsNotNull(testHive.GetId());
    }

    [Test]
    public void HiveHasBees()
    {
      Assert.IsNotNull(testHive.GetBees());
    }

    [Test]
    public void HiveCanAddBees()
    {
      testHive.AddBee();
      testHive.AddBee();
      testHive.AddBee();
      testHive.AddBee();
      testHive.AddBee();
      int numberOfBees = testHive.GetBees().Count;
      Assert.AreEqual(numberOfBees, 5);
    }

    [Test]
    public void AddedBeesKnowTheirHiveId()
    {
      testHive.AddBee();
      testHive.AddBee();
      testHive.AddBee();
      Bee beeOne = testHive.GetBees()[0];
      Bee beeTwo = testHive.GetBees()[1];
      Bee beeThree = testHive.GetBees()[2];
      Assert.AreEqual(testHive.GetId(), beeOne.GetHiveId());
      Assert.AreEqual(testHive.GetId(), beeOne.GetHiveId());
      Assert.AreEqual(testHive.GetId(), beeOne.GetHiveId());
    }

}
