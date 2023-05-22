using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBody : MonoBehaviour
{
    private float SPEED = 3f;
    private Vector3 target;

    private Rigidbody rigidBody;

    private Hive hive;

    private bool isDead = false;

    private Exploder exploder;

    protected virtual void Start()
    {
      rigidBody = GetComponent<Rigidbody>();
    }
    
    private void Update() 
    {
      if (hive != null) 
      {
      move(); 
      faceForward();      
      }
    
    }

    private void faceForward() 
    {
    // float degreesPerSecond = 120f;
    //  transform.Rotate(new Vector3(0, 0, degreesPerSecond) * Time.deltaTime);
    transform.LookAt(hive.GetPosition());


    }
    private void move()
    {
      // rigidBody.velocity = rigidBody.velocity.normalized * SPEED;
      {
        Debug.Log("Hive");
        Debug.Log(hive);
        float step = SPEED * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, hive.GetPosition(), step);
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
      bounceBack(other);
    }

    public void SetHive(Hive hive) {
      Debug.Log("Setting hive");
        this.hive = hive;
        Debug.Log(this.hive);
    } 

    private void bounceBack(Collision other)
    {

        // how much the character should be knocked back
        var magnitude = 130;
        // calculate force vector
        var force = transform.position - other.transform.position;
        // normalize force vector to get direction only and trim magnitude
        force.Normalize();
        rigidBody.AddForce(force * magnitude);
        

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
