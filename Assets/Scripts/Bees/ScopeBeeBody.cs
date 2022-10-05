using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScopeBeeBody : BeeBody

{

    private Rigidbody rigidBody;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }


    //  private void OnCollisionEnter(Collision other)
    // {
    //     bounceBack(other);
    // }

    



    // private void bounceBack(Collision other)
    // {
       
    //  // how much the character should be knocked back
    //     var magnitude = 100;
    //     // calculate force vector
    //     var force = transform.position - other.transform.position;
    //     // normalize force vector to get direction only and trim magnitude
    //     force.Normalize();
    //     rigidBody.AddForce(force * magnitude);

    // }

}
