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

    protected float launchPositionY = 0.7f;

    protected Vector3 endPosition;

    protected BeeBody beeProperties;

    
    public void LoadBee(Bee bee)
    {
      if (bee is not null) 
      {
       this.loadedBee = bee;
       this.isLoaded = true;
      }
    }

    public virtual void LaunchBee()
    {
      if (isLoaded && endPosition != launchPosition)
      {
        this.loadedBee.Fly();
        Vector3 direction = calculateDirection();
        var beeBody = Instantiate(beePrefab, launchPosition, Quaternion.LookRotation(direction, Vector3.forward)); // Quaternion.identity affects rotation?
        beeBody.GetComponent<BeeBody>().SetHiveId(this.loadedBee.GetHiveId());
        beeBody.GetComponent<Rigidbody>().AddForce(direction);
        this.loadedBee.SetBody(beeBody);
      }
       reset();
    }

    protected virtual void reset()
    {
      this.isLoaded = false;
      endPosition = launchPosition;
    }


    protected virtual Vector3 calculateDirection()
    {
      Vector3 direction = this.endPosition - this.launchPosition;
      return direction;
    }

    public bool IsLoaded()
    {
      return this.isLoaded;
    }


    // Getters and Setters

    public void SetLaunchPosition(Vector3 worldPosition)
    {
      this.launchPosition = new Vector3 (worldPosition.x, worldPosition.y + launchPositionY, worldPosition.z);
    }

    public Vector3 GetLaunchPosition()
    {
      return this.launchPosition;
    }

    public virtual void SetEndPosition(Vector3 endPosition)
    {
      this.endPosition = endPosition;
    }

    public Vector3 GetEndPosition()
    {
      return this.endPosition;
    }

    public float GetLaunchPositionY()
    {
      return this.launchPositionY;
    }


    public Hive GetHive()
    {
      return this.hive;
    }

    public Bee GetLoadedBee()
    {
      return this.loadedBee;
    }

}
