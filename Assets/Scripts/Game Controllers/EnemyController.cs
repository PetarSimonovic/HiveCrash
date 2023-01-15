using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyHiveTile;

    [SerializeField]
    private BeeLauncher beeLauncher; 

    [SerializeField]
    private BeeController beeController; 

    [SerializeField]
    private GameObject enemyHivePrefab;

    [SerializeField]
    private DisplayController displayController;

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
        GameObject tile;

        if (Globals.test) 
        {
          tile = tiles.Find(t => t.GetComponent<Tile>().column == 14 && t.GetComponent<Tile>().row == 9);
        } 
        else 
        {
          tile = chooseEnemyHiveTile(tiles);
          while (tile.GetComponent<Tile>().IsBorderTile())
          {
            tile = chooseEnemyHiveTile(tiles);
          }
        }
        tiles.Remove(tile);
        Destroy(tile);
        Vector3 tilePosition = new Vector3 (tile.transform.position.x, 0.1f, tile.transform.position.y);
        enemyHiveTile = Instantiate(enemyHiveTile, tile.transform.position, Quaternion.identity);
        enemyHiveTileStatus = enemyHiveTile.GetComponent<Tile>();
        enemyHiveIsPlaced = true;
        tiles.Add(enemyHiveTile);
        
    }

    private GameObject chooseEnemyHiveTile(List<GameObject> tiles) 
    {
          int index = Random.Range(0, tiles.Count);
          return tiles[index];
    }

        private void initialiseBeeController()
        {
            beeController = Instantiate(beeController);
            beeController.SetHive(enemyHive);
            beeController.AddBees(5);
        }

      private void initialiseTimer()
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
        initialiseHive();
        initialiseBeeController();
        initialiseTimer();
      }  

      private void initialiseHive()
      {
        Vector3 hivePosition = new Vector3(enemyHiveTile.transform.position.x, (enemyHiveTile.transform.position.y + (enemyHiveTile.GetComponent<Tile>().GetHeight()/4)), enemyHiveTile.transform.position.z);
        enemyHivePrefab = Instantiate(enemyHivePrefab, hivePosition, Quaternion.identity);
        enemyHive = enemyHivePrefab.GetComponent<Hive>();
        displayController.SetEnemyHive(enemyHive);
        enemyHivePrefab.name = enemyHive.GetId();
        beeLauncher.SetHive(enemyHive);


      }


      private void launchBee()
      {
        beeLauncher.LoadBee(enemyHive.GetBee());
        //Vector3 endPosition = new Vector3 (Random.Range(-4.0f, 4.0f), 0.5f, Random.Range(-4.0f, 4.0f));
        beeLauncher.SetEndPosition(playerHive.GetPosition());
        if (beeLauncher.IsLoaded()) {
          beeLauncher.LaunchBee();
        }
      }

      public Hive GetEnemyHive()
      {
        return this.enemyHive;
      }

      public void SetDisplayController(DisplayController displayController)
      {
        this.displayController = displayController;
      }
}
