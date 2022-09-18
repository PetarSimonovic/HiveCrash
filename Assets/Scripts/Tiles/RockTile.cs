using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockTile : Tile
{
    private Collider[] colliders;

    private Collider scopeCollider;

    protected override void Start()
    {
        base.Start();
        colliders = GetComponentsInChildren<Collider>();
        scopeCollider = colliders[1]; // improve this: find the scopecollider in the array; don't rely on position
        scopeCollider.enabled = false;
    }

     public void OnCollisionEnter(Collision collision)
    {
       if (this.IsHidden())
       {
        checkCollision(collision.collider);
        scopeCollider.enabled = true;
       }
    }
}
