using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    [SerializeField]
    private GameObject flowerBody1;


    [SerializeField]
    private GameObject flowerBody2;


    [SerializeField]
    private GameObject flowerBody3;
    

    public Timer timer; 

    private GameObject flower;

    private bool isFullyGrown = false;

    private bool inBloom = false;

    private Vector3 position;

    private FlowerAnimator flowerAnimator;

    private bool hasBee;

    private int growthStage = 1;




   private void Start()
    {
      initiateTimer();
    }

    // Update is called once per frame
    private void Update()
    {
      if (isFullyGrown)
      {
        checkTimer();
      }
    }

      public bool IsInBloom()
    {
        return this.inBloom;
    }


    public bool IsFullyGrown()
    {
        return this.isFullyGrown;
    }

    public Vector3 GetPosition()
      {
        return this.position;
      }

    public void SetPosition(Vector3 position)
    {
      this.position = position;
    }

    

    private GameObject getFlowerPrefab()
    {
      switch (growthStage) 
      {
        case 1:
          Debug.Log(flowerBody1);
          return flowerBody1;
        case 2: 
          Debug.Log(flowerBody2);
          return flowerBody2;
        default:
          return flowerBody3;

      }
    }

    private void openFlower()
    {
      inBloom = true;
      flowerAnimator.Bloom();
    }

    private void closeFlower()
    {
      inBloom = false;
      RemoveBee();
      flowerAnimator.Close();
    }


    private void checkTimer()
    {
        if (inBloom != timer.IsOn())
        {
            inBloom = timer.IsOn();
            if (inBloom) {
              openFlower();
            }
            else 
            {
              closeFlower();
            }
        }
    }

    private void initiateTimer()
    {
      timer = gameObject.GetComponent<Timer>();
      timer.SetOn(true);
      timer.SetCountdownSeconds(20f);
    }

    public void SetGrowthStage(int growthStage) 
    {
      this.growthStage = growthStage;
    }

    public void Grow() 
    {
      if (isFullyGrown) {return;}
      CreateFlower();
      growthStage += 1;
      if (growthStage == 4)
      {
        isFullyGrown = true;
      }

      }

      public void CreateFlower()
    {
      if (flower != null) {Destroy(flower);}
      flower = Instantiate(getFlowerPrefab(), transform.position, Quaternion.identity, this.transform); 
      flowerAnimator = flower.GetComponent<FlowerAnimator>();
    }


    public void PlaceBee() {
      hasBee = true;
    }

    public void RemoveBee() {
      hasBee = false;
    }

    public bool HasBee() {
      return hasBee;
    }
    
    public void DestroySelf() {
      Debug.Log("FLower is destroying itself!");
    }


    
}
