using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeLauncher : MonoBehaviour
{
    [SerializeField]
    private GameObject beePrefab;

    [SerializeField]
    private GameObject scopeBeePrefab;

    private Bee loadedBee;

    private bool isLoaded = false;

    private Vector3 launchPosition;

    private float launchPositionY = 0.5f;

    private Vector3 endDragPosition;

    private GameObject scopeBee;

    private BeeBody beeProperties;

    private Rigidbody scopeBeeBody;



    public Bee GetLoadedBee()
    {
      return this.loadedBee;
    }

    public void LoadBee(Bee bee)
    {
      Destroy(scopeBee);
      this.loadedBee = bee;
      this.isLoaded = true;
    }

    public void SetLaunchPosition(Vector3 worldPosition)
    {
      this.launchPosition = new Vector3 (worldPosition.x, worldPosition.y + launchPositionY, worldPosition.z);
    }

    public Vector3 GetLaunchPosition()
    {
      return this.launchPosition;
    }

    public void SetEndDragPosition(Vector3 endDragPosition)
    {
      Vector3 newDragPosition = new Vector3(endDragPosition.x, launchPositionY, endDragPosition.z);
      if (this.endDragPosition == newDragPosition)
      {
        return;
      }
      Destroy(scopeBee);
      this.endDragPosition = newDragPosition;
      launchScopeBee();

     // beeScope.DrawLine(endDragPosition);
    }

    private void launchScopeBee()
    {
      scopeBee = Instantiate(scopeBeePrefab, launchPosition, Quaternion.LookRotation(calculateDirection(), Vector3.down)); // Quaternion.identity affects rotation?    
      scopeBeeBody = scopeBee.GetComponent<Rigidbody>();
      Vector3 direction = calculateDirection();
      scopeBeeBody.AddForce(-(calculateDirection()));
      beeProperties = beePrefab.GetComponent<BeeBody>();
   //   scopeBeeBody.velocity = scopeBeeBody.velocity.normalized * (beeProperties.GetMoveSpeed() * 100);;
    }

    public Vector3 GetEndDragPosition()
    {
      return this.endDragPosition;
    }

    public void LaunchBee()
    {
      this.loadedBee.Fly();
      launchPosition.y = launchPositionY;
      Vector3 direction = calculateDirection();
      var beeBody = Instantiate(beePrefab, launchPosition, Quaternion.LookRotation(direction, Vector3.down)); // Quaternion.identity affects rotation?
      beeBody.GetComponent<BeeBody>().SetHiveId(this.loadedBee.GetHiveId());
      beeBody.GetComponent<Rigidbody>().AddForce(-direction);
      this.loadedBee.SetBody(beeBody);
      reset();
    }

    public void reset()
    {
      Destroy(scopeBee);
      this.isLoaded = false;
      endDragPosition = launchPosition;
    }


    private Vector3 calculateDirection()
    {
      Vector3 direction = this.endDragPosition - this.launchPosition;
      return new Vector3(direction.x, launchPositionY, direction.z);
    }

    public bool IsLoaded()
    {
      return this.isLoaded;
    }


}
