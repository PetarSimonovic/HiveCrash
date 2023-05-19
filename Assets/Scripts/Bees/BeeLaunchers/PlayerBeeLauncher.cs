using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBeeLauncher : BeeLauncher
{

    [SerializeField]
    private GameObject scopeBeePrefab;
    

    private GameObject scopeBee;

    private Rigidbody scopeBeeBody;

    private bool drag = false;
 
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
      drag = true;

      /// side effects? move into different method
      Destroy(scopeBee);
      launchScopeBee();
    }

    public void LaunchBee()
    {
        if (isLoaded && drag) 
      {
        this.loadedBee.Fly();
        Vector3 direction = calculateDirection();
        Vector3 launchPosition = fixYPosition(hive.GetPosition());
        GameObject beeBody = Instantiate(beePrefab, launchPosition, Quaternion.LookRotation(-direction, Vector3.forward)); // Quaternion.identity affects rotation?
        setBeeBodyProperties(beeBody);
       
        ApplyForceToBeeBody(beeBody.GetComponent<Rigidbody>(), launchPosition);
      }
        reset();
    }



    private void launchScopeBee()
    {
      Vector3 launchPosition = fixYPosition(hive.GetPosition());
      scopeBee = Instantiate(scopeBeePrefab, launchPosition, Quaternion.identity); // Quaternion.identity affects rotation?    
      scopeBeeBody = scopeBee.GetComponent<Rigidbody>();
      Vector3 direction = calculateDirection();
      scopeBee.GetComponent<BeeBody>().SetHive(hive);
      ApplyForceToBeeBody(scopeBee.GetComponent<Rigidbody>(), launchPosition);
    }

    protected override void reset()
    {
      base.reset();
      drag = false;
      Destroy(scopeBee);
    }

     protected override Vector3 calculateDirection()
    {
      Vector3 direction = this.endPosition - hive.GetPosition();
      return -direction;
    }
    
}


