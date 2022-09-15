using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeLauncher : MonoBehaviour
{
    [SerializeField]
    private GameObject beePrefab;

    [SerializeField]
    private GameObject beeScopePrefab;

    private Bee loadedBee;

    private bool isLoaded = false;

    private Vector3 launchPosition;

    private float launchPositionY = 0.8f;

    private float linePositionY = 2.0f;

    private Vector3 endDragPosition;

    private GameObject scopeBee;

    private bool scopeOn;


    private void Update()
    {
      if (scopeOn) 
      {
        handleScopeBee();
      }

    }

    private void handleScopeBee()
    {
      Vector3 scopeBeePosition = scopeBee.transform.position;
      scopeBee.transform.position = new Vector3(scopeBeePosition.x, launchPositionY, scopeBeePosition.z);
    }

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

    public void SetLaunchPosition(Vector3 launchPosition)
    {
      this.launchPosition = launchPosition;
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
      scopeBee = Instantiate(beeScopePrefab, launchPosition, Quaternion.LookRotation(calculateDirection(), Vector3.down)); // Quaternion.identity affects rotation?
      scopeBee.GetComponent<Rigidbody>().AddForce(-(calculateDirection() * 100), ForceMode.Impulse);
      scopeOn = true;
    }

    public Vector3 GetEndDragPosition()
    {
      return this.endDragPosition;
    }

    public void LaunchBee()
    {
      Destroy(scopeBee);
      scopeOn = false;
      this.loadedBee.Fly();
      launchPosition.y = 0.8f;
      Vector3 direction = calculateDirection();
      var beeBody = Instantiate(beePrefab, launchPosition, Quaternion.LookRotation(direction, Vector3.down)); // Quaternion.identity affects rotation?
      beeBody.GetComponent<BeeBody>().SetHiveId(this.loadedBee.GetHiveId());
      beeBody.GetComponent<Rigidbody>().AddForce(-direction);
      this.loadedBee.SetBody(beeBody);
      this.isLoaded = false;
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
