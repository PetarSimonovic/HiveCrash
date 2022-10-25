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
      if (this.endPosition == newPosition)
      {
        return;
      } 
      else if (!isLoaded) 
      {
        return;
      }
      this.endPosition = newPosition;

      /// side effects? move into different method
      Destroy(scopeBee);
      launchScopeBee();
    }

    private void launchScopeBee()
    {
      scopeBee = Instantiate(scopeBeePrefab, launchPosition, Quaternion.identity); // Quaternion.identity affects rotation?    
      scopeBeeBody = scopeBee.GetComponent<Rigidbody>();
      Vector3 direction = calculateDirection();
      scopeBeeBody.AddForceAtPosition(calculateDirection().normalized, launchPosition, ForceMode.Impulse);
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
