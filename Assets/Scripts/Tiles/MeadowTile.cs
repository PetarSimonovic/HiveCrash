using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeadowTile : Tile
{
    [SerializeField]
    GameObject flowerPrefab;

    Flower flower;
    // Start is called before the first frame update

    int chanceOfFlower = 10;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Reveal()
    {
    
        base.Reveal();
        

        if (Random.Range(0, chanceOfFlower) == 1)
        {
            plantFlower();
        }

    }

    private void plantFlower()
    {
        Vector3 flowerPosition = gameObject.transform.position;
        flowerPosition.y = gameObject.transform.position.y + 0.48f;  // change so it adjusts to meadow height
        GameObject flowerObject = Instantiate(flowerPrefab, flowerPosition, Quaternion.identity); // Quaternion.identity affects rotation?
        flower = flowerObject.GetComponent<Flower>();
        flower.SetPosition(flowerPosition);
        flower.CreateBody();  

    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.gameObject.tag == "pollen")
        {
            plantFlower();
        }
    }
}
