using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBeeLauncher : AutomatedBeeLauncher
{
    private Vector3 playerHivePosition;


    protected override void Start() {
      SetIsPlayer(false);
    }

    public void SetPlayerHivePosition(Vector3 playerHivePosition) {
      this.playerHivePosition = playerHivePosition;
    }

    public override void LaunchBee()
    {
        base.LaunchBee();
     
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
    
        reset();
    }

}
