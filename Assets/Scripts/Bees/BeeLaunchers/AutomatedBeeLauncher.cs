using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomatedBeeLauncher : BeeLauncher
{

    protected Vector3 target;

    protected override void Start() {
      SetIsPlayer(false);
    }
    protected override void reset()
    {
        this.isLoaded = false;
    }

    public override void LaunchBee()
    {
        base.LaunchBee();
      {
        this.loadedBee.Fly();
        Vector3 launchPosition = fixYPosition(hive.GetPosition());
        GameObject beeObject = Instantiate(beePrefab, launchPosition, Quaternion.LookRotation(target, Vector3.forward)); // Quaternion.identity affects rotation?
        EnemyBeeBody beeBody = beeObject.GetComponent<EnemyBeeBody>(); 
        beeBody.SetHiveId(this.loadedBee.GetHiveId());
        beeBody.SetBee(this.loadedBee);
        beeBody.SetHive(hive);
        beeBody.SetPlayer(isPlayer);
        beeBody.SetTarget(target);
        this.loadedBee.SetBody(beeObject);
        ApplyForceToBeeBody(beeBody.GetComponent<Rigidbody>(), target, launchPosition);
      }
        reset();
    }
    public void SetTarget(Vector3 target) 
    {
      this.target = target;
    }

    protected  void ApplyForceToBeeBody(Rigidbody bee, Vector3 target, Vector3 launchPosition)
    {
      bee.AddForceAtPosition((target - launchPosition).normalized, launchPosition, ForceMode.Impulse);

    }
}
