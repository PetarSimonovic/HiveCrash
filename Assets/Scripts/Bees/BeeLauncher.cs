using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeLauncher : MonoBehaviour
{
    [SerializeField]
    private GameObject beePrefab;

    private Bee loadedBee;

    private bool isLoaded;

    private Vector3 launchPosition;

    private Vector3 endDragPosition;

    public Bee GetLoadedBee()
    {
      return this.loadedBee;
    }

    public void LoadBee(Bee bee)
    {
      this.loadedBee = bee;
      Debug.Log("Bee loaded");
    }

    public void SetLaunchPosition(Vector3 launchPosition)
    {
      Debug.Log("Setting launch position");
      Debug.Log(launchPosition);
      this.launchPosition = launchPosition;
    }

    public Vector3 GetLaunchPosition()
    {
      return this.launchPosition;
    }

    public void SetEndDragPosition(Vector3 endDragPosition)
    {
      this.endDragPosition = new Vector3(endDragPosition.x, 1, endDragPosition.z);
    }

    public Vector3 GetEndDragPosition()
    {
      return this.endDragPosition;
    }

    public void LaunchBee()
    {
      this.loadedBee.Fly();
      launchPosition.y = 1;
      var beeBody = Instantiate(beePrefab, launchPosition, Quaternion.identity); // Quaternion.identity affects rotation?
      Vector3 direction = calculateDirection();
      Debug.Log("Direction");
      Debug.Log(direction);
      beeBody.GetComponent<Rigidbody>().AddForce(launchPosition.x - endDragPosition.x, 0, launchPosition.z - endDragPosition.z);
    }


    private Vector3 calculateDirection()
    {
      Debug.Log("launch position");
      Debug.Log(this.launchPosition);
      Debug.Log("end drag position");
      Debug.Log(this.endDragPosition);
      return this.endDragPosition - this.launchPosition;
    }


}
