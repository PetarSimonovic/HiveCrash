using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hive
{
  private bool isPlaced = false;

  public void Place()
  {
    this.isPlaced = true;
  }

  public bool IsPlaced()
  {
    return this.isPlaced;
  }

}
