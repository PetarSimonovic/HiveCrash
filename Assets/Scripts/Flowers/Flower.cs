using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    [SerializeField]
    GameObject flowerBody;

    GameObject flower;

    public float timer;

    private bool isPlanted = false;

    private bool inBloom = false;

    private Vector3 position;

    private FlowerAnimator flowerAnimator;


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

      public bool IsInBloom()
    {
        return this.inBloom;
    }


    public bool IsPlanted()
    {
        return this.isPlanted;
    }

    public Vector3 GetPosition()
      {
        return this.position;
      }

    public void SetPosition(Vector3 position)
    {
      this.position = position;
    }

    public void CreateBody()
    {
     isPlanted = true;
     flower = Instantiate(flowerBody, transform.position, Quaternion.identity, this.transform); 
     flowerAnimator = flower.GetComponent<FlowerAnimator>();
    }

    private void openFlower()
    {
      inBloom = true;
      Debug.Log("Opening Flower");
      flowerAnimator.Bloom();
    }

    private void closeFlower()
    {
      inBloom = false;

      Debug.Log("Closing Flower");
      flowerAnimator.Close();


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
