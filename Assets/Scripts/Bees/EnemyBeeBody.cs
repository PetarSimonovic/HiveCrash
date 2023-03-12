using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBeeBody : BeeBody
{
    private Vector3 playerHivePosition;

    public void SetPlayerHivePosition(Vector3 playerHivePosition) {
        this.playerHivePosition = playerHivePosition;
    }
}
