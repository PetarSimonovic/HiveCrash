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
    private GameObject hivePrefab;
    private Hive hive;

    [SetUp]
    public void SetUp()
    {
        setUpBeeLauncher();
        setUpTestBee();
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
    public void IsNotLoadedAfterBeeIsLaunched()
    {
        beeLauncher.LoadBee(testBee);
        beeLauncher.LaunchBee();
        Assert.IsFalse(beeLauncher.IsLoaded());
    }






    // Initalisation methods

    private void setUpBeeLauncher()
    {
        beeLauncherObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Bees/BeeLaunchers/BeeLauncher"));
        beeLauncher = beeLauncherObject.GetComponent<BeeLauncher>();
        hivePrefab = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Hives/Hive"));
        beeLauncher.SetHive(hivePrefab.GetComponent<Hive>());
    }

    private void setUpTestBee()
    {
        testBee = new Bee("1");
    }


    private void setEndPosition()
    {
        endPosition = new Vector3 (0, 10, 0);
        beeLauncher.SetEndPosition(endPosition);
    }


}



