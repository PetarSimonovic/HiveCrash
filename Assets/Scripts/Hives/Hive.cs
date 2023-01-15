using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hive : MonoBehaviour
{

  private Rigidbody rigidBody;

  private bool isPlaced = false;

  private bool crashed = false; 
  
  private List<Bee> bees = new List<Bee>();
  
  private Vector3 position;

  private int pollen;

  private int pollenCapacity = 10000;
  
  
  private void Awake()
  {
    rigidBody = GetComponent<Rigidbody>();
    AddPollen(this.pollenCapacity);
  }

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
    bee.SetMessage(bee.GetName() + " has died");
    if (this.bees.Count <= 0) 
    {
      this.crashed = true;
      Debug.Log("All bees died");
    }

  }

  public void AddPollen(int pollen)
  {
    this.pollen += pollen;
    if (this.pollen > this.pollenCapacity) 
    {
      this.pollen = this.pollenCapacity;
    }
  setMass();
  }

  public void RemovePollen(int pollen) 
  { 
    this.pollen -= pollen;
    if (this.pollen < 0)
    {
      this.pollen = 0;
    }
  setMass();
  }

  private void setMass()
  {
    var percentage = GetPollenPercentage();
    this.rigidBody.mass = (float)percentage/10;
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
    return transform.position;
  }

  private int calculatePollenTaken()
  {
    int pollenTaken = this.pollen - 100 <= 0 ? this.pollen : 100;
    return pollenTaken;
  }

  private void OnCollisionEnter(Collision other)
  { 
    switch (other.gameObject.tag) {
        case "scope":
          rigidBody.velocity = Vector3.zero;
          rigidBody.angularVelocity = Vector3.zero;
          break;
        case "lake":
          Debug.Log("Hive sank");
          this.crashed = true;
          break;
        case "bee":
          Bee bee = other.gameObject.GetComponent<BeeBody>().GetBee();
          applyForce(other);
          bee.SetMessage(bee.GetName() + " attacked hive");
          int pollenTaken = calculatePollenTaken();
          RemovePollen(pollenTaken);
          bee.AddPollen(pollenTaken);
          break;
      
        default:
          break;
      }
  }

  private void applyForce(Collision other)
  {
    rigidBody.isKinematic = false;
    Vector3 force = transform.position - other.transform.position;
    rigidBody.AddForce(force, ForceMode.Impulse);
  }

  public float GetHiveMass() 
  {
    return rigidBody.mass;
  } 

  public bool HasCrashed() 
  {
    return this.crashed;
  }


}
