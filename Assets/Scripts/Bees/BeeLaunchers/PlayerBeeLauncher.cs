using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBeeLauncher : BeeLauncher
{

    [SerializeField]
    private GameObject scopeBeePrefab;

    private GameObject scopeBee;

    private Rigidbody scopeBeeBody;
 
    public override void SetEndPosition(Vector3 endPosition)
    {
      Vector3 newPosition = new Vector3(endPosition.x, launchPositionY, endPosition.z);
      if (this.endPosition == newDragPosition)
      {
        return;
      }
      this.endPosition = newDragPosition;
      Destroy(scopeBee);
      launchScopeBee();
    }

    private void launchScopeBee()
    {
      scopeBee = Instantiate(scopeBeePrefab, launchPosition, Quaternion.LookRotation(calculateDirection(), Vector3.down)); // Quaternion.identity affects rotation?    
      scopeBeeBody = scopeBee.GetComponent<Rigidbody>();
      Vector3 direction = calculateDirection();
      scopeBeeBody.AddForce((calculateDirection()));
    }

    protected override void reset()
    {
      base.reset();
      Destroy(scopeBee);
    }

     protected override Vector3 calculateDirection()
    {
      Vector3 direction = this.endPosition - this.launchPosition;
      return -direction;
    }

}
