using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScopeBeeBody : BeeBody
{

    protected override void Update()
    {
      moveBee();
      rotateBeeForward();
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
