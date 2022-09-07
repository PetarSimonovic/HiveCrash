using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    [SerializeField]
    GameObject flowerBody;

    private bool isPlanted;

    void Awake()
    {

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsPlanted()
    {
        return this.isPlanted;
    }

    public void CreateBody()
    {
     Instantiate(flowerBody, transform.position, Quaternion.identity); 
    }
}
