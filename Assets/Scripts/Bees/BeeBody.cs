using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class BeeBody : MonoBehaviour
  {

    public bool isEnteringHive = false;

    private bool isOutsideHive = false;

    private bool collectingPollen = false;

    private bool isIdling = false;

    private const float IDLE_SPEED = 1.5f;

    private const float RETURN_SPEED = 3f;

    private string hiveId;

    public Vector3 hivePosition;

    private Rigidbody rigidBody;

    private Flower flower;

    private Transform target;

     [SerializeField]
    private float moveSpeed = 20f;


    private void Start()
    {
      rigidBody = GetComponent<Rigidbody>();
      hivePosition = transform.position;
    }


    private void Update()
    {
     if (collectingPollen)
     {
      collectPollen();
     }
     else {
      moveBee();
     }
    }

    private void moveBee()
    {
      rigidBody.velocity = rigidBody.velocity.normalized * moveSpeed;
      if (moveSpeed > IDLE_SPEED)
      {
        moveSpeed = moveSpeed - 0.02f;
      }
      else
      {
       returnToHive();
      }
    }

    private void returnToHive()
    {
      isIdling = true;
      moveSpeed = IDLE_SPEED;
      float step = RETURN_SPEED * Time.deltaTime;
      transform.position = Vector3.MoveTowards(transform.localPosition, hivePosition, step);
      Quaternion rotation = Quaternion.LookRotation(hivePosition, Vector3.down);
      transform.rotation = rotation;
    }


    private void OnCollisionExit(Collision other)
    {
      if (other.gameObject.tag.ToString() == "hive")
      {
        isOutsideHive = true;
      }
    }

    private void OnTriggerEnter(Collider other)
    {
      processCollision(other);

    }

    private bool processCollision(Collider other)
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

    private string getHiveId(Collider other)
    {
      string id = other.transform.parent.gameObject.name;
      Debug.Log(id);
      return id;
    }


    private void OnCollisionEnter(Collision other)
    {
        if (!processCollision(other.collider))
        {
        collectingPollen = false;
        if (isIdling) 
          {
            bounceBack(other);
          };
        }
    }

    private void OnCollisionStay(Collision other)
    {
        processCollision(other.collider);
    }

    private void bounceBack(Collision other)
    {

        // how much the character should be knocked back
        var magnitude = 10;
        // calculate force vector
        var force = transform.position - other.transform.position;
        // normalize force vector to get direction only and trim magnitude
        force.Normalize();
        rigidBody.AddForce(force * magnitude, ForceMode.Impulse);

    }

    public void SetHiveId(string hiveId)
    {
      this.hiveId = hiveId;
    }

    private void enterHive(Collider other)
    {
      Debug.Log("Trying to enter hive");
      // Just use a 'playerHive' tag instead of an ID?
      if (getHiveId(other) == this.hiveId && isOutsideHive)
        {
          Debug.Log("Entering hive");
          isEnteringHive = true;
        }
    }

    private void processFlowerCollision(Collider other)
    {
      flower = other.transform.parent.gameObject.GetComponent<Flower>();
      checkPollenCollection();
      if (collectingPollen)
      {
       moveSpeed = IDLE_SPEED;
       rigidBody.velocity = rigidBody.velocity.normalized * moveSpeed;
      }
    }

    private void placeBeeOnFlower()
    {  
      moveSpeed = IDLE_SPEED;
      rigidBody.velocity = rigidBody.velocity.normalized * moveSpeed;
      float step = RETURN_SPEED * Time.deltaTime;
      transform.position = Vector3.MoveTowards(transform.localPosition, flower.GetPosition(), step);
      Quaternion rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
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

}
