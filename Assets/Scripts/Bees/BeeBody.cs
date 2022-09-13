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

    private const float IDLE_SPEED = 1.5f;

    private const float RETURN_SPEED = 3f;

    private const float Y_POSITION = 0.7f;

    private string hiveId;

    public Vector3 hivePosition;

    private Rigidbody rigidBody;

    private Flower flower;

    private Transform target;

     [SerializeField]
    private float moveSpeed = 20f;

    [SerializeField]
    private float angularVelocity = 0.9f;




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
      fixBeeToYPosition();
     }

    }

    private void moveBee()
    {
      rigidBody.velocity = rigidBody.velocity.normalized * moveSpeed;
      if (moveSpeed > IDLE_SPEED)
      {
        moveSpeed = moveSpeed - 0.001f;
      }
      else
      {
       returnToHive();
      }
    }

    private void returnToHive()
    {
      moveSpeed = IDLE_SPEED;
      float step = RETURN_SPEED * Time.deltaTime;
      transform.position = Vector3.MoveTowards(transform.localPosition, hivePosition, step);
    }

    private void fixBeeToYPosition()
    {
      transform.position = new Vector3(transform.position.x, Y_POSITION, transform.position.z);
    }


    private void OnTriggerExit(Collider other)
    {
      if (other.gameObject.tag.ToString() == "hive")
      {
        Debug.Log("Leaving Hive!");
        isOutsideHive = true;
      }
    }

    private void OnTriggerEnter(Collider other)
    {
      string otherObject = other.gameObject.tag.ToString();
      switch (otherObject) {
          
        case "hive":
          enterHive(other);
          break;

        case "flower":
          processFlowerCollision(other);
          break;

         default:
          break;
      }

    }

    private string getHiveId(Collider other)
    {
      return other.transform.parent.gameObject.name;
    }


    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("COLLIDE WITH" + other.gameObject);
        bounceBack(other);
    }

    private void bounceBack(Collision other)
    {

        // how much the character should be knocked back
        var magnitude = 999;
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
      // Just use a 'playerHive' tag instead of an ID?
      if (getHiveId(other) == this.hiveId && isOutsideHive)
        {
          Debug.Log(other.transform.parent.gameObject.name);
          Debug.Log("Entering Hive!");
          isEnteringHive = true;
        }
    }

    private void processFlowerCollision(Collider other)
    {
      flower = other.transform.parent.gameObject.GetComponent<Flower>();
      checkPollenCollection();
    }

    private void placeBeeOnFlower()
    {
    
      transform.position = flower.GetPosition();
    }

    private void checkPollenCollection() 
    {
      collectingPollen = flower.IsInBloom();
    }

    private void collectPollen() 
    {
      placeBeeOnFlower();
      checkPollenCollection();
    }

}
