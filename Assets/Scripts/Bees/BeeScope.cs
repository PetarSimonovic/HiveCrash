using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeScope : MonoBehaviour

{

    private Rigidbody rigidBody;
    private Collider collider;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
    }

     private void OnCollisionEnter(Collision other)
    {
        bounceBack(other);
    }



    private void bounceBack(Collision other)
    {
       
     // how much the character should be knocked back
        var magnitude = 4;
        // calculate force vector
        var force = transform.position - other.transform.position;
        // normalize force vector to get direction only and trim magnitude
        force.Normalize();
        rigidBody.AddForce(force * magnitude, ForceMode.Impulse);

    }

}
