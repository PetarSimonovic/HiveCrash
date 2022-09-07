using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject flowerPrefab;

    private List<GameObject> flowers = new List<GameObject>();

    private List<GameObject> meadows = new List<GameObject>();


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMeadows(List<GameObject> tiles)
    {
        foreach (GameObject tile in tiles)
        {
            if (tile.tag == "meadow") 
            {
                meadows.Add(tile);
            }
        } 
    }

    public void CheckMeadows()
    {   
        foreach (GameObject meadow in meadows)
        {
            if (!isHidden(meadow))
            { 
               
               plantFlower(meadow.transform.position);
            }
        }
        removeRevealedMeadows();
    }

    private bool isHidden(GameObject meadow)
    {
        return meadow.GetComponent<Tile>().IsHidden();

    }

    private void removeRevealedMeadows()
    {
        meadows.RemoveAll(meadow => !isHidden(meadow));
    }

    private void plantFlower(Vector3 meadowPosition)
    {
      Vector3 flowerPosition = meadowPosition;
      flowerPosition.y = meadowPosition.y + 0.48f;  // change so it adjusts to meadow height
      var flower = Instantiate(flowerPrefab, flowerPosition, Quaternion.identity); // Quaternion.identity affects rotation?
      flowers.Add(flower);
      if (Random.Range(0, 10) == 1)
      {
        flower.GetComponent<Flower>().CreateBody();  
      }
    }
}
