using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hive : MonoBehaviour
{
  [SerializeField]
  private GameObject textBubblePrefab;

  [SerializeField]
  private GameObject pollenCounterPrefab;

  private GameObject pollenCounter;
  private Rigidbody rigidBody;

  private bool isPlaced = false;

  private bool crashed = false; 
  
  private List<Bee> bees = new List<Bee>();
  
  private Vector3 position;

  private int pollen;

  private int pollenCapacity = 10000;

  private bool crashedTextBubbleSent;

  public bool titleHive = false;
  
  
  private void Awake()
  {
    rigidBody = GetComponent<Rigidbody>();
    this.pollen = this.pollenCapacity;
  }
  

  private void Update()
  {
    int beesInHive = 0;
   bees.ForEach(bee => {
    if (bee.IsInHive()) {beesInHive++;}
   });
   if (!titleHive) 
   {
   string pollenCountText = beesInHive + "/" + bees.Count + " " + GetPollenPercentage().ToString() + "%";
   pollenCounter.GetComponent<StaticTextBubble>().SetText(pollenCountText);
   positionPollenCounter();
   }

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
    if (this.bees.Count <= 0) 
    {
      this.crashed = true && !crashedTextBubbleSent;
      LaunchTextBubble("No more bees", false);
      launchHiveCrashTextBubble();
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
    this.rigidBody.mass = (float)percentage/20;
    if (this.rigidBody.mass < 0.1f) {this.rigidBody.mass = 0.1f;}
  }

  public int GetPollen()
  {
    return this.pollen;
  }

  public int GetPollenPercentage()
  {
    var decimalValue = ((decimal)this.pollen/(decimal)this.pollenCapacity) * 100;
    return (byte)decimalValue;
  
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
    return gameObject.transform.position;
  }

  private int calculatePollenTaken(int beePollenCapacity)
  {
    int pollenTaken = this.pollen - beePollenCapacity <= 0 ? this.pollen : beePollenCapacity;
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
          LaunchTextBubble("Hive drowned", false);
          this.crashed = true;
          stopMoving();
          launchHiveCrashTextBubble();
          break;
        case "bee":
          Bee bee = other.gameObject.GetComponent<BeeBody>().GetBee();
          applyForce(other);
          int pollenTaken = calculatePollenTaken(bee.GetPollenCapacity());
          RemovePollen(pollenTaken);
          bee.AddPollen(pollenTaken);
          string message = bee.GetName() + " stole " +  pollenTaken.ToString() + " pollen";
          LaunchTextBubble(message, false);
          other.gameObject.GetComponent<BeeBody>().ReturnToHive();
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

  public void LaunchTextBubble(string message, bool isPositive = true)
  {
    Vector3 textPosition = GetPosition();
    textPosition.y = 1.50f;
    GameObject textBubble = Instantiate(textBubblePrefab, textPosition, Quaternion.identity);
    textBubble.GetComponent<TextBubble>().SetText(message, isPositive);
  }

  private void launchHiveCrashTextBubble() {

    crashedTextBubbleSent = true;
    int numberOfMessages = 20;
    for (int i = 0; i < numberOfMessages; i++) {
      LaunchTextBubble("HiveCrash", false);
    }

  }

  private void launchPollenCounter() {
  
    pollenCounter = Instantiate(pollenCounterPrefab);
    positionPollenCounter();
  }

   protected virtual void stopMoving()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }

  private void positionPollenCounter() {
    Vector3 textPosition = GetPosition();
    textPosition.y = 1f;
    pollenCounter.transform.position = textPosition;
  }

}
