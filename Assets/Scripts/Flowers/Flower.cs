using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    [SerializeField]
    private GameObject flowerBody;

    [SerializeField]
    private GameObject pollenCloudPrefab;

    private GameObject pollenCloud;

    public Timer timer; 

    private GameObject flower;

    private bool isPlanted = false;

    private bool inBloom = false;

    private Vector3 position;

    private FlowerAnimator flowerAnimator;

    private bool hasBee;




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

    // private void createPollenCloud()
    // {
    //   Vector3 pollenCloudPositiion = flower.transform.position;
    //   pollenCloudPositiion.y = 0.75f;
    //   pollenCloud = Instantiate(pollenCloudPrefab, pollenCloudPositiion, Quaternion.identity);
    // }

    public void PlaceBee() {
      hasBee = true;
     // createPollenCloud();

    }

    public void RemoveBee() {
      hasBee = false;
      //Destroy(pollenCloud);
    }

    public bool HasBee() {
      return hasBee;
    }


    
}
