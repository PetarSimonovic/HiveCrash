using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class HiveTest
{

    private Hive testHive;

    [SetUp]
    public void SetUp()
    {
      testHive = new Hive();
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
