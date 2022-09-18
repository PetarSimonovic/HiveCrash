using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hive : MonoBehaviour
{

  private bool isPlaced = false;
  
  private List<Bee> bees = new List<Bee>();
  
  private Vector3 position;

  private int pollen;
  

  public void Place()
  {
    this.isPlaced = true;
  }

  public bool IsPlaced()
  {
    return this.isPlaced;
  }

  public string GetId()
  {
    return this.GetInstanceID().ToString();
  }

  public List<Bee> GetBees()
  {
    return this.bees;
  }

  public void AddBee()
  {
    this.bees.Add(new Bee(this.GetId()));
  }


  public Bee GetBee()
  {
    return this.bees.Find(bee => bee.IsInHive());
  }

  public void SetPosition(Vector3 position)
  {
    this.position = position;
  }


  public Vector3 GetPosition()
  {
    return this.position;
  }

  public void AddPollen(int pollen)
  {
    this.pollen += pollen;
    Debug.Log("Hive pollen is " + this.pollen);
  }


}
