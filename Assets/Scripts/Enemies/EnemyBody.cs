using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBody : MonoBehaviour
{
    private float SPEED = 3F;
    private Vector3 target;

    private Rigidbody rigidBody;

    private Hive hive;

    private void Start()
    {
      rigidBody = GetComponent<Rigidbody>();
    }
    
    private void Update() 
    {
      move(); 
      faceForward();
    }

    private void faceForward() 
    {
        Quaternion rotation = Quaternion.LookRotation(rigidBody.position, Vector3.forward);
        transform.rotation = rotation;
    }
    private void move()
    {
      // rigidBody.velocity = rigidBody.velocity.normalized * moveSpeed;
      {
        
        float step = SPEED * Time.deltaTime;
        transform.position = Vector3.MoveTowards(rigidBody.transform.position, hive.GetPosition(), step);
        }
    }

     private void OnCollisionEnter(Collision other) {
      // bounceBack(other);
      Debug.Log("Enemy collided with " + other.gameObject.tag);
     }

    public void SetHive(Hive hive) {
        this.hive = hive;
    } 

     private void bounceBack(Collision other)
    {

        // how much the character should be knocked back
        var magnitude = 130;
        // calculate force vector
        var force = transform.position - other.transform.position;
        // normalize force vector to get direction only and trim magnitude
        force.Normalize();
       rigidBody.AddForceAtPosition(force * magnitude, transform.position);
        

    }
}
