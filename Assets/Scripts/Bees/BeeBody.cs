using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class BeeBody : MonoBehaviour
  {
  

    [SerializeField]
    private GameObject beeExploder;

    [SerializeField]
    private GameObject textBubblePrefab;
    
    private Bee bee;

    private bool isPlayer;

    public bool isEnteringHive = false;

    protected bool isOutsideHive = false;

    private bool collectingPollen = false;

    protected bool isIdling = false;

    protected float moveSpeed = 6f;

    protected const float IDLE_SPEED = 1.5f;

    protected const float RETURN_SPEED = 3f;

    protected const float DECELERATION_RATE = 150f;

    private string hiveId;

    public float hivePositionY;

    protected Rigidbody rigidBody;

    private Flower flower;



    protected Hive hive;


    protected virtual void Start()
    {
      rigidBody = GetComponent<Rigidbody>();
      hivePositionY = rigidBody.transform.position.y;
    }


    protected virtual void Update()
    {
      if (hive.HasCrashed()) 
      {
        explodeBee();
      }
      else if (collectingPollen)
      {
        collectPollen();
      }
      else {
        moveBee();
        rotateBeeForward();
      }
    }

  

    protected virtual void moveBee()
    {
      // rigidBody.velocity = rigidBody.velocity.normalized * moveSpeed;
      if (moveSpeed > IDLE_SPEED)
      {
        moveSpeed = moveSpeed - (moveSpeed/DECELERATION_RATE);
      }
      else
      {
        GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity.normalized;
        ReturnToHive();
      }
    }

    protected virtual void rotateBeeForward(){
      Quaternion rotation = Quaternion.LookRotation(rigidBody.velocity, Vector3.up);
      transform.rotation = rotation;
    }

    public void ReturnToHive()
    {
      isIdling = true;
      moveSpeed = IDLE_SPEED;
      float step = RETURN_SPEED * Time.deltaTime;
      Vector3 hivePosition = new Vector3 (hive.GetPosition().x, hivePositionY, hive.GetPosition().z);
      transform.position = Vector3.MoveTowards(transform.position, hivePosition, step);
    }


    private void OnTriggerExit(Collider other)
    {
      if (other.gameObject.tag.ToString() == "hive")
      {
        isOutsideHive = true;
      }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
      processTriggerCollision(other);

    }

    private bool processTriggerCollision(Collider other)
    {
      string otherObject = other.gameObject.tag.ToString();
      switch (otherObject) { 
        case "hive":
          enterHive(other);
          return true; // ?? Why is it returning a bool

        case "flower":
          processFlowerCollision(other);
          return true;

        default:
          return false;
      }
    }

    public string GetHiveId()
    {
      return this.hiveId;
    }


    protected virtual void OnCollisionEnter(Collision other)
    {

    
      if (collectingPollen) 
      {
        collectingPollen = false;
        flower.RemoveBee();
        BeeBody otherBeeBody = other.gameObject.GetComponent<BeeBody>();
        if (otherBeeBody != null) 
          { 
            checkIfBeeKilledInCollision(otherBeeBody);
          }
          return;

      }
        if(isIdling) {
        bounceBack(other);
        }

    }

    private void checkIfBeeKilledInCollision(BeeBody otherBeeBody) {
      collectingPollen = false; // repetition?
      flower.RemoveBee();
      ReturnToHive();

      switch (otherBeeBody.hiveId == this.hiveId) 
      { 
        case true:
          Debug.Log("Friendly collision");
          break;
        default:
          explodeBee(otherBeeBody);
          break;
      }
          otherBeeBody.ReturnToHive();
          if (otherBeeBody.CollectingPollen())
          {
            otherBeeBody.SetCollectingPollen(false);
            otherBeeBody.flower.RemoveBee();
          }
    }

    private void explodeBee(BeeBody otherBeeBody) 
    {
          beeExploder = Instantiate(beeExploder);
          string message = otherBeeBody.GetBee().GetName() + " killed " + this.bee.GetName();
          LaunchTextBubble(message, false);
          beeExploder.GetComponent<BeeExploder>().explodeBee(flower.GetPosition(), isPlayer);
          this.bee.SetHealth(0);
    }

    private void explodeBee() 
    {
          beeExploder = Instantiate(beeExploder);
          string message = this.bee.GetName() + " lost its hive";
          LaunchTextBubble(message, false);
          beeExploder.GetComponent<BeeExploder>().explodeBee(transform.position, isPlayer);
          this.bee.SetHealth(0);
    }

    private void bounceBack(Collision other)
    {

        // how much the character should be knocked back
        var magnitude = 130;
        // calculate force vector
        var force = transform.position - other.transform.position;
        // normalize force vector to get direction only and trim magnitude
        force.Normalize();
        rigidBody.AddForce(force * magnitude);
        

    }

    public void SetHiveId(string hiveId)
    {
      this.hiveId = hiveId;
    }

    private void enterHive(Collider other)
    {
      if (isOutsideHive)
        {
          isEnteringHive = true;
          stopMoving();
        }
    }

    private void processFlowerCollision(Collider other)
    {
      flower = other.transform.parent.gameObject.GetComponent<Flower>();
      if (flower.HasBee()) { return; }
      checkPollenCollection();
      if (collectingPollen)
      {
        flower.PlaceBee();
        moveSpeed = IDLE_SPEED;
        rigidBody.velocity = rigidBody.velocity.normalized * moveSpeed;
      }
    }

    private void placeBeeOnFlower()
    {  
      moveSpeed = IDLE_SPEED;
     // rigidBody.velocity = rigidBody.velocity.normalized * moveSpeed;
      float step = RETURN_SPEED * Time.deltaTime;
      transform.position = Vector3.MoveTowards(transform.localPosition, flower.GetPosition(), step);
      Quaternion rotation = Quaternion.LookRotation(Vector3.up, Vector3.up);
      transform.rotation = rotation;
    }

    private void checkPollenCollection() 
    {
      collectingPollen = flower.IsInBloom();
    }

    private void collectPollen() 
    {
      checkPollenCollection();
      placeBeeOnFlower();
    }

    public float GetMoveSpeed()
    {
      return moveSpeed;
    }

    public bool CollectingPollen()
    {
      return collectingPollen;
    }

    public void SetBee(Bee bee)
    {
      this.bee = bee;
    }

    public Bee GetBee()
    {
      return this.bee;
    }

    public void SetHive(Hive hive)
    {
      this.hive = hive;
    }

    protected virtual void stopMoving()
    {
        moveSpeed = 0.0f;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        isOutsideHive = false;
    }

    public void SetPlayer(bool isPlayer) 
    {
      this.isPlayer = isPlayer;
    }

    public bool IsPlayer() {
      return this.isPlayer;
    }

    public void LaunchTextBubble(string message, bool isPositive = true)
    {
      Vector3 textPosition = transform.position;
      textPosition.y = 1.00f;
      GameObject textBubble = Instantiate(textBubblePrefab, textPosition, Quaternion.identity);
      textBubble.GetComponent<TextBubble>().SetText(message, isPositive);
    }

    public void SetCollectingPollen(bool collectingPollen)
    {
      this.collectingPollen = collectingPollen;
    }

 
}
