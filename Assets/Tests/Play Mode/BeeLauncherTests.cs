using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;


public class BeeLauncherTests
{
    private BeeLauncher beeLauncher;
    private GameObject beeLauncherObject;
    private Bee testBee;
    private Vector3 launchPosition;
    private Vector3 endPosition;

    [SetUp]
    public void SetUp()
    {
        setUpBeeLauncher();
        setUpTestBee();
        setLaunchPosition();
        setLaunchPosition();
    }

    [TearDown]
    public void Teardown()
    {
        Object.Destroy(beeLauncherObject);
    }

    [Test]
    public void ItCanLoadABee()
    {
        Assert.IsFalse(beeLauncher.IsLoaded());
        beeLauncher.LoadBee(testBee);
        Assert.IsTrue(beeLauncher.IsLoaded());
    }

    [Test]
    public void ItCanGetALoadedBee()
    {
        beeLauncher.LoadBee(testBee);
        Bee loadedBee = beeLauncher.GetLoadedBee();
        Assert.AreEqual(testBee,loadedBee);

    }

    [Test]
    public void ItCanLaunchABeeBody()
    {
        beeLauncher.LoadBee(testBee);
        Assert.IsNull(testBee.GetBody());
        beeLauncher.LaunchBee();
        Assert.IsInstanceOf<GameObject>(testBee.GetBody());
    }

     [Test]
    public void ItCanTellABeeToFly()
    {
        beeLauncher.LoadBee(testBee);
        beeLauncher.LaunchBee();
        Assert.IsFalse(testBee.IsInHive());
    }


    [Test]
    public void ItCanSetItsLaunchPositionAndAdjustsForYPositionLock()
    {
        Vector3 launchPosition = new Vector3 (10, 10, 0);
        Vector3 launchPositionYOffset = new Vector3(launchPosition.x, launchPosition.y + beeLauncher.GetLaunchPositionY(), launchPosition.z);
        beeLauncher.SetLaunchPosition(launchPosition);
        Assert.AreEqual(launchPositionYOffset, beeLauncher.GetLaunchPosition());
    }

    [Test]
    public void ItCanSetItsEndPositionAndAdjustsForYPositionLock()
    {
        Vector3 endPosition = new Vector3 (10, 10, 0);
        Vector3 endPositionYOffset = new Vector3(endPosition.x, endPosition.y + beeLauncher.GetLaunchPositionY(), endPosition.z);
        beeLauncher.SetEndPosition(endPosition);
        Assert.AreEqual(endPositionYOffset, beeLauncher.GetEndPosition());
    }

    [Test]
    public void IsNotLoadedAfterBeeIsLaunched()
    {
        beeLauncher.LoadBee(testBee);
        beeLauncher.LaunchBee();
        Assert.IsFalse(beeLauncher.IsLoaded());
    }


    [Test]
    public void EndPositionIsTheSameAsLaunchPositionAfterBeeIsLaunched()
    {
        beeLauncher.LoadBee(testBee);
        beeLauncher.LaunchBee();
        Assert.AreEqual(beeLauncher.GetLaunchPosition(), beeLauncher.GetEndPosition());
    }



    // Initalisation methods

    private void setUpBeeLauncher()
    {
        beeLauncherObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Bees/BeeLaunchers/BeeLauncher"));
        beeLauncher = beeLauncherObject.GetComponent<BeeLauncher>();
    }

    private void setUpTestBee()
    {
        testBee = new Bee("1");
    }

    private void setLaunchPosition()
    {
        launchPosition = new Vector3 (10, 10, 0);
        beeLauncher.SetLaunchPosition(launchPosition);
       
    }

    private void setEndPosition()
    {
        endPosition = new Vector3 (0, 10, 0);
        beeLauncher.SetEndPosition(endPosition);
    }


}



