using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBeeLauncher : BeeLauncher
{

    [SerializeField]
    private GameObject scopeBeePrefab;

    private GameObject scopeBee;

    private Rigidbody scopeBeeBody;
 
    public void SetEndDragPosition(Vector3 endDragPosition)
    {
      Vector3 newDragPosition = new Vector3(endDragPosition.x, launchPositionY, endDragPosition.z);
      if (this.endDragPosition == newDragPosition)
      {
        return;
      }
      this.endDragPosition = newDragPosition;
      Destroy(scopeBee);
      launchScopeBee();

     // beeScope.DrawLine(endDragPosition);
    }

     public Vector3 GetEndDragPosition()
    {
      return this.endDragPosition;
    }



    private void launchScopeBee()
    {
      scopeBee = Instantiate(scopeBeePrefab, launchPosition, Quaternion.LookRotation(calculateDirection(), Vector3.down)); // Quaternion.identity affects rotation?    
      scopeBeeBody = scopeBee.GetComponent<Rigidbody>();
      Vector3 direction = calculateDirection();
      scopeBeeBody.AddForce(-(calculateDirection()));
    }

    protected override void reset()
    {
      base.reset();
      Destroy(scopeBee);
    }

}
