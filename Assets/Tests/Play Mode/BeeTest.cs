using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class BeeTest
{
    Bee testBee;

    [SetUp]
    public void SetUp()
    {
        testBee = new Bee("1");
    }

    [Test]
    public void ItLeavesTheHiveWhenInFlight()
    {
        testBee.Fly();
        Assert.IsFalse(testBee.IsInHive());
    }

    [Test]
    public void ItCanReturnToHive()
    {
        testBee.EnterHive();
        Assert.IsTrue(testBee.IsInHive());
    }

    [Test]
    public void ItCollectsPollenAtAGivenRate()
    {
        testBee.SetPollenCollectionRate(5);
        testBee.CollectPollen();
        int pollen = testBee.GetPollen();
        Assert.AreEqual(pollen, 5);
    }

    [Test]
    public void ItCanRemoveAllOfItsPollen()
    {
        testBee.SetPollenCollectionRate(5);
        testBee.CollectPollen();
        int pollen = testBee.GetPollen();
        Assert.AreEqual(5, pollen);
        testBee.RemoveAllPollen();
        pollen = testBee.GetPollen();
        Assert.AreEqual(0, pollen);
    }

    [Test]
    public void ItCanBecomeHungry()
    {
        Assert.IsFalse(testBee.IsHungry());
        testBee.SetHunger(true);
        Assert.IsTrue(testBee.IsHungry());
    }

    [Test]
    public void ItCanLoseHealth()
    {
        int initialHealth = testBee.GetHealth();
        testBee.ReduceHealth();
        int reducedHealth = testBee.GetHealth();
        Assert.IsTrue(reducedHealth < initialHealth);
    }

    [Test]
    public void ItCanGainHealth()
    {
        int initialHealth = testBee.GetHealth();
        testBee.IncreaseHealth();
        int increasedHealth = testBee.GetHealth();
        Assert.IsTrue(increasedHealth > initialHealth);
    }


    [Test]
    public void ItCanSetAndGetMessages()
    {
        string message = "I can set and get messages";
        testBee.SetMessage(message);
        Assert.AreEqual(message, testBee.GetMessage());
    }

}
