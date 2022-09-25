using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyHiveTile;

    private Hive playerHive;

    private Hive enemyHive;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
       // tiles.Add(enemyHiveTile);
    }
}
