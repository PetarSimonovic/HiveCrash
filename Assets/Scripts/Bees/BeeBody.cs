using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BeeBody : MonoBehaviour
  {

    public Vector3 hivePosition;

    private Vector3 lastTile;
    private Rigidbody rigidBody;

    [SerializeField]
    private float moveSpeed = 20f;

    [SerializeField]
    private float angularVelocity = 0.9f;

    private const float IDLE_SPEED = 1.5f;

    private const float RETURN_SPEED = 3f;

    public bool isEnteringHive = false;

    private bool isIdling = false;

    private bool isOutsideHive = false;

    private string hiveId;

    private Transform target;



    private void Start()
    {
      isIdling = false;
      rigidBody = GetComponent<Rigidbody>();
      hivePosition = transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
      rigidBody.velocity = rigidBody.velocity.normalized * moveSpeed;
      if (moveSpeed > IDLE_SPEED)
      {
        moveSpeed = moveSpeed - 0.001f;
      }
      else
      {
        moveSpeed = IDLE_SPEED;
        isIdling = true;
        float step = RETURN_SPEED * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.localPosition, hivePosition, step);
      }
      transform.position = new Vector3(transform.position.x, 0.8f, transform.position.z);
    }

    private void OnTriggerExit(Collider other)
    {
      if (other.gameObject.name.ToString() == "hex")
      {
        Debug.Log("Leaving Hive!");
        isOutsideHive = true;
      }
    }

    private void OnTriggerEnter(Collider other)
    {
      string otherId = getOtherId(other);
      if (otherId == this.hiveId && isOutsideHive)
      {
        Debug.Log(other.transform.parent.gameObject.name);
        Debug.Log("Entering Hive!");
        isEnteringHive = true;
      }


      // if (other.gameObject.tag == this.tag && isOutsideHive)
      // {
      //   isEnteringHive = true;
      // }
    }

    private string getOtherId(Collider other)
    {
      return other.transform.parent.gameObject.name;
    }


    private void OnCollisionEnter(Collision other)
    {

     // if (isIdling)
     //  {
        bounceBack(other);
      // }
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

}
