using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyHiveTile;

    [SerializeField]
    private GameObject beeLauncher;

    private bool discovered;

    private Hive playerHive;

    private Hive enemyHive;

    private bool enemyHiveIsPlaced;

    private Tile enemyHiveTileStatus;

    void Update()
    {
        if (enemyHiveIsPlaced && !enemyHiveTileStatus.IsHidden()) 
        {
            Debug.Log("Enemy ACTIVATED");
        }
    }

    public void SetPlayerHive(Hive playerHive)
    {
        this.playerHive = playerHive;
    }

    public void PlaceEnemyHive(List<GameObject> tiles)
    {
        int index = Random.Range(0, tiles.Count);
        GameObject tile = tiles[index];
        tiles.Remove(tile);
        Destroy(tile);
        enemyHiveTile = Instantiate(enemyHiveTile, tile.transform.position, Quaternion.identity);
        Debug.Log("Enemy");
        Debug.Log(tile.transform.position);
        enemyHiveTileStatus = enemyHiveTile.GetComponent<Tile>();
        enemyHiveIsPlaced = true;

     //   beeLauncher.SetLaunchPosition(enemyHiveTile.transform.position);
       // beeLauncher.SetEndDragPosition(playerHive.GetPosition());
       // tiles.Add(enemyHiveTile);
    }
}
