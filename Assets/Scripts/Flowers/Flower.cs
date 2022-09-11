using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    [SerializeField]
    GameObject flowerBody;

    public float timer;

    private bool isInBloom;

    private bool blossom;

    void Awake()
    {

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      if (isInBloom)
      {
        checkTimer();
      }
      if (blossom)
      {
        Blossom();
      }
    }

    public void Blossom()
    {
        
    }


    public bool IsInBloom()
    {
        return this.isInBloom;
    }

    public void CreateBody()
    {
     isInBloom = true;
     Instantiate(flowerBody, transform.position, Quaternion.identity); 
    }

    private void checkTimer()
    {
        timer -= Time.deltaTime;
        if (timer < 0 )
        {
            blossom = true;
            timer = 20;
        }
    }
}
