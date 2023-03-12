using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBeeBody : BeeBody
{
    private float SPEED = 3F;
    private Vector3 playerHivePosition;

    protected override void moveBee()
    {
      // rigidBody.velocity = rigidBody.velocity.normalized * moveSpeed;
      if (!isIdling)
      {
        float step = SPEED * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, playerHivePosition, step);;
        }
      else
      {
        GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity.normalized;
        ReturnToHive();
      }
    }

    public void SetPlayerHivePosition(Vector3 playerHivePosition) {
        this.playerHivePosition = playerHivePosition;
    }
}
