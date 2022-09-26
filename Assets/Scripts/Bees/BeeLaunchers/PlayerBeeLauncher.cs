using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBeeLauncher : BeeLauncher
{

    [SerializeField]
    private GameObject scopeBeePrefab;

    private GameObject scopeBee;

    private Rigidbody scopeBeeBody;
 
    public override void SetEndDragPosition(Vector3 endDragPosition)
    {
      Debug.Log("HER!");
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



    private void launchScopeBee()
    {
      Debug.Log("launching scope bee");
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
