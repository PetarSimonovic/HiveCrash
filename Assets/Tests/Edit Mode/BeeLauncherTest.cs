using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class BeeLauncherTest
{

  private GameObject beeLauncherGameObject = new GameObject();
  private BeeLauncher beeLauncher;
  private Bee testBee;

  [SetUp]
  public void SetUp()
  {
    Bee testBee = new Bee("1");
    beeLauncher = beeLauncherGameObject.AddComponent(typeof(BeeLauncher)) as BeeLauncher;
  }

  [Test]
  public void ItCanSetItsLaunchPosition()
  {
    Vector3 launchPosition = new Vector3 (10, 10, 0);
    beeLauncher.SetLaunchPosition(launchPosition);
    Assert.AreEqual(launchPosition, beeLauncher.GetLaunchPosition());

  }

  [Test]
  public void ItCanSetItsEndDragPosition()
  {
    Vector3 endDragPosition = new Vector3 (10, 10, 0);
    beeLauncher.SetEndDragPosition(endDragPosition);
    Assert.AreEqual(endDragPosition, beeLauncher.GetEndDragPosition());
  }

  [Test]
  public void BeeCanBeLoaded()
  {
    beeLauncher.LoadBee(testBee);
    Assert.AreEqual(testBee, beeLauncher.GetLoadedBee());
  }

}
