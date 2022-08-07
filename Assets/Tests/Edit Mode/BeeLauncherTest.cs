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
    Bee testBee = new Bee(1);
    beeLauncher = beeLauncherGameObject.AddComponent(typeof(BeeLauncher)) as BeeLauncher;
  }

  [Test]
  public void BeeCanBeLoaded()
  {
    beeLauncher.LoadBee(testBee);
    Assert.AreEqual(testBee, beeLauncher.GetLoadedBee());
  }

}
