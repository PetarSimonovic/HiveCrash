using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class HiveTest
{

    private GameObject testHivePrefab;
    private Hive testHive;
    private Bee bee;

    [SetUp]
    public void SetUp()
    {
      testHivePrefab = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Hives/Hive"));
      testHive = testHivePrefab.GetComponent<Hive>();
      addFiveBeesToHive();
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
      int numberOfBees = testHive.GetBees().Count;
      Assert.AreEqual(numberOfBees, 5);
    }

    [Test]
    public void AddedBeesKnowTheirHiveId()
    {
      Bee beeOne = testHive.GetBees()[0];
      Bee beeTwo = testHive.GetBees()[1];
      Bee beeThree = testHive.GetBees()[2];
      Assert.AreEqual(testHive.GetId(), beeOne.GetHiveId());
      Assert.AreEqual(testHive.GetId(), beeOne.GetHiveId());
      Assert.AreEqual(testHive.GetId(), beeOne.GetHiveId());
    }

  [Test]
  public void CanFindAndReturnABeeThatIsNotInFlight()
  {
    Bee bee = testHive.GetBee();
    Assert.IsTrue(bee.IsInHive());
  }

  [Test]
  public void WillNotReturnAnyBeesIfAllHaveLeftHive()
  {
    foreach (Bee bee in testHive.GetBees())
    {
      bee.Fly();
    }
    Bee testBee = testHive.GetBee();
    Assert.IsNull(testBee);
  }

  // [Test]
  // public void ItCanSetItsPosition()
  // {
  //   Vector3 testPosition = new Vector3 (10, 10, 10);
  //   testHive.SetPosition(testPosition);
  //   Assert.AreEqual(testPosition, testHive.GetPosition());
  // }

  [Test]
  public void HiveCanAddPollen()
  {
    testHive.SetPollenCapacity(100);
    testHive.AddPollen(50);
    Assert.AreEqual(50, testHive.GetPollen());
  }

  [Test]
  public void HivePollenCannotExceedItsPollenCapacity()
  {
    testHive.SetPollenCapacity(100);
    testHive.AddPollen(70);
    Assert.AreEqual(70, testHive.GetPollen());
    testHive.AddPollen(1000);
    Assert.AreEqual(100, testHive.GetPollen());
  }

  [Test]
  public void HiveCanLosePollen()
  {
    testHive.SetPollenCapacity(100);
    testHive.RemovePollen(30);
    Assert.AreEqual(70, testHive.GetPollen());
  }

  [Test]
  public void HivePollenCannotGoBelowZero()
  {
    testHive.SetPollenCapacity(100);
    testHive.RemovePollen(110);
    Assert.AreEqual(0, testHive.GetPollen());
  }

  private void addFiveBeesToHive()
  {
    for (int i = 0; i < 5; i++)
     {
       var bee = new Bee(testHive.GetId());
       testHive.AddBee();
     }
  }

}
