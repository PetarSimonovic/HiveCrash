using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeLauncher : MonoBehaviour
{
    [SerializeField]
    private GameObject beePrefab;

    [SerializeField]
    private Vector3 endOfTheLine;

    [SerializeField]
    private BeeScope beeScope;

    private Bee loadedBee;

    private bool isLoaded = false;

    private Vector3 launchPosition;

    private float launchPositionY = 0.8f;

    private float linePositionY = 2.0f;

    private Vector3 endDragPosition;



    private void Awake()
    {
      beeScope = GetComponent<BeeScope>();
    }

    public Bee GetLoadedBee()
    {
      return this.loadedBee;
    }

    public void LoadBee(Bee bee)
    {
      beeScope.On();
      this.loadedBee = bee;
      this.isLoaded = true;
    }

    public void SetLaunchPosition(Vector3 launchPosition)
    {
      this.launchPosition = launchPosition;
      beeScope.SetStartOfLine(new Vector3(launchPosition.x, launchPositionY, launchPosition.z));
    }

    public Vector3 GetLaunchPosition()
    {
      return this.launchPosition;
    }

    public void SetEndDragPosition(Vector3 endDragPosition)
    {
      this.endDragPosition = new Vector3(endDragPosition.x, launchPositionY, endDragPosition.z);
      beeScope.DrawLine(endDragPosition);
    }

    public Vector3 GetEndDragPosition()
    {
      return this.endDragPosition;
    }

    public void LaunchBee()
    {
      this.loadedBee.Fly();
      launchPosition.y = 0.8f;
      Vector3 direction = calculateDirection();
      var beeBody = Instantiate(beePrefab, launchPosition, Quaternion.LookRotation(direction, Vector3.down)); // Quaternion.identity affects rotation?
      beeBody.GetComponent<BeeBody>().SetHiveId(this.loadedBee.GetHiveId());
      beeBody.GetComponent<Rigidbody>().AddForce(-direction);
      this.loadedBee.SetBody(beeBody);
      this.isLoaded = false;
      beeScope.Off();
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
