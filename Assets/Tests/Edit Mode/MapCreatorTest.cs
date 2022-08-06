using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class MapCreatorTest
{
    // A Test behaves as an ordinary method
    [Test]
    public void MapTestSimplePasses()
    {
      MapCreator mapCreator = new MapCreator();
      Assert.IsTrue(mapCreator.Test());
    }


}
