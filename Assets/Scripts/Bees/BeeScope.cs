using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeScope : MonoBehaviour

{

    private Rigidbody rigidBody;
    private Collider collider;
    private const float Y_POSITION = 0.5f; // always has to be same as Bee - inheritance


    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
    }

    private void Update()
    {
        fixBeeToYPosition();
    }

    private void fixBeeToYPosition()
    {
      transform.position = new Vector3(transform.position.x, Y_POSITION, transform.position.z);
    }


     private void OnCollisionEnter(Collision other)
    {
        bounceBack(other);
    }

    



    private void bounceBack(Collision other)
    {
       
     // how much the character should be knocked back
        var magnitude = 100;
        // calculate force vector
        var force = transform.position - other.transform.position;
        // normalize force vector to get direction only and trim magnitude
        force.Normalize();
        rigidBody.AddForce(force * magnitude);

    }

}
