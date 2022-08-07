using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hive : MonoBehaviour
{

  private bool isPlaced = false;
  private List<Bee> bees = new List<Bee>();

  public void Place()
  {
    this.isPlaced = true;
  }

  public bool IsPlaced()
  {
    return this.isPlaced;
  }

  public int GetId()
  {
    return this.GetInstanceID();
  }

  public List<Bee> GetBees()
  {
    return this.bees;
  }

  public void AddBee()
  {
    this.bees.Add(new Bee(this.GetId()));
  }

}
