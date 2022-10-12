using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hive : MonoBehaviour
{


  private bool isPlaced = false;
  
  private List<Bee> bees = new List<Bee>();
  
  private Vector3 position;

  private int pollen;

  private int pollenCapacity = 10000;
  

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

  public void AddBee(Bee bee)
  {
    this.bees.Add(bee);
  }


  public Bee GetBee()
  {
    return this.bees.Find(bee => bee.IsInHive());
  }

  

  public void RemoveBee(Bee bee)
  {
    this.bees.Remove(bee);
    Debug.Log(bee.GetName() + " has died");
  }

  public void SetPollen(int pollen)
  {
    this.pollen += pollen;
    if (this.pollen > this.pollenCapacity) 
    {
      this.pollen = this.pollenCapacity;
    }
    else if (this.pollen < 0)
    {
      this.pollen = 0;
    }
    Debug.Log("hive pollen now " + GetPollen() + " " + GetPollenPercentage());
  }

  public int GetPollen()
  {
    return this.pollen;
  }

  public int GetPollenPercentage()
  {
   var decimalValue = ((decimal)this.pollen/(decimal)this.pollenCapacity) * 100;
   return (int)decimalValue;
  
  }

  public void SetPollenCapacity(int pollenCapacity)
  {
    this.pollenCapacity = pollenCapacity;
  }


  public void SetPosition(Vector3 position)
  {
    this.position = position;
  }


  public Vector3 GetPosition()
  {
    return this.position;
  }

}
