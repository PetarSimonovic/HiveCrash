using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHiveTile : ColliderTile
{
    [SerializeField]
    GameObject enemyHive;

    protected override void reveal()
    {
        base.reveal();
        Instantiate(enemyHive, transform.position, Quaternion.identity);
    }
}
