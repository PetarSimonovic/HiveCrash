using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeLauncher : MonoBehaviour
{
    private Bee loadedBee;

    private Vector3 launchPosition;

    public Bee GetLoadedBee()
    {
      return this.loadedBee;
    }

    public void LoadBee(Bee bee)
    {
      this.loadedBee = bee;
    }

    public void SetLaunchPosition(Vector3 launchPosition)
    {
      this.launchPosition = launchPosition;
    }

    public Vector3 GetLaunchPosition()
    {
      return this.launchPosition;
    }
}
