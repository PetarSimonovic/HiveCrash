using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeLauncher : MonoBehaviour
{
    [SerializeField]
    private GameObject beePrefab;

    private Bee loadedBee;

    private bool isLoaded = false;

    private Vector3 launchPosition;

    private Vector3 endDragPosition;

    public Bee GetLoadedBee()
    {
      return this.loadedBee;
    }

    public void LoadBee(Bee bee)
    {
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
      beeBody.GetComponent<BeeBody>().SetHiveId(this.loadedBee.GetHiveId());
      beeBody.GetComponent<Rigidbody>().AddForce(-direction);
      this.isLoaded = false;
    }


    private Vector3 calculateDirection()
    {
      return this.endDragPosition - this.launchPosition;
    }

    public bool IsLoaded()
    {
      return this.isLoaded;
    }


}
