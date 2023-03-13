using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBeeBody : BeeBody
{
    private float SPEED = 3F;
    private Vector3 target;

    protected override void moveBee()
    {
      // rigidBody.velocity = rigidBody.velocity.normalized * moveSpeed;
      if (!isIdling)
      {
        float step = SPEED * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target, step);
        if (transform.position == target) {ReturnToHive();}
        }
      else
      {
        GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity.normalized;
        ReturnToHive();
      }
    }

    public void SetTarget(Vector3 target) {
        this.target = target;
    }
}
