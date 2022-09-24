using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTile : Tile
{
    private Collider[] colliders;

    private Collider scopeCollider;

    protected override void Awake()
    {
        base.Awake();
        colliders = GetComponentsInChildren<Collider>();
        scopeCollider = colliders[1]; // improve this: find the scopecollider in the array; don't rely on position
        scopeCollider.enabled = false;
    }
    
    protected override void reveal()
    {
        base.reveal();
        scopeCollider.enabled = true;
    }

     public void OnCollisionEnter(Collision collision)
    {
       if (this.isHidden)
       {
        checkCollision(collision.collider);
       }
    }
}
