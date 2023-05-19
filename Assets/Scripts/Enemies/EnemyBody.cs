using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBody : MonoBehaviour
{
    private float SPEED = 2f;
    private Vector3 target;

    private Rigidbody rigidBody;

    private Hive hive;

    private bool isDead = false;

    private Exploder exploder;

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
      Quaternion rotation; 
     // if (rigidBody.velocity != Vector3.zero) {
        rotation = Quaternion.LookRotation(hive.GetPosition());
      // } else {
      //     rotation = Quaternion.identity;
      // }
       transform.rotation = rotation;
    }
    private void move()
    {
      // rigidBody.velocity = rigidBody.velocity.normalized * SPEED;
      {
        
        float step = SPEED * Time.deltaTime;
        transform.position = Vector3.MoveTowards(rigidBody.transform.position, hive.GetPosition(), step);
        }
    }

    private void OnCollisionEnter(Collision other) {
      if (other.gameObject.tag.ToString() == "bee")
      {
        BeeBody beeBody = other.gameObject.GetComponent<BeeBody>();
        if (!beeBody.CollectingPollen()) 
        {
            explode();
        }
      }
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

    private void explode()
    { 
      isDead = true;
      exploder = GetComponent<Exploder>();
      Debug.Log(transform.GetChild(0));
      exploder.explodeEntity(transform.GetChild(0), transform.position, false);
    }


    public bool IsDead() 
    {
      return isDead;
    }
}
