using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBody : MonoBehaviour
{
    private float SPEED = 3F;
    private Vector3 target;

    private Rigidbody rigidBody;

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
        Quaternion rotation = Quaternion.LookRotation(rigidBody.velocity, Vector3.up);
        transform.rotation = rotation;
    }
    private void move()
    {
      // rigidBody.velocity = rigidBody.velocity.normalized * moveSpeed;
      {
        
        float step = SPEED * Time.deltaTime;
        transform.position = Vector3.MoveTowards(rigidBody.transform.position, target, step);
        }
    }

    
    public void SetTarget(Vector3 target) {
        this.target = target;
    } 
}
