using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBeeLauncher : BeeLauncher
{

    [SerializeField]
    private GameObject scopeBeePrefab;

    private GameObject scopeBee;

    private Rigidbody scopeBeeBody;



    public override void LoadBee(Bee bee)
    {
      base.LoadBee(bee);
    //  Destroy(scopeBee);
    }


 
    public override void SetEndDragPosition(Vector3 endDragPosition)
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



    private void launchScopeBee()
    {
      Debug.Log("Launching scopeBee");
      scopeBee = Instantiate(scopeBeePrefab, launchPosition, Quaternion.LookRotation(calculateDirection(), Vector3.down)); // Quaternion.identity affects rotation?    
      scopeBeeBody = scopeBee.GetComponent<Rigidbody>();
      Vector3 direction = calculateDirection();
      scopeBeeBody.AddForce(-(calculateDirection()));
      beeProperties = beePrefab.GetComponent<BeeBody>();
      Debug.Log(beeProperties);
    }

    protected override void reset()
    {
      base.reset();
      Destroy(scopeBee);
    }

}
