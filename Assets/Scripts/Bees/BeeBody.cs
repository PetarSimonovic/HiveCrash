using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class BeeBody : MonoBehaviour
  {

    public bool isEnteringHive = false;

    protected bool isOutsideHive = false;

    private bool collectingPollen = false;

    private bool isIdling = false;

    private const float IDLE_SPEED = 1.5f;

    private const float RETURN_SPEED = 3f;

    private string hiveId;

    public Vector3 hivePosition;

    protected Rigidbody rigidBody;

    private Flower flower;

    private Transform target;

    protected float moveSpeed = 6f;


    private void Start()
    {
      Debug.Log("moveSpeed " + moveSpeed);
      rigidBody = GetComponent<Rigidbody>();
      hivePosition = transform.position;
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
      spinBee(); 

     }
    }

    private void spinBee()
    {
      transform.Rotate(new Vector3(0, 0, 9f));
    }

    protected virtual void moveBee()
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

    private void rotateBeeForward(){
      Quaternion rotation = Quaternion.LookRotation(rigidBody.velocity, Vector3.up);
      transform.rotation = rotation;
    }

    private void returnToHive()
    {
      isIdling = true;
      moveSpeed = IDLE_SPEED;
      float step = RETURN_SPEED * Time.deltaTime;
      transform.position = Vector3.MoveTowards(transform.localPosition, hivePosition, step);
      Quaternion rotation = Quaternion.LookRotation(hivePosition, Vector3.up);
      transform.rotation = rotation;
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
        collectingPollen = false;
        if (isIdling) 
          {
            bounceBack(other);
          };
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
      if (isOutsideHive)
        {
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
 
}
