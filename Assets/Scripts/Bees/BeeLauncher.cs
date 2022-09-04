using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeLauncher : MonoBehaviour
{
    [SerializeField]
    private GameObject beePrefab;

    [SerializeField]
    private Vector3 endOfTheLine;

    private Bee loadedBee;

    private bool isLoaded = false;

    private Vector3 launchPosition;

    private float launchPositionY = 0.8f;

    private float linePositionY = 2.0f;

    private Vector3 endDragPosition;

    private LineRenderer lineRenderer;


    private void Awake()
    {
      lineRenderer = GetComponent<LineRenderer>();
      Debug.Log(lineRenderer);
    }

    public Bee GetLoadedBee()
    {
      return this.loadedBee;
    }

    public void LoadBee(Bee bee)
    {
      Debug.Log("Linerenderer");
      Debug.Log(lineRenderer);
      this.loadedBee = bee;
      this.isLoaded = true;
    }

    public void SetLaunchPosition(Vector3 launchPosition)
    {
      this.launchPosition = launchPosition;
      this.lineRenderer.SetPosition(0, new Vector3(launchPosition.x, launchPositionY, launchPosition.z));

    }

    public Vector3 GetLaunchPosition()
    {
      return this.launchPosition;
    }

    public void SetEndDragPosition(Vector3 endDragPosition)
    {
      Debug.Log("positions");
      Debug.Log(endDragPosition);
      this.endDragPosition = new Vector3(endDragPosition.x, launchPositionY, endDragPosition.z);
      float xPosition = this.launchPosition.x - this.endDragPosition.x;
      float zPosition = this.launchPosition.z - this.endDragPosition.z;
      endOfTheLine = calculateDirection() + endDragPosition;
      Debug.Log(endOfTheLine);
      this.lineRenderer.SetPosition(1, endOfTheLine);
    }

    public Vector3 GetEndDragPosition()
    {
      return this.endDragPosition;
    }

    public void LaunchBee()
    {
      this.loadedBee.Fly();
      launchPosition.y = 0.8f;
      var beeBody = Instantiate(beePrefab, launchPosition, Quaternion.identity); // Quaternion.identity affects rotation?
      Vector3 direction = calculateDirection();
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
