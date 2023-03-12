using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBeeLauncher : BeeLauncher
{
    private Vector3 playerHivePosition;

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
        GameObject beeObject = Instantiate(beePrefab, launchPosition, Quaternion.LookRotation(playerHivePosition, Vector3.forward)); // Quaternion.identity affects rotation?
        EnemyBeeBody beeBody = beeObject.GetComponent<EnemyBeeBody>(); 
        beeBody.SetHiveId(this.loadedBee.GetHiveId());
        beeBody.SetBee(this.loadedBee);
        beeBody.SetHive(hive);
        beeBody.SetPlayer(isPlayer);
        beeBody.SetPlayerHivePosition(playerHivePosition);
        this.loadedBee.SetBody(beeObject);
        ApplyForceToBeeBody(beeObject.GetComponent<Rigidbody>(), launchPosition);
      }
        reset();
    }

    public void SetPlayerHivePosition(Vector3 playerHivePosition) {
      this.playerHivePosition = playerHivePosition;
    }
}
