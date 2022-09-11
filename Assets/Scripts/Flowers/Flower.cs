using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    [SerializeField]
    GameObject flowerBody;

    public float timer;

    private bool isPlanted;

    private bool inBloom;

    private Animator animator;


    void Awake()
    {
    //  animator = GetComponent<Animator>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      if (isPlanted)
      {
        checkTimer();
      }
    }


    public bool IsPlanted()
    {
        return this.isPlanted;
    }

    public void CreateBody()
    {
     isPlanted = true;
     Instantiate(flowerBody, transform.position, Quaternion.identity); 
    }

    private void openFlower()
    {
      inBloom = true;
      Debug.Log("Opening Flower");
     

;
    }

    private void closeFlower()
    {
      inBloom = false;

      Debug.Log("Closing Flower");


    }


    private void checkTimer()
    {
        timer -= Time.deltaTime;
        if (timer < 0 )
        {
            if (inBloom) {
              closeFlower();
            }
            else 
            {
              openFlower();
            }
            timer = 20;
        }
    }
}
