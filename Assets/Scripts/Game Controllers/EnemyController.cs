using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyHiveTile;

    [SerializeField]
    private EnemyBeeLauncher beeLauncher; 

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

    private float beeLaunchInterval = 3f; 

    private MapCreator mapCreator;



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
          while (invalidPositionForEnemy(tile))
          {
            tile = chooseEnemyHiveTile(tiles);
          }
        }
        Vector3 tilePosition = tile.transform.position;
        int row = tile.GetComponent<Tile>().row;
        int column = tile.GetComponent<Tile>().column;
        tiles.Remove(tile);
        Destroy(tile);
        enemyHiveTile = Instantiate(enemyHiveTile, tilePosition, Quaternion.identity);
        enemyHiveTileStatus = enemyHiveTile.GetComponent<Tile>();
        enemyHiveTileStatus.row = row;
        enemyHiveTileStatus.column = column;
        enemyHiveIsPlaced = true;
        tiles.Add(enemyHiveTile);
        Debug.Log("Enemy is at ");
        tile.GetComponent<Tile>().PrintPosition();
        Debug.Log("Distance from player is"); 
        Debug.Log(Vector3.Distance(tilePosition, playerHive.GetPosition()));   
        mapCreator.SurroundHiveWithMeadows(column, row, false);
    }

    private bool invalidPositionForEnemy(GameObject tile) 
    {
      return (
          tile.GetComponent<Tile>().IsBorderTile() || 
          Vector3.Distance(tile.transform.position, playerHive.GetPosition()) < 3f
      );
    }

    private GameObject chooseEnemyHiveTile(List<GameObject> tiles) 
    {
          int index = Random.Range(0, tiles.Count);
          return tiles[index];
    }

      

      private void initialiseTimer()
        {
        timer = gameObject.GetComponent<Timer>();
        timer.SetOn(true);
        timer.SetCountdownSeconds(beeLaunchInterval);
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
        initialiseHive();
        initialiseTimer();
        activated = true;

      }  

      private void initialiseHive()
      {
        Vector3 hivePosition = new Vector3(enemyHiveTile.transform.position.x, (enemyHiveTile.transform.position.y + (enemyHiveTile.GetComponent<Tile>().GetHeight()/4)), enemyHiveTile.transform.position.z);
        enemyHivePrefab = Instantiate(enemyHivePrefab, hivePosition, Quaternion.identity);
        enemyHive = enemyHivePrefab.GetComponent<Hive>();
        enemyHivePrefab.name = enemyHive.GetId();
        beeLauncher.SetHive(enemyHive);
        beeLauncher.SetPlayerHivePosition(playerHive.GetPosition());
        enemyHive.LaunchTextBubble("Rival hive disturbed", false);
        int starterBees = 5;
        for (int i = 0; i < starterBees; i++)
          {
            enemyHive.AddBee();
          }
      }


      private void launchBee()
      {
        Vector3 target = getTarget();
        beeLauncher.LoadBee(enemyHive.GetBee());
        //Vector3 endPosition = new Vector3 (Random.Range(-4.0f, 4.0f), 0.5f, Random.Range(-4.0f, 4.0f))
        beeLauncher.SetTarget(target);
        if (beeLauncher.IsLoaded()) {
          beeLauncher.LaunchBee();
        }
      }

      public Hive GetEnemyHive()
      {
        return this.enemyHive;
      }

      private Vector3 getTarget() 
      {
       try {
          Bee bee = playerHive.GetBees().Find(bee => bee.GetBody().GetComponent<BeeBody>().CollectingPollen() == true);
          return bee.GetBody().transform.position;
       }
       catch {
          return playerHive.GetPosition();
       }
      }

      public void SetDisplayController(DisplayController displayController)
      {
        this.displayController = displayController;
      }

    public void SetMapCreator(MapCreator mapCreator) {
      this.mapCreator = mapCreator;
    }

    public bool EnemyHasCrashed() 
    {
      if (!activated) return false;
      return enemyHive.HasCrashed();
    }
}
