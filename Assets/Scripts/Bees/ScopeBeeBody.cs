using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScopeBeeBody : BeeBody
{

    private bool hasStopped;

    protected override void Update()
    {
    //  if (!hasStopped) 
    //  {
      moveBee();
    // }
    }

    protected override void OnTriggerEnter(Collider other)
    {
      string otherObject = other.gameObject.tag.ToString();
      if (otherObject == "hive" && isOutsideHive) 
      {
        stopMoving();
      }
    }

    private void stopMoving()
    {
        Debug.Log("Stoping scope motion");
        moveSpeed = 0.0f;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        isOutsideHive = false;
        hasStopped = true;
    }

}
