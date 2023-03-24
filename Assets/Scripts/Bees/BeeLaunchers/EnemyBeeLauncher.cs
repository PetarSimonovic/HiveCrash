using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBeeLauncher : AutomatedBeeLauncher
{
    private Vector3 playerHivePosition;

    private Vector3 target;

    protected override void Start() {
      SetIsPlayer(false);
    }

    public void SetPlayerHivePosition(Vector3 playerHivePosition) {
      this.playerHivePosition = playerHivePosition;
    }

}
