using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeLauncher : MonoBehaviour
{
    [SerializeField]
    protected GameObject beePrefab;

    protected Bee loadedBee;

    protected Hive hive;

    protected bool isLoaded = false;

    protected Vector3 launchPosition;

    protected float launchPositionY = 0.5f;

    protected Vector3 endDragPosition;

    protected BeeBody beeProperties;

    public Bee GetLoadedBee()
    {
      return this.loadedBee;
    }

    public void LoadBee()
    {
      Bee bee = hive.GetBee();
      if (bee is not null) 
      {
       this.loadedBee = bee;
       this.isLoaded = true;
      }
    }

     public void LoadBee(Bee bee)
    {
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

    public virtual void SetEndDragPosition(Vector3 endDragPosition)
    {
      Vector3 newDragPosition = new Vector3(endDragPosition.x, launchPositionY, endDragPosition.z);
      this.endDragPosition = newDragPosition;

     // beeScope.DrawLine(endDragPosition);
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

    protected virtual void reset()
    {
      this.isLoaded = false;
      endDragPosition = launchPosition;
    }


    protected Vector3 calculateDirection()
    {
      Vector3 direction = this.endDragPosition - this.launchPosition;
      return new Vector3(direction.x, launchPositionY, direction.z);
    }

    public bool IsLoaded()
    {
      return this.isLoaded;
    }

    public void SetHive(Hive hive)
    {
      this.hive = hive;
      SetLaunchPosition(hive.GetPosition());
    }

    public float GetLaunchPositionY()
    {
      return this.launchPositionY;
    }


}
