using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyHiveTile;

    [SerializeField]
    private EnemyBeeLauncher beeLauncher; // This is GameObject ...

    [SerializeField]
    private BeeController beeController; // But this is BeeController?!?

    [SerializeField]
    private GameObject enemyHivePrefab;

    private bool activated;

    private Hive playerHive;


    private Hive enemyHive;

    private bool enemyHiveIsPlaced;

    private Tile enemyHiveTileStatus;

    private Timer timer;


    void Update()
    {
        if (enemyHiveIsPlaced && !enemyHiveTileStatus.IsHidden()) 
        {
            if (!activated)
            {
                activate();
            }
            checkTimer();
        }
    }

    public void SetPlayerHive(Hive playerHive)
    {
        this.playerHive = playerHive;
    }

    public void PlaceEnemyTile(List<GameObject> tiles)
    {
        int index = Random.Range(0, tiles.Count);
        GameObject tile = tiles[index];
        tiles.Remove(tile);
        Destroy(tile);
        enemyHiveTile = Instantiate(enemyHiveTile, tile.transform.position, Quaternion.identity);
        enemyHiveTileStatus = enemyHiveTile.GetComponent<Tile>();
        enemyHiveIsPlaced = true;
        tiles.Add(enemyHiveTile);
    }

        private void initaliseBeeController()
        {
            beeController = Instantiate(beeController);
            beeController.SetHive(enemyHive);
            beeController.AddBees(5);
        }

      private void initaliseTimer()
        {
        timer = gameObject.GetComponent<Timer>();
        timer.SetOn(true);
        timer.SetCountdownSeconds(10f);
        }

      private void checkTimer()
      {
        if (!timer.IsOn())
        {
            timer.SetOn(true);
            timer.Restart();
            launchBee();
        }
      }

      private void activate()
      {
        activated = true;
        initaliseHive();
        initaliseBeeController();
        initaliseTimer();
        initialiseBeeLauncher();
      }  

      private void initaliseHive()
      {
        enemyHivePrefab = Instantiate(enemyHivePrefab, enemyHiveTile.transform.position, Quaternion.identity);
        enemyHive = enemyHivePrefab.GetComponent<Hive>();
        enemyHive.SetPosition(enemyHivePrefab.transform.position);
        enemyHivePrefab.name = enemyHive.GetId();
      }

      private void initialiseBeeLauncher()
      {
        beeLauncher.SetLaunchPosition(enemyHive.GetPosition());
        beeLauncher.SetHive(enemyHive);
      }

      private void launchBee()
      {
        beeLauncher.LoadBee();
        Vector3 endPosition = new Vector3 (Random.Range(-4.0f, 4.0f), 0.5f, Random.Range(-4.0f, 4.0f));
        beeLauncher.SetEndPosition(endPosition);
        if (beeLauncher.IsLoaded()) {
          beeLauncher.LaunchBee();
        }
      }
}
