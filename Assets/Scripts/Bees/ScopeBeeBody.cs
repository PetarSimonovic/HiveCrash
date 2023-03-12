using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScopeBeeBody : BeeBody
{

    protected float scopeSpeed = 200f;

    protected override void Update()
    {
      moveBee();
      rotateBeeForward();
    }


    protected virtual void moveBee()
    {
      rigidBody.velocity = rigidBody.velocity.normalized * scopeSpeed;
      
    }

    protected override void OnTriggerEnter(Collider other)
    {
      string otherObject = other.gameObject.tag.ToString();
      if (otherObject == "hive" && isOutsideHive) 
      {
        stopMoving();
      }
    }
}
