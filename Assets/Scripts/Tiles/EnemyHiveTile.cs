using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHiveTile : ColliderTile
{
    GameObject enemyHive;

    protected override void reveal()
    {
        base.reveal();
        Debug.Log("Hive here");
    }
}
