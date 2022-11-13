using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class BeeBody : MonoBehaviour
  {
  

   [SerializeField]
    private GameObject beeExploder;
    private Bee bee;

    public bool isEnteringHive = false;

    protected bool isOutsideHive = false;

    private bool collectingPollen = false;

    private bool isIdling = false;

    private const float IDLE_SPEED = 1.5f;

    private const float RETURN_SPEED = 3f;

    private string hiveId;

    public float hivePositionY;

    protected Rigidbody rigidBody;

    private Flower flower;

    private Transform target;

    protected float moveSpeed = 6f;

    private Hive hive;


    private void Start()
    {
      rigidBody = GetComponent<Rigidbody>();
      hivePositionY = rigidBody.transform.position.y;
    }


    protected virtual void Update()
    {
     if (collectingPollen)
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
        moveSpeed = moveSpeed - 0.06f;
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
          return true;

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


    private void OnCollisionEnter(Collision other)
    {
      if (isIdling) 
      {
        bounceBack(other);
      }
       else if (collectingPollen) 
      {
        BeeBody otherBeeBody = other.gameObject.GetComponent<BeeBody>();
        checkIfBeeKilledInCollision(otherBeeBody);
      }
    }

    private void checkIfBeeKilledInCollision(BeeBody otherBeeBody) {
      collectingPollen = false;
      flower.RemoveBee();

      switch (otherBeeBody.hiveId == this.hiveId) 
      { 
        case true:
          Debug.Log("Friendly collision");
          ReturnToHive();
          break;
        default:
          bee.SetMessage(otherBeeBody.GetBee().GetName() + " killed " + bee.GetName() + bee.GetHealth());
          Debug.Log(otherBeeBody.GetBee().GetName() + " killed " + bee.GetName() + bee.GetHealth());
          beeExploder.GetComponent<BeeExploder>().explodeBee(flower.GetPosition());
          bee.SetHealth(0);
          break;
      }
          otherBeeBody.ReturnToHive();

    }

    private void bounceBack(Collision other)
    {

        // how much the character should be knocked back
        var magnitude = 350;
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

 
}
