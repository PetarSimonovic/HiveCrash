using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    [SerializeField]
    private GameObject flowerBody;

    public Timer timer; 

    private GameObject flower;

    private bool isPlanted = false;

    private bool inBloom = false;

    private Vector3 position;

    private FlowerAnimator flowerAnimator;




   private void Start()
    {
      initiateTimer();
    }

    // Update is called once per frame
    private void Update()
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
      flowerAnimator.Bloom();
    }

    private void closeFlower()
    {
      inBloom = false;
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
      timer.SetTime(20f);
    }
}
