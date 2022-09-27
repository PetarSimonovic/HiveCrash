using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;


public class BeeLauncherTests
{
    private BeeLauncher beeLauncher;
    private GameObject beeLauncherObject;
    private Bee testBee;

    [SetUp]
    public void SetUp()
    {
        testBee = new Bee("1");
        beeLauncherObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Bees/BeeLaunchers/BeeLauncher"));
        beeLauncher = beeLauncherObject.GetComponent<BeeLauncher>();
    }

    [TearDown]
    public void Teardown()
    {
        Object.Destroy(beeLauncherObject);
    }

   // A Test behaves as an ordinary method
    [Test]
    public void ItCanLoadABee()
    {
        // Use the Assert class to test conditions
        Assert.IsFalse(beeLauncher.IsLoaded());
        beeLauncher.LoadBee(testBee);
        Assert.IsTrue(beeLauncher.IsLoaded());
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

}


    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    // [UnityTest]
    // public IEnumerator ItCanLoadABee()
    // {
    //     Assert.IsFalse(beeLauncher.IsLoaded());
    //     beeLauncher.LoadBee(testBee);
    //     Assert.IsTrue(beeLauncher.IsLoaded());




    //     // Use the Assert class to test conditions.
    //     // Use yield to skip a frame.
    //     yield return null;
    // }
// }
