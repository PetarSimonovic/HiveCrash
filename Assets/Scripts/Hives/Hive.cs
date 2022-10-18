using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hive : MonoBehaviour
{

  private Rigidbody rigidBody;

  private bool isPlaced = false;
  
  private List<Bee> bees = new List<Bee>();
  
  private Vector3 position;

  private int pollen;

  private int pollenCapacity = 10000;
  
  private void Awake()
  {
    rigidBody = GetComponent<Rigidbody>();
    Debug.Log("Rigidbody");
    Debug.Log(rigidBody.mass);
  }

  public void Place()
  {
    Debug.Log("PLACED!");
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

  public void AddPollen(int pollen)
  {
    this.pollen += pollen;
    if (this.pollen > this.pollenCapacity) 
    {
      this.pollen = this.pollenCapacity;
    }
    setMass();
    Debug.Log("hive pollen now " + GetPollen() + " " + GetPollenPercentage());
  }

  public void RemovePollen(int pollen) 
  { 
    this.pollen -= pollen;
    if (this.pollen < 0)
    {
      this.pollen = 0;
    }
     setMass();
     Debug.Log("hive pollen now " + GetPollen() + " " + GetPollenPercentage());
  }

  private void setMass()
  {
    this.rigidBody.mass = (float)this.pollen;
    Debug.Log("mass: " + this.rigidBody.mass);
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

  private int calculatePollenTaken()
  {
    int pollenTaken = this.pollen - 100 <= 0 ? this.pollen : 100;
    return pollenTaken;
  }

  private void OnCollisionEnter(Collision other)
  {
    Debug.Log("Hive Collision!");
    if (other.gameObject.tag == "bee")
    {
      Bee bee = other.gameObject.GetComponent<BeeBody>().GetBee();

      bee.SetMessage(bee.GetName() + " attacked hive");
      int pollenTaken = calculatePollenTaken();
      RemovePollen(pollenTaken);
      bee.AddPollen(pollenTaken);
    }
  }
    


}
