using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeLauncher : MonoBehaviour
{
    [SerializeField]
    protected GameObject beePrefab;

    protected Bee loadedBee;

    protected bool isLoaded = false;


    protected float launchPositionY = 0.7f;

    protected Vector3 endPosition;

    protected BeeBody beeProperties;

    protected Hive hive;

    private bool isPlayer;

    
    protected virtual void Start() {
      SetIsPlayer(true);
    }
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
      if (isLoaded)
      {
        this.loadedBee.Fly();
        Vector3 direction = calculateDirection();
        Vector3 launchPosition = fixYPosition(hive.GetPosition());
        GameObject beeBody = Instantiate(beePrefab, launchPosition, Quaternion.LookRotation(-direction, Vector3.forward)); // Quaternion.identity affects rotation?
        beeBody.GetComponent<BeeBody>().SetHiveId(this.loadedBee.GetHiveId());
        beeBody.GetComponent<BeeBody>().SetBee(this.loadedBee);
        beeBody.GetComponent<BeeBody>().SetHive(hive);
        beeBody.GetComponent<BeeBody>().SetPlayer(isPlayer);
        this.loadedBee.SetBody(beeBody);
        ApplyForceToBeeBody(beeBody.GetComponent<Rigidbody>(), launchPosition);
      }
        reset();
    }

    protected virtual void ApplyForceToBeeBody(Rigidbody bee, Vector3 launchPosition)
    {
      bee.AddForceAtPosition(calculateDirection().normalized, launchPosition, ForceMode.Impulse);

    }

    protected virtual void reset()
    {
      this.isLoaded = false;
    }


    protected virtual Vector3 calculateDirection()
    {
      Vector3 direction = this.endPosition - hive.GetPosition();
      return direction;
    }

    public bool IsLoaded()
    {
      return this.isLoaded;
    }

    protected Vector3 fixYPosition(Vector3 position)
    {
      return new Vector3 (position.x, launchPositionY, position.z);
    }


    // Getters and Setters
  

    public virtual void SetEndPosition(Vector3 worldPosition)
    {
    }

    public Vector3 GetEndPosition()
    {
      return this.endPosition;
    }

    public float GetLaunchPositionY()
    {
      return this.launchPositionY;
    }

    public Bee GetLoadedBee()
    {
      return this.loadedBee;
    }

    public void SetHive(Hive hive)
    {
      this.hive = hive;
    }

     public void SetIsPlayer(bool isPlayer) {
      this.isPlayer = isPlayer;
    }


}
